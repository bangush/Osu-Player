﻿using Milky.OsuPlayer.Common.Data.EF.Model;
using Milky.OsuPlayer.Common.Migrations;
using osu_database_reader.Components.Beatmaps;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Threading.Tasks;
using OSharp.Beatmap.MetaData;

namespace Milky.OsuPlayer.Common.Data.EF
{
    public class BeatmapDbContext : DbContext
    {
        static BeatmapDbContext()
        {
            Database.SetInitializer(
                new MigrateDatabaseToLatestVersion<BeatmapDbContext, BeatmapMigrationConfiguration>(true));
        }

        public DbSet<Beatmap> Beatmaps { get; set; }

        public BeatmapDbContext() : base("name=beatmapDb")
        {
            //Database.Initialize(false);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Conventions
            //    .Remove<System.Data.Entity.ModelConfiguration.Conventions.PluralizingTableNameConvention>();
            modelBuilder.Configurations.Add(new BeatmapMap());
            base.OnModelCreating(modelBuilder);
        }

        public static async Task SyncMapsFromHoLLyAsync(IEnumerable<BeatmapEntry> entry, bool addOnly)
        {
            await Task.Run(() =>
            {
                using (var context = new BeatmapDbContext())
                {
                    if (addOnly)
                    {
                        var dbMaps = context.Beatmaps.Where(k => !k.InOwnFolder).ToList();
                        var newList = entry.Select(Beatmap.ParseFromHolly).ToList();
                        var except = newList.Except(dbMaps, new Beatmap.Comparer(true));

                        context.Beatmaps.AddRange(except);
                        context.SaveChanges();
                    }
                    else
                    {
                        var dbMaps = context.Beatmaps.Where(k => !k.InOwnFolder);
                        context.Beatmaps.RemoveRange(dbMaps);

                        var osuMaps = entry.Select(Beatmap.ParseFromHolly);
                        context.Beatmaps.AddRange(osuMaps);

                        context.SaveChanges();
                    }
                }
            });
        }
    }

    internal class MapIdentifiable : IEqualityComparer<IMapIdentifiable>
    {
        public bool Equals(IMapIdentifiable x, IMapIdentifiable y)
        {
            if (x == null && y == null)
                return true;
            if (x == null || y == null)
                return false;
            return x.GetIdentity().Equals(y.GetIdentity());
        }

        public int GetHashCode(IMapIdentifiable obj)
        {
            return obj.FolderName.GetHashCode() + obj.FolderName.GetHashCode();
        }
    }

    public class BeatmapMap : EntityTypeConfiguration<Beatmap>
    {
        public BeatmapMap()
        {
            this.ToTable("beatmap");
            this.HasKey(m => m.Id);
        }
    }
}
