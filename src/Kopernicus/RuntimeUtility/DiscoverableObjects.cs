﻿/**
 * Kopernicus Planetary System Modifier
 * ------------------------------------------------------------- 
 * This library is free software; you can redistribute it and/or
 * modify it under the terms of the GNU Lesser General Public
 * License as published by the Free Software Foundation; either
 * version 3 of the License, or (at your option) any later version.
 *
 * This library is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 * Lesser General Public License for more details.
 *
 * You should have received a copy of the GNU Lesser General Public
 * License along with this library; if not, write to the Free Software
 * Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston,
 * MA 02110-1301  USA
 * 
 * This library is intended to be used as a plugin for Kerbal Space Program
 * which is copyright 2011-2017 Squad. Your usage of Kerbal Space Program
 * itself is governed by the terms of its EULA, not the license above.
 * 
 * https://kerbalspaceprogram.com
 */

using Kopernicus.Configuration.Asteroids;
using KSPAchievements;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Kopernicus
{
    // Class to manage Asteroids
    [KSPScenario(ScenarioCreationOptions.AddToAllGames, GameScenes.FLIGHT, GameScenes.TRACKSTATION, GameScenes.SPACECENTER)]
    public class DiscoverableObjects : ScenarioModule
    {
        // All ateroid configurations we know
        public static List<Asteroid> asteroids { get; set; }

        // Spawn interval
        public Single spawnInterval = 0.1f;

        // Construct
        static DiscoverableObjects()
        {
            asteroids = new List<Asteroid>();
        }

        // Kill the old spawner
        public override void OnAwake()
        {
            if (!CompatibilityChecker.IsCompatible())
            {
                Destroy(this);
                return;
            }
            base.OnAwake();
        }

        // Startup
        void Start()
        {
            // Kill old Scenario Discoverable Objects without editing the collection while iterating through the same collection
            // @Squad: I stab you with a try { } catch { } block.
            if (HighLogic.CurrentGame.RemoveProtoScenarioModule(typeof(ScenarioDiscoverableObjects)))
            {
                // RemoveProtoScenarioModule doesn't remove the actual Scenario; workaround!
                foreach (ScenarioDiscoverableObjects scen in
                    Resources.FindObjectsOfTypeAll(typeof(ScenarioDiscoverableObjects)))
                {
                    scen.StopAllCoroutines();
                    Destroy(scen);
                }
                Debug.Log("[Kopernicus] ScenarioDiscoverableObjects successfully removed.");
            }

            foreach (Asteroid asteroid in asteroids)
                StartCoroutine(AsteroidDaemon(asteroid));
        }

        // Update the Asteroids
        public void UpdateAsteroid(Asteroid asteroid, Double time)
        {
            List<Vessel> spaceObjects = FlightGlobals.Vessels.Where(v => !v.DiscoveryInfo.HaveKnowledgeAbout(DiscoveryLevels.StateVectors) && v.DiscoveryInfo.GetSignalLife(Planetarium.GetUniversalTime()) == 0).ToList();
            Int32 limit = Random.Range(asteroid.spawnGroupMinLimit, asteroid.spawnGroupMaxLimit);
            if (spaceObjects.Any())
            {
                Vessel vessel = spaceObjects.First();
                Debug.Log("[Kopernicus] " + vessel.vesselName + " has been untracked for too long and is now lost.");
                vessel.Die();
            }
            else if (GameVariables.Instance.UnlockedSpaceObjectDiscovery(ScenarioUpgradeableFacilities.GetFacilityLevel(SpaceCenterFacility.TrackingStation)))
            {
                Int32 untrackedCount = FlightGlobals.Vessels.Count(v => !v.DiscoveryInfo.HaveKnowledgeAbout(DiscoveryLevels.StateVectors)) - spaceObjects.Count;
                Int32 max = Mathf.Max(untrackedCount, limit);
                if (max > untrackedCount)
                {
                    if (Random.Range(0, 100) < asteroid.probability)
                    {
                        UInt32 seed = (UInt32)Random.Range(0, Int32.MaxValue);
                        Random.InitState((Int32)seed);
                        SpawnAsteroid(asteroid, seed);
                    }
                    else
                    {
                        Debug.Log("[Kopernicus] No new objects this time. (Probablility is " + asteroid.probability.Value + "%)");
                    }
                }
            }
        }

        // Spawn the actual asteroid
        public void SpawnAsteroid(Asteroid asteroid, UInt32 seed)
        {
            // Create Default Orbit
            Orbit orbit = null;
            CelestialBody body = null;

            // Select Orbit Type
            Int32 type = Random.Range(0, 3);
            if (type == 0 && asteroid.location.around.Count != 0)
            {
                // Around
                IEnumerable<Location.AroundLoader> arounds = GetProbabilityList(asteroid.location.around, asteroid.location.around.Select(a => a.probability.Value));
                Location.AroundLoader around = arounds.ElementAt(Random.Range(0, arounds.Count()));
                body = PSystemManager.Instance.localBodies.Find(b => b.name == around.body);
                if (!body) return;
                if (around.reached && !ReachedBody(body)) return;
                orbit = new Orbit();
                orbit.referenceBody = body;
                orbit.eccentricity = around.eccentricity;
                orbit.semiMajorAxis = around.semiMajorAxis;
                orbit.inclination = around.inclination;
                orbit.LAN = around.longitudeOfAscendingNode;
                orbit.argumentOfPeriapsis = around.argumentOfPeriapsis;
                orbit.meanAnomalyAtEpoch = around.meanAnomalyAtEpoch;
                orbit.epoch = around.epoch;
                orbit.Init();
            }
            else if (type == 1 && asteroid.location.nearby.Count != 0)
            {
                // Nearby
                Location.NearbyLoader[] nearbys = GetProbabilityList(asteroid.location.nearby, asteroid.location.nearby.Select(a => a.probability.Value)).ToArray();
                Location.NearbyLoader nearby = nearbys[Random.Range(0, nearbys.Length)];
                body = PSystemManager.Instance.localBodies.Find(b => b.name == nearby.body);
                if (!body) return;
                if (nearby.reached && !ReachedBody(body)) return;
                orbit = new Orbit();
                orbit.eccentricity = body.orbit.eccentricity + nearby.eccentricity;
                orbit.semiMajorAxis = body.orbit.semiMajorAxis * nearby.semiMajorAxis;
                orbit.inclination = body.orbit.inclination + nearby.inclination;
                orbit.LAN = body.orbit.LAN * nearby.longitudeOfAscendingNode;
                orbit.argumentOfPeriapsis = body.orbit.argumentOfPeriapsis * nearby.argumentOfPeriapsis;
                orbit.meanAnomalyAtEpoch = body.orbit.meanAnomalyAtEpoch * nearby.meanAnomalyAtEpoch;
                orbit.epoch = body.orbit.epoch;
                orbit.referenceBody = body.orbit.referenceBody;
                orbit.Init();
            }
            else if (type == 2 && asteroid.location.flyby.Count != 0)
            {
                // Flyby
                Location.FlybyLoader[] flybys = GetProbabilityList(asteroid.location.flyby, asteroid.location.flyby.Select(a => a.probability.Value)).ToArray();
                Location.FlybyLoader flyby = flybys[Random.Range(0, flybys.Length)];
                body = PSystemManager.Instance.localBodies.Find(b => b.name == flyby.body);
                if (!body) return;
                if (flyby.reached && !ReachedBody(body)) return;
                orbit = Orbit.CreateRandomOrbitFlyBy(body, Random.Range(flyby.minDuration, flyby.maxDuration));
            }

            // Check 
            if (orbit == null)
            {
                Debug.Log("[Kopernicus] No new objects this time. (Probablility is " + asteroid.probability.Value + "%)");
                return;
            }

            // Name
            String name = DiscoverableObjectsUtil.GenerateAsteroidName();

            // Lifetime
            Double lifetime = Random.Range(asteroid.minUntrackedLifetime, asteroid.maxUntrackedLifetime) * 24d * 60d * 60d;
            Double maxLifetime = asteroid.maxUntrackedLifetime * 24d * 60d * 60d;

            // Size
            UntrackedObjectClass size = (UntrackedObjectClass)((Int32)(asteroid.size.Value.Evaluate(Random.Range(0f, 1f)) * Enum.GetNames(typeof(UntrackedObjectClass)).Length));

            // Spawn
            ConfigNode vessel = ProtoVessel.CreateVesselNode(
                name,
                VesselType.SpaceObject,
                orbit,
                0,
                new[]
                {
                    ProtoVessel.CreatePartNode(
                        "PotatoRoid",
                        seed,
                        new ProtoCrewMember[0]
                    )
                },
                new ConfigNode("ACTIONGROUPS"),
                ProtoVessel.CreateDiscoveryNode(
                    DiscoveryLevels.Presence,
                    size,
                    lifetime,
                    maxLifetime
                )
            );
            OverrideNode(ref vessel, asteroid.vessel);
            ProtoVessel protoVessel = new ProtoVessel(vessel, HighLogic.CurrentGame);
            if (asteroid.uniqueName && FlightGlobals.Vessels.Count(v => v.vesselName == protoVessel.vesselName) != 0) return;
            Kopernicus.Events.OnRuntimeUtilitySpawnAsteroid.Fire(asteroid, protoVessel);
            protoVessel.Load(HighLogic.CurrentGame.flightState);
            GameEvents.onNewVesselCreated.Fire(protoVessel.vesselRef);
            Debug.Log("[Kopernicus] New object found near " + body.name + ": " + protoVessel.vesselName + "!");
        }

        // Asteroid Spawner
        public IEnumerator<WaitForSeconds> AsteroidDaemon(Asteroid asteroid)
        {
            while (true)
            {
                // Update Asteroids
                UpdateAsteroid(asteroid, Planetarium.GetUniversalTime());

                // Wait
                yield return new WaitForSeconds(Mathf.Max(asteroid.interval / TimeWarp.CurrentRate, spawnInterval));
            }
        }

        // Gets a list to reflect probablilties
        protected IEnumerable<T> GetProbabilityList<T>(IEnumerable<T> enumerable, IEnumerable<Single> probabilities)
        {
            for (Int32 i = 0; i < enumerable.Count(); i++)
                for (Int32 j = 0; j < probabilities.ElementAt(i); j++)
                    yield return enumerable.ElementAt(i);
        }

        // Overrides a ConfigNode recursively
        protected void OverrideNode(ref ConfigNode original, ConfigNode custom, Boolean rec = false)
        {
            // null checks
            if (original == null || custom == null)
                return;

            // Go through the values
            foreach (ConfigNode.Value value in custom.values)
                original.SetValue(value.name, value.value, true);

            // Get nodes that should get removed
            if (original.HasValue("removeNodes"))
            {
                String[] names = original.GetValue("removeNodes").Split(new Char[] { ',', ' ', ';' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (String name in names)
                    original.RemoveNodes(name);
            }

            // Go through the nodes
            foreach (ConfigNode node in custom.nodes)
            {
                if (!original.HasNode(node.name))
                {
                    original.AddNode(node).AddValue("__PATCHED__", "__YES__");
                    continue;
                }
                ConfigNode[] nodes = original.GetNodes(node.name);
                if (nodes.Any(n => !n.HasValue("__PATCHED__")))
                {
                    ConfigNode node_ = nodes.First(n => !n.HasValue("__PATCHED__"));
                    OverrideNode(ref node_, node, true);
                    node_.AddValue("__PATCHED__", "__YES__");
                }
                else
                    original.AddNode(node).AddValue("__PATCHED__", "__YES__");
            }

            // Remove patches
            if (!rec)
                Utility.DoRecursive(original, o => o.GetNodes(), node => node.RemoveValues("__PATCHED__"));
        }

        // Determines whether a body was already visited
        protected Boolean ReachedBody(CelestialBody body)
        {
            CelestialBodySubtree bodyTree = ProgressTracking.Instance.GetBodyTree(body.name);
            return bodyTree != null && bodyTree.IsReached;
        }
    }
}
