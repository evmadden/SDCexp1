﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SDCode.Web.Classes.Database;

namespace SDCode.Web.Migrations
{
    [DbContext(typeof(SQLiteDBContext))]
    partial class SQLiteDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.8");

            modelBuilder.Entity("SDCode.Web.Models.Data.ConsentDbModel", b =>
                {
                    b.Property<string>("ParticipantID")
                        .HasColumnType("TEXT");

                    b.Property<bool>("ADHD")
                        .HasColumnType("INTEGER");

                    b.Property<bool?>("AgreeLanguage")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("AgreeParticipate")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("AltShifts")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("DataProtection")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("HeadInjury")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("InfoSheet")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("NPSDisorder")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("NormalVision")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("VisionProblems")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Withdraw")
                        .HasColumnType("INTEGER");

                    b.HasKey("ParticipantID");

                    b.ToTable("Consents");
                });

            modelBuilder.Entity("SDCode.Web.Models.Data.DemographicsDbModel", b =>
                {
                    b.Property<string>("ParticipantID")
                        .HasColumnType("TEXT");

                    b.Property<string>("Age")
                        .HasColumnType("TEXT");

                    b.Property<string>("Bilingual")
                        .HasColumnType("TEXT");

                    b.Property<string>("CurrentCountry")
                        .HasColumnType("TEXT");

                    b.Property<bool?>("Glasses")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Handed")
                        .HasColumnType("INTEGER");

                    b.Property<bool?>("Impairments")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Language")
                        .HasColumnType("TEXT");

                    b.Property<int?>("Sex")
                        .HasColumnType("INTEGER");

                    b.Property<string>("YearStudy")
                        .HasColumnType("TEXT");

                    b.HasKey("ParticipantID");

                    b.ToTable("Demographics");
                });

            modelBuilder.Entity("SDCode.Web.Models.Data.EpworthDbModel", b =>
                {
                    b.Property<string>("ParticipantID")
                        .HasColumnType("TEXT");

                    b.Property<int?>("Afternoon")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Lunch")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("PassengerCar")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("PublicPlace")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Reading")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("TV")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Talking")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Traffic")
                        .HasColumnType("INTEGER");

                    b.HasKey("ParticipantID");

