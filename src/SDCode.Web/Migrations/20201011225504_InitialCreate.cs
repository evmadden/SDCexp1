using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SDCode.Web.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Consents",
                columns: table => new
                {
                    ParticipantID = table.Column<string>(nullable: false),
                    InfoSheet = table.Column<bool>(nullable: false),
                    Withdraw = table.Column<bool>(nullable: false),
                    NPSDisorder = table.Column<bool>(nullable: false),
                    ADHD = table.Column<bool>(nullable: false),
                    HeadInjury = table.Column<bool>(nullable: false),
                    NormalVision = table.Column<bool>(nullable: false),
                    VisionProblems = table.Column<bool>(nullable: false),
                    AltShifts = table.Column<bool>(nullable: false),
                    DataProtection = table.Column<bool>(nullable: false),
                    AgreeParticipate = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consents", x => x.ParticipantID);
                });

            migrationBuilder.CreateTable(
                name: "Demographics",
                columns: table => new
                {
                    ParticipantID = table.Column<string>(nullable: false),
                    Sex = table.Column<int>(nullable: true),
                    Age = table.Column<string>(nullable: true),
                    YearStudy = table.Column<string>(nullable: true),
                    Handed = table.Column<int>(nullable: true),
                    Impairments = table.Column<bool>(nullable: true),
                    Glasses = table.Column<bool>(nullable: true),
                    Language = table.Column<string>(nullable: true),
                    Bilingual = table.Column<string>(nullable: true),
                    CurrentCountry = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Demographics", x => x.ParticipantID);
                });

            migrationBuilder.CreateTable(
                name: "Epworths",
                columns: table => new
                {
                    ParticipantID = table.Column<string>(nullable: false),
                    Reading = table.Column<int>(nullable: true),
                    TV = table.Column<int>(nullable: true),
                    PublicPlace = table.Column<int>(nullable: true),
                    PassengerCar = table.Column<int>(nullable: true),
                    Afternoon = table.Column<int>(nullable: true),
                    Talking = table.Column<int>(nullable: true),
                    Lunch = table.Column<int>(nullable: true),
                    Traffic = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Epworths", x => x.ParticipantID);
                });

            migrationBuilder.CreateTable(
                name: "NeglectedImages",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ParticipantID = table.Column<string>(nullable: true),
                    PhaseName = table.Column<string>(nullable: true),
                    Index = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NeglectedImages", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ObscuredImages",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ParticipantID = table.Column<string>(nullable: true),
                    PhaseName = table.Column<string>(nullable: true),
                    Index = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObscuredImages", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PhaseImages",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ParticipantID = table.Column<string>(nullable: true),
                    PhaseName = table.Column<string>(nullable: true),
                    Index = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhaseImages", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PSQIs",
                columns: table => new
                {
                    ParticipantID = table.Column<string>(nullable: false),
                    MonthBed = table.Column<string>(nullable: true),
                    MonthLatency = table.Column<string>(nullable: true),
                    MonthWake = table.Column<string>(nullable: true),
                    TotalHours = table.Column<string>(nullable: true),
                    TotalMinutes = table.Column<string>(nullable: true),
                    No30Min = table.Column<int>(nullable: true),
                    WASO = table.Column<int>(nullable: true),
                    Bathroom = table.Column<int>(nullable: true),
                    Breathing = table.Column<int>(nullable: true),
                    Snoring = table.Column<int>(nullable: true),
                    Hot = table.Column<int>(nullable: true),
                    Cold = table.Column<int>(nullable: true),
                    Dreams = table.Column<int>(nullable: true),
                    Pain = table.Column<int>(nullable: true),
                    OtherFrequency = table.Column<int>(nullable: true),
                    OtherDescribe = table.Column<string>(nullable: true),
                    SleepQuality = table.Column<int>(nullable: true),
                    Medication = table.Column<int>(nullable: true),
                    Sleepiness = table.Column<int>(nullable: true),
                    Enthusiasm = table.Column<int>(nullable: true),
                    BedPartner = table.Column<int>(nullable: true),
                    PartSnore = table.Column<int>(nullable: true),
                    BreathPause = table.Column<int>(nullable: true),
                    Legs = table.Column<int>(nullable: true),
                    Disorientation = table.Column<int>(nullable: true),
                    OtherRestless = table.Column<int>(nullable: true),
                    OtherRestDescribe = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PSQIs", x => x.ParticipantID);
                });

            migrationBuilder.CreateTable(
                name: "ResponseDatas",
                columns: table => new
                {
                    ParticipantID = table.Column<string>(nullable: false),
                    TestName = table.Column<string>(nullable: false),
                    SessionID = table.Column<Guid>(nullable: false),
                    Image = table.Column<string>(nullable: false),
                    Congruency = table.Column<int>(nullable: false),
                    Context = table.Column<int>(nullable: false),
                    Judgement = table.Column<int>(nullable: false),
                    Confidence = table.Column<int>(nullable: false),
                    ReactionTime = table.Column<long>(nullable: false),
                    Feedback = table.Column<int>(nullable: false),
                    WhenUtc = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResponseDatas", x => new { x.SessionID, x.ParticipantID, x.TestName, x.Image });
                });

            migrationBuilder.CreateTable(
                name: "SessionMetas",
                columns: table => new
                {
                    ParticipantID = table.Column<string>(nullable: false),
                    SessionName = table.Column<string>(nullable: false),
                    NeglectedReason = table.Column<string>(nullable: true),
                    ObscuredReason = table.Column<string>(nullable: true),
                    FinishedWhenUtc = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SessionMetas", x => new { x.ParticipantID, x.SessionName });
                });

            migrationBuilder.CreateTable(
                name: "SleepQuestions",
                columns: table => new
                {
                    ParticipantID = table.Column<string>(nullable: false),
                    ImmediateBed = table.Column<string>(nullable: true),
                    ImmediateWake = table.Column<string>(nullable: true),
                    ImmediateLatency = table.Column<string>(nullable: true),
                    ImmediateTST = table.Column<string>(nullable: true),
                    DelayedBed = table.Column<string>(nullable: true),
                    DelayedWake = table.Column<string>(nullable: true),
                    DelayedLatency = table.Column<string>(nullable: true),
                    DelayedTST = table.Column<string>(nullable: true),
                    FollowupBed = table.Column<string>(nullable: true),
                    FollowupWake = table.Column<string>(nullable: true),
                    FollowupLatency = table.Column<string>(nullable: true),
                    FollowupTST = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SleepQuestions", x => x.ParticipantID);
                });

            migrationBuilder.CreateTable(
                name: "Stanfords",
                columns: table => new
                {
                    ParticipantID = table.Column<string>(nullable: false),
                    Immediate = table.Column<int>(nullable: true),
                    ImmediateUtc = table.Column<DateTime>(nullable: true),
                    Delayed = table.Column<int>(nullable: true),
                    DelayedUtc = table.Column<DateTime>(nullable: true),
                    Followup = table.Column<int>(nullable: true),
                    FollowupUtc = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stanfords", x => x.ParticipantID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Consents");

            migrationBuilder.DropTable(
                name: "Demographics");

            migrationBuilder.DropTable(
                name: "Epworths");

            migrationBuilder.DropTable(
                name: "NeglectedImages");

            migrationBuilder.DropTable(
                name: "ObscuredImages");

            migrationBuilder.DropTable(
                name: "PhaseImages");

            migrationBuilder.DropTable(
                name: "PSQIs");

            migrationBuilder.DropTable(
                name: "ResponseDatas");

            migrationBuilder.DropTable(
                name: "SessionMetas");

            migrationBuilder.DropTable(
                name: "SleepQuestions");

            migrationBuilder.DropTable(
                name: "Stanfords");
        }
    }
}