                    b.ToTable("Epworths");
                });

            modelBuilder.Entity("SDCode.Web.Models.Data.PSQIDbModel", b =>
                {
                    b.Property<string>("ParticipantID")
                        .HasColumnType("TEXT");

                    b.Property<int?>("Bathroom")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("BedPartner")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("BreathPause")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Breathing")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Cold")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Disorientation")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Dreams")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Enthusiasm")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Hot")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Legs")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Medication")
                        .HasColumnType("INTEGER");

                    b.Property<string>("MonthBed")
                        .HasColumnType("TEXT");

                    b.Property<string>("MonthLatency")
                        .HasColumnType("TEXT");

                    b.Property<string>("MonthWake")
                        .HasColumnType("TEXT");

                    b.Property<int?>("No30Min")
                        .HasColumnType("INTEGER");

                    b.Property<string>("OtherDescribe")
                        .HasColumnType("TEXT");

                    b.Property<int?>("OtherFrequency")
                        .HasColumnType("INTEGER");

                    b.Property<string>("OtherRestDescribe")
                        .HasColumnType("TEXT");

                    b.Property<int?>("OtherRestless")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Pain")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("PartSnore")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("SleepQuality")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Sleepiness")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Snoring")
                        .HasColumnType("INTEGER");

                    b.Property<string>("TotalHours")
                        .HasColumnType("TEXT");

                    b.Property<string>("TotalMinutes")
                        .HasColumnType("TEXT");

                    b.Property<int?>("WASO")
                        .HasColumnType("INTEGER");

                    b.HasKey("ParticipantID");

                    b.ToTable("PSQIs");
                });

            modelBuilder.Entity("SDCode.Web.Models.Data.ResponseDbDataModel", b =>
                {
                    b.Property<Guid>("SessionID")
                        .HasColumnType("TEXT");

                    b.Property<string>("ParticipantID")
                        .HasColumnType("TEXT");

                    b.Property<string>("TestName")
                        .HasColumnType("TEXT");

                    b.Property<string>("Image")
                        .HasColumnType("TEXT");

                    b.Property<int>("Confidence")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Congruency")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Context")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Feedback")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Judgement")
                        .HasColumnType("INTEGER");

                    b.Property<long>("ReactionTime")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("WhenUtc")
                        .HasColumnType("TEXT");

                    b.HasKey("SessionID", "ParticipantID", "TestName", "Image");

                    b.ToTable("ResponseDatas");
                });

            modelBuilder.Entity("SDCode.Web.Models.Data.SessionMetaDbModel", b =>
                {
                    b.Property<string>("ParticipantID")
                        .HasColumnType("TEXT");

                    b.Property<string>("SessionName")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("FinishedWhenUtc")
                        .HasColumnType("TEXT");

                    b.Property<string>("NeglectedReason")
                        .HasColumnType("TEXT");

                    b.Property<string>("ObscuredReason")
                        .HasColumnType("TEXT");

                    b.HasKey("ParticipantID", "SessionName");

                    b.ToTable("SessionMetas");
                });

            modelBuilder.Entity("SDCode.Web.Models.Data.SleepQuestionsDbModel", b =>
                {
                    b.Property<string>("ParticipantID")
                        .HasColumnType("TEXT");

                    b.Property<string>("DelayedBed")
                        .HasColumnType("TEXT");

                    b.Property<string>("DelayedLatency")
                        .HasColumnType("TEXT");

                    b.Property<string>("DelayedTST")
                        .HasColumnType("TEXT");

                    b.Property<string>("DelayedWake")
                        .HasColumnType("TEXT");

                    b.Property<string>("FollowupBed")
                        .HasColumnType("TEXT");

                    b.Property<string>("FollowupLatency")
                        .HasColumnType("TEXT");

                    b.Property<string>("FollowupTST")
                        .HasColumnType("TEXT");

                    b.Property<string>("FollowupWake")
                        .HasColumnType("TEXT");

                    b.Property<string>("ImmediateBed")
                        .HasColumnType("TEXT");

                    b.Property<string>("ImmediateLatency")
                        .HasColumnType("TEXT");

                    b.Property<string>("ImmediateTST")
                        .HasColumnType("TEXT");

                    b.Property<string>("ImmediateWake")
                        .HasColumnType("TEXT");

                    b.HasKey("ParticipantID");

                    b.ToTable("SleepQuestions");
                });

            modelBuilder.Entity("SDCode.Web.Models.Data.StanfordDbModel", b =>
                {
                    b.Property<string>("ParticipantID")
                        .HasColumnType("TEXT");

                    b.Property<int?>("Delayed")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("DelayedUtc")
                        .HasColumnType("TEXT");

                    b.Property<int?>("Followup")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("FollowupUtc")
                        .HasColumnType("TEXT");

                    b.Property<int?>("Immediate")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("ImmediateUtc")
                        .HasColumnType("TEXT");

                    b.HasKey("ParticipantID");

                    b.ToTable("Stanfords");
                });

            modelBuilder.Entity("SDCode.Web.Models.NeglectedImageModel", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Index")
                        .HasColumnType("TEXT");

                    b.Property<string>("ParticipantID")
                        .HasColumnType("TEXT");

                    b.Property<string>("PhaseName")
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.ToTable("NeglectedImages");
                });

            modelBuilder.Entity("SDCode.Web.Models.ObscuredImageModel", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Index")
                        .HasColumnType("TEXT");

                    b.Property<string>("ParticipantID")
                        .HasColumnType("TEXT");

                    b.Property<string>("PhaseName")
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.ToTable("ObscuredImages");
                });

            modelBuilder.Entity("SDCode.Web.Models.PhaseImageModel", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Index")
                        .HasColumnType("TEXT");

                    b.Property<string>("ParticipantID")
                        .HasColumnType("TEXT");

                    b.Property<string>("PhaseName")
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.ToTable("PhaseImages");
                });
#pragma warning restore 612, 618
        }
    }
}
