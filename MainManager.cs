using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading;
using InputIOManager;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
	private enum Fonts
	{
		BubblegumSans = 0,
		D3Streetism = 1,
		UNUSED = 2,
		Uzura = 3,
		BalsamiqSans = 4,
		ONEMobilePOP = 5
	}

	public enum Directions
	{
		Up = 0,
		Down = 1,
		Left = 2,
		Right = 3,
		Confirm = 4,
		Cancel = 5,
		Switch = 6,
		Select = 7,
		Start = 8,
		Help = 9
	}

	public enum Animations
	{
		Idle = 0,
		Walk = 1,
		Jump = 2,
		Fall = 3,
		ItemGet = 4,
		Angry = 5,
		Sad = 6,
		Upset = 7,
		Happy = 8,
		Surprized = 9,
		Flustered = 10,
		Hurt = 11,
		Death = 12,
		BattleIdle = 13,
		Sleep = 14,
		Fallen = 15,
		HurtFallen = 16,
		WeakBattleIdle = 17,
		KO = 18,
		PickAction = 19,
		WeakPickAction = 20,
		Woobly = 21,
		HurtWooble = 22,
		Chase = 23,
		Block = 24,
		SleepFallen = 25,
		AirTackle = 26,
		ItemWalk = 27,
		TossItem = 28,
		Sit = 29,
		FakeHurt = 30,
		Dig = 31,
		DigMove = 32
	}

	public enum Transitions
	{
		FadeIn = 0,
		FadeOut = 1,
		LeafIn = 2,
		LeafOut = 3,
		Circle = 4,
		CircleOut = 5,
		HexagonIn = 6,
		HexagonOut = 7
	}

	public enum ItemUsage
	{
		None = 0,
		HPRecover = 1,
		TPRecover = 2,
		HPRecoverAll = 3,
		HPRecoverFull = 4,
		TPRecoverFull = 5,
		HPUP = 6,
		TPUP = 7,
		AttackUp = 8,
		DefenseUp = 9,
		Battle = 10,
		Revive = 11,
		ReviveAll = 12,
		AutoRevive = 13,
		CurePoison = 14,
		CureFreeze = 15,
		CureNumb = 16,
		CureSleep = 17,
		CureParty = 18,
		AddPoison = 19,
		AddSleep = 20,
		AddNumb = 21,
		AddFreeze = 22,
		HPto1 = 23,
		TPto1 = 24,
		HPto1All = 25,
		GradualHP = 26,
		GradualTP = 27,
		GradualHPParty = 28,
		DefUpStat = 29,
		AtkUpStat = 30,
		Sturdy = 31,
		HPUPAll = 32,
		MPUP = 33,
		ChargeUp = 34,
		AtkDownAfter = 35,
		CureFire = 36,
		CureAll = 37,
		TurnNextTurn = 38,
		HPorDamage = 39,
		CurePoisonAll = 40
	}

	public enum LibraryPages
	{
		Discoveries = 0,
		Bestiary = 1,
		Recipes = 2,
		Logbook = 3,
		Map = 4
	}

	public enum BadgeEffects
	{
		None = 0,
		HPUP = 1,
		TPUP = 2,
		HPRecover = 3,
		TPRecover = 4,
		AttackUp = 5,
		DefenseUp = 6,
		LockSkills = 7,
		Detect = 8,
		SpeedUp = 9,
		PoisonRes = 10,
		SleepRes = 11,
		NumbRes = 12,
		FreezeRes = 13,
		PoisonAttack = 14,
		PoisonDefense = 15,
		SleepDefense = 16,
		NumbDefense = 17,
		FreezeDefense = 18,
		AttackMultiply = 19,
		DefenseMuliply = 20,
		LockItems = 21,
		LockRelay = 22,
		LockRelayPass = 23
	}

	public enum BattleCondition
	{
		Freeze = 0,
		Poison = 1,
		Numb = 2,
		Sleep = 3,
		AttackUp = 4,
		DefenseUp = 5,
		AttackDown = 6,
		DefenseDown = 7,
		Topple = 8,
		Flipped = 9,
		Shield = 10,
		Taunted = 11,
		Sturdy = 12,
		GradualHP = 13,
		GradualTP = 14,
		Eaten = 15,
		EventStop = 16,
		Fire = 17,
		Inked = 18,
		Sticky = 19,
		Reflection = 20
	}

	public enum Emoticons
	{
		None = 0,
		Talk = 1,
		QuestionMark = 2,
		Exclamation = 3,
		DotsLong = 4,
		Detector = 5,
		Pushable = 6
	}

	public enum Library
	{
		Discovery = 0,
		Bestiary = 1,
		Recipes = 2,
		Logbook = 3,
		Map = 4
	}

	public enum BattleMaps
	{
		Grasslands1 = 0,
		Desert1 = 1,
		Snakemouth1 = 2,
		AssociationHQ = 3,
		Snakemouth2 = 4,
		Snakemouth3 = 5,
		Snakemouth4 = 6,
		Theater = 7,
		GoldenBattle1 = 8,
		OutskirtsLow = 9,
		Cave0 = 10,
		GoldenSettlementArena = 11,
		GoldenBattle2 = 12,
		GoldenHillsBoss = 13,
		GoldenBattle4 = 14,
		GoldenBattle5 = 15,
		HBsLab = 16,
		Bakery = 17,
		FactoryP = 18,
		FactoryS = 19,
		FactoryC = 20,
		FactoryS2 = 21,
		SandCastle = 22,
		SandCastleIce = 23,
		HideoutBattle = 24,
		HideoutAstotheles = 25,
		SandCastleDark = 26,
		SandCastleDarkIce = 27,
		SandCastleRoof = 28,
		SandCastleBoss = 29,
		AntPalace = 30,
		PlazaAttack = 31,
		BridgeAttack = 32,
		CastleAttack = 33,
		Grasslands2 = 34,
		FarGrasslands = 35,
		Swamplands = 36,
		BarrenLands = 37,
		ChomperCaves = 38,
		ChomperCavesBoss = 39,
		WaspPrison = 40,
		WaspThrone = 41,
		TermiteColiseum = 42,
		AbandonedTent = 43,
		Broodmother = 44,
		KaliShop = 45,
		MetalLake = 46,
		StreamMountain = 47,
		StreamMountainBoss = 48,
		BugariaCommercial = 49,
		CaveOfTrials = 50,
		UpperSnek = 51,
		UpperSnekBoss = 52,
		RubberPrisonInside = 53,
		RubberPrisonOutside = 54,
		WaspKingdomPrison = 55,
		CarminaRoom = 56,
		RubberPrisonBoss = 57,
		MysteryIslandInside = 58,
		GiantLair1 = 59,
		GiantLair2 = 60,
		GiantLairFridge = 61,
		FinalBoss1 = 62,
		FinalBoss2 = 63,
		PitcherPlant = 64,
		AntBridge = 65,
		CardTourney = 66,
		UndergroundBar = 67,
		GiantLair3 = 68,
		GiantLair4 = 69,
		DefiantRootBattle = 70,
		HBsLab2 = 71
	}

	public enum Healing
	{
		Full = 0,
		FullHPOnly = 1,
		TPOnly = 2
	}

	public enum Musics
	{
		Field0 = 0,
		Battle0 = 1,
		Cave0 = 2,
		Battle1 = 3,
		Calm = 4,
		Inside0 = 5,
		LevelUp = 6,
		Theater = 7,
		Tension = 8,
		Chef0 = 9,
		Moth = 10,
		Beetle = 11,
		Title = 12,
		Field1 = 13,
		Inside1 = 14,
		Inside2 = 15,
		Dungeon0 = 16,
		Mothiva = 17,
		Festival = 18,
		Battle2 = 19,
		Venus = 20,
		Field2 = 21,
		Dungeon1 = 22,
		Miniboss = 23,
		Field3 = 24,
		Battle3 = 25,
		Wind = 26,
		Water = 27,
		Dungeon2 = 28,
		Chef1 = 29,
		Chef2 = 30,
		Bee = 31,
		Battle4 = 32,
		Dungeon2b = 33,
		Sad = 34,
		MothivaCalm = 35,
		BeeQ = 36,
		Tension2 = 37,
		MachineHum = 38,
		Battle5 = 39,
		Dungeon3 = 40,
		Dungeon4 = 41,
		Field4 = 42,
		Battle6 = 43,
		Cave1 = 44,
		Sad2 = 45,
		Battle7 = 46,
		Field5 = 47,
		Submarine = 48,
		Termite = 49,
		Breathing = 50,
		TermiteLoop = 51,
		WaspHive = 52,
		Dungeon5 = 53,
		Invasion = 54,
		MetalIsland = 55,
		Bounty = 56,
		Centipede = 57,
		Lab = 58,
		FlyingBee = 59,
		Battle8 = 60,
		Battle9 = 61,
		Alert = 62,
		Giant1 = 63,
		Giant2 = 64,
		Giant3 = 65,
		Final1 = 66,
		Final2 = 67,
		MiteKnight = 68,
		Field6 = 69,
		Pier = 70,
		Credits = 71,
		Tension3 = 72,
		Field7 = 73,
		TeamSnek = 74
	}

	public enum StatBonus
	{
		HP = 0,
		Attack = 1,
		Defense = 2,
		TP = 3,
		MP = 4
	}

	public enum BadgeTypes
	{
		HPPlus = 0,
		TPPlus = 1,
		Detector = 2,
		AttackUp = 3,
		DefenseUp = 4,
		SpeedUp = 5,
		PoisonAttacker = 6,
		PoisonResistance = 7,
		Berserker = 8,
		PoisonDefender = 9,
		Empower = 10,
		DoublePain = 11,
		SleepyResistance = 12,
		MightyPeeble = 13,
		SpikeBod = 14,
		Depower1 = 15,
		Depower2 = 16,
		HPScope = 17,
		BumpAttack = 18,
		SuperBlock = 19,
		FavoriteOne = 20,
		NumbResis = 21,
		PoisonNeedle = 22,
		StrongStart = 23,
		WeakStomach = 24,
		TPSaver = 25,
		ReversePoison = 26,
		EternalPoison = 27,
		Beemerang2 = 28,
		MightierPebble = 29,
		DoublePainReal = 30,
		Empower2 = 31,
		LifeSteal = 32,
		FreezeResistance = 33,
		ShockTrooper = 34,
		FrontSupport = 35,
		BackSupport = 36,
		Emfeeble = 37,
		Emfeeble2 = 38,
		Fortify = 39,
		Fortify2 = 40,
		NumbNeedle = 41,
		EXPBoost = 42,
		StatusBoost = 43,
		PoisonTouch = 44,
		RelayTransfer = 45,
		FrostBite = 46,
		HeavySleeper = 47,
		SecurePouch = 48,
		PowerExchange = 49,
		DefenseExchange = 50,
		Tardigrade = 51,
		ChargeUp = 52,
		ChargeUp2 = 53,
		LeafCloak = 54,
		RandomStart = 55,
		Meditation = 56,
		ElecNeedles = 57,
		NeedlePincer = 58,
		FreezeTime = 59,
		HeavyThrow = 60,
		Reflection = 61,
		Prayer = 62,
		AntlionJaws = 63,
		HappyHeart = 64,
		HappyTP = 65,
		ResistAll = 66,
		TauntPlus = 67,
		MiracleMatter = 68,
		VictoryBuzz = 69,
		HealingBuzz = 70,
		CrazyPrepared = 71,
		HPFunnel = 72,
		HardCharge = 73,
		HealPlus = 74,
		StatusMirror = 75,
		LuckierDay = 76,
		Plating = 77,
		Seedling = 78,
		BerryFinder = 79,
		RoyalDecree = 80,
		BadDream = 81,
		Hook = 82,
		HoloCloak = 83,
		Pip = 84,
		Helper = 85,
		Ambush = 86,
		LastWind = 87,
		ItemRecycle = 88,
		BombPlus = 89,
		HelperBoost = 90
	}

	public enum Skills
	{
		RESERVED = 0,
		RESERVED2 = 1,
		BeeRangMultiHit = 2,
		BeetleTaunt = 3,
		Icefall = 4,
		BeeFly = 5,
		BeetleDig = 6,
		BubbleShield = 7,
		Empower = 8,
		PeebleToss = 9,
		HornDash = 10,
		SecretStash = 11,
		DefenseBreak1 = 12,
		DefenseBreakAll = 13,
		AttackUp = 14,
		DefenseUp = 15,
		NeedleToss = 16,
		BubbleShieldLite = 17,
		HurricaneBeemerang = 18,
		PebbleTossPlus = 19,
		RevivalMassage = 20,
		FrigidCoffin = 21,
		ChargeUpPlus = 22,
		EmpowerPlus = 23,
		NeedlePincer = 24,
		IceRain = 25,
		IceBeemerang = 26,
		IceDrill = 27,
		AttackDown = 28,
		AttackDownPlus = 29,
		DefenseUpPlus = 30,
		IceSphere = 31,
		HeavyStrike = 32,
		Sturdy = 33,
		FieldBeemerang = 34,
		FieldHalt = 35,
		FieldFly = 36,
		FieldHorn = 37,
		FieldDash = 38,
		FieldDig = 39,
		FieldFreeze = 40,
		FieldIcecle = 41,
		FieldShield = 42,
		PepTalk = 43,
		HeavyThrow = 44,
		SharingStash = 45,
		RoyalDecree = 46,
		Cleanse = 47,
		HardCharge = 48,
		FirstDash = 49
	}

	public enum BoardQuests
	{
		None = 0,
		InnQuest = 1,
		ChuckQuest = 2,
		TheaterQuest = 3,
		ToyQuest = 4,
		LadybugQuest = 5,
		UndergroundBar = 6,
		CableCar = 7,
		SeedlingKing = 8,
		FalseMonarch = 9,
		MotherChomper = 10,
		Prologue = 11,
		Chapter1 = 12,
		Chapter2 = 13,
		Chapter3 = 14,
		Chapter4 = 15,
		Chapter5 = 16,
		Chapter6 = 17,
		Crisbee = 18,
		Kut = 19,
		Fry = 20,
		Sandwyrm = 21,
		Butomo = 22,
		PeacockSpider = 23,
		Tanjerin = 24,
		ZaspDoll = 25,
		Leif = 26,
		LibraryantRed = 27,
		Madeleine1 = 28,
		Venus = 29,
		Bee = 30,
		PowerPlant = 31,
		CardGame = 32,
		CicadaBook = 33,
		Vivi = 34,
		MenderQuest = 35,
		Kali = 36,
		Isau = 37,
		Bomby = 38,
		Madeleine2 = 39,
		GenEri = 40,
		Mun = 41,
		BanditHunt = 42,
		ArtBee = 43,
		TermiteLunch = 44,
		Alex = 45,
		Mayor = 46,
		SeedlingHunt = 47,
		Layna = 48,
		Eetl = 49,
		Farmer = 50,
		RizSis = 51,
		Wizard = 52,
		Eremi = 53,
		MoleCricket = 54,
		Maki = 55,
		BadBook = 56,
		WaspTwins = 57,
		BlacksmithGuy = 58,
		WorkerTermite = 59,
		Beetle = 60,
		Rebecca = 61,
		Roach = 62,
		StratosDelilah = 63
	}

	public enum Enemies
	{
		CordycepsAnt = 0,
		Mushroom = 1,
		Spuder = 2,
		Zasp = 3,
		Cactus = 4,
		Pseudoscorpion = 5,
		Thief = 6,
		Bandit = 7,
		ArmoredPoly = 8,
		Seedling = 9,
		FlyingSeedling = 10,
		MakiTutorial = 11,
		MothWeb = 12,
		SpuderReal = 13,
		Sneil = 14,
		Mothiva1 = 15,
		Acornling = 16,
		Weevil = 17,
		MrTester = 18,
		AngryPlant = 19,
		FlyTrap = 20,
		Acolyte = 21,
		AcolyteVine = 22,
		Beetle = 23,
		VenusBoss = 24,
		WaspTrooper = 25,
		WaspBomber = 26,
		WaspDriller = 27,
		WaspHealer = 28,
		Midge = 29,
		Underling = 30,
		Scarlet = 31,
		GoldenSeedling = 32,
		Sandworm = 33,
		Carmina = 34,
		SeedlingKing = 35,
		MidgeBroodmother = 36,
		Plumpling = 37,
		Flowering = 38,
		Burglar = 39,
		BanditLeader = 40,
		MotherChomper = 41,
		Ahoneynation = 42,
		BeeBot = 43,
		BeeTurret = 44,
		ShockWorm = 45,
		BeeBoss = 46,
		MenderBot = 47,
		Abomihoney = 48,
		Scorpion = 49,
		SandWyrm = 50,
		Kali = 51,
		Zombee = 52,
		Zombeetle = 53,
		ZombieRoach = 54,
		PeacockSpider = 55,
		Bloatshroom = 56,
		Krawler = 57,
		Cape = 58,
		SandWall = 59,
		IceWall = 60,
		CursedSkull = 61,
		WaspKingIntermission = 62,
		JumpingSpider = 63,
		MimicSpider = 64,
		LeafbugNinja = 65,
		LeafbugArcher = 66,
		LeafbugClubber = 67,
		SkullCaterpillar = 68,
		Centipede = 69,
		ChomperBrute = 70,
		Mantidfly = 71,
		WaspGeneral = 72,
		WildChomper = 73,
		TermiteSoldier = 74,
		TermiteNasute = 75,
		PrimalWeevil = 76,
		FalseMonarch = 77,
		Mothfly = 78,
		MothflyCluster = 79,
		Ironclad = 80,
		ToeBiter = 81,
		Ruffian = 82,
		Strider = 83,
		DivingSpider = 84,
		Cenn = 85,
		Pisci = 86,
		DeadLanderA = 87,
		DeadLanderB = 88,
		DeadLanderG = 89,
		WaspKing = 90,
		EverlastingKing = 91,
		Maki = 92,
		Kina = 93,
		Yin = 94,
		UltimaxTank = 95,
		Zommoth = 96,
		Fisherman = 97,
		Pitcher = 98,
		SandWyrmTail = 99,
		PisciWall = 100,
		KeyR = 101,
		KeyL = 102,
		Tablet = 103,
		PitcherFlytrap = 104,
		FireKrawler = 105,
		FireWarden = 106,
		FireCape = 107,
		IceKrawler = 108,
		IceWarden = 109,
		TANGYBUG = 110,
		Stratos = 111,
		Delilah = 112,
		HoloVi = 113,
		HoloKabbu = 114,
		HoloLeif = 115
	}

	public enum Areas
	{
		BugariaOutskirts = 0,
		BugariaCity = 1,
		Snakemouth = 2,
		Desert = 3,
		GoldenHills = 4,
		GoldenWay = 5,
		GoldenSettlement = 6,
		BarrenLands = 7,
		FarGrasslands = 8,
		WildGrasslands = 9,
		DefiantRoot = 10,
		SandCastle = 11,
		Beehive = 12,
		HoneyFactory = 13,
		RubberPrison = 14,
		GiantLair = 15,
		MetalLake = 16,
		MetalIsland = 17,
		TermiteCity = 18,
		WaspKingdom = 19,
		BanditHideout = 20,
		StreamMountain = 21,
		ChomperCaves = 22,
		FishingVillage = 23,
		UpperSnakemouth = 24
	}

	public enum Maps
	{
		TestRoom = 0,
		NearSnakemouth = 1,
		OutsideSnakemouth = 2,
		AntTunnels = 3,
		DesertEntrance = 4,
		DesertBadlands = 5,
		DesertBookArea = 6,
		DesertRockFormation = 7,
		DesertTrenchSouth = 8,
		BugariaMainPlaza = 9,
		BugariaCommercial = 10,
		SnakemouthBridgeRoom = 11,
		SnakemouthDoorRoom = 12,
		SnakemouthFallRoom = 13,
		SnakemouthLake = 14,
		SnakemouthEmpty = 15,
		BugariaOutskirtsOutsideCity = 16,
		BugariaOutskitsSnakemouthCorridor1 = 17,
		BugariaOutskirtsSnakemouthCorridor2 = 18,
		SnakemouthUndergrondDoor = 19,
		SnakemouthMushroomPit = 20,
		SnakemouthTreasureRoom = 21,
		SnakemouthUndergroundRightA = 22,
		SnakemouthUndergroundRightB = 23,
		SnakemouthUndergroundLeftA = 24,
		SnakemouthUndergroundLeftB = 25,
		BugariaTheater = 26,
		ChucksAbode = 27,
		BugariaResidential = 28,
		GoldenHillsCableCar = 29,
		UndergroundBar = 30,
		AntPalace1 = 31,
		AntPalace2 = 32,
		AntBridge = 33,
		AntPalaceLibrary = 34,
		GoldenPathTunnel = 35,
		BOGoldenPath = 36,
		AntPalaceWarRoom = 37,
		GoldenHillsPath2 = 38,
		GoldenSettlementEntrance = 39,
		GoldenSettlement1 = 40,
		GoldenSettlement1Night = 41,
		GoldenSettlement2 = 42,
		GoldenSettlement2Night = 43,
		GoldenHillsPath3 = 44,
		GoldenHillsDungeonEntrance = 45,
		GoldenHillsDungeonLeftMain = 46,
		GoldenHillsDungeonCrankLeft = 47,
		GoldenHillsDungeonRightCrank = 48,
		GoldenHillsLowerRightCrank = 49,
		GoldenHillsDungeonLeftCrankHalf = 50,
		GoldenHillsDungeonUpperMain = 51,
		GoldenHillsDungeonUpperSide = 52,
		GoldenHillsDungeonBoss = 53,
		BugariaPier = 54,
		BugariaOutskirtsEast1 = 55,
		BugariaOutskirtsEast2 = 56,
		BOLostSandsEntrance = 57,
		DefiantRoot1 = 58,
		DefiantRootWell = 59,
		DefiantRoot2 = 60,
		DefiantRoot3 = 61,
		BeehiveOutside = 62,
		BeehiveThroneRoom = 63,
		BeehiveScannerRoom = 64,
		GoldenSettlement3 = 65,
		GoldenSettlement3Night = 66,
		BeehiveMainArea = 67,
		HBsLab = 68,
		BeehiveBalcony = 69,
		HoneycombsLab = 70,
		JaunesGallery = 71,
		HoneyFactoryEntrance = 72,
		AntMinesBreakRoom = 73,
		HoneyFactoryWorkerRooms = 74,
		HoneyFactoryCore = 75,
		DesertDREastEntrance = 76,
		DesertFGBorder = 77,
		DesertDRSouthEntrance = 78,
		DesertBadgeAlcove = 79,
		DesertCaravanMap = 80,
		DesertSandPitArea = 81,
		DesertBeforeGH = 82,
		FactoryProcessingFirstRoom = 83,
		FactoryProcessing2 = 84,
		FactoryProcessingPump = 85,
		FactoryProcessingPuzzle1 = 86,
		FactoryProcessingPuzzle2 = 87,
		FactoryProcessingPuzzle3 = 88,
		FactoryProcessingMalbee = 89,
		FactoryStorageMaze = 90,
		FactoryStorageElevator = 91,
		FactoryStorageMiniboss = 92,
		FactoryStorageOverseer = 93,
		MetalIsland1 = 94,
		DesertRoachVillage = 95,
		DesertOasis = 96,
		DesertOasisEntrance = 97,
		DesertWestDunes = 98,
		HideoutEntrance = 99,
		HideoutCell = 100,
		HideoutCentralRoom = 101,
		HideoutLeftA = 102,
		HideoutStairsRoom = 103,
		HideoutGarden = 104,
		HideoutWestStorage = 105,
		HideoutRightA = 106,
		DesertSandCastle = 107,
		DesertMountain = 108,
		DesertTrenchMiddle = 109,
		DesertJumpPuzzle = 110,
		DesertSouthern = 111,
		DesertScorpion = 112,
		DesertEastmost = 113,
		GoldenSMinigame = 114,
		Blank = 115,
		SandCastleEntrance = 116,
		SandCastleSlidePuzzle = 117,
		SandCastleStatueRoom = 118,
		SandCastleBasement = 119,
		SandCastleRoof = 120,
		SandCastleMainRoom = 121,
		SandCastleBossKeyRoom = 122,
		BugariaPlazaAttack = 123,
		BugariaBridgeAttack = 124,
		BugariaCastleAttack = 125,
		SandCastlePressurePuzzle = 126,
		SandCastleRockRoom = 127,
		SandCastleBossRoom = 128,
		SandCastleTreasureRoom = 129,
		BugariaAssociationAttack = 130,
		MetalIsland2 = 131,
		StreamMountain1 = 132,
		StreamMountain2 = 133,
		StreamMountain3 = 134,
		FGCave = 135,
		SeedlingHaven = 136,
		FarGrasslands1 = 137,
		FarGrasslandsOutsideCave = 138,
		FarGrasslandsWizard = 139,
		FarGrasslands2 = 140,
		FarGrasslandsLake = 141,
		FarGrasslandsOutsideVillage = 142,
		FarGrasslands3 = 143,
		FishingVillage = 144,
		SwamplandsEntrance = 145,
		FGOutsideSwamplands = 146,
		WaspKingdomOutside = 147,
		Swamplands2 = 148,
		BarrenLandsEntrance = 149,
		BarrenLandsCD = 150,
		Swamplands3 = 151,
		SwamplandsBridge = 152,
		FarGrasslands4 = 153,
		SwamplandsBoss = 154,
		ChomperCave1 = 155,
		ChomperCaves2 = 156,
		ChomperCaves3 = 157,
		Swamplands4 = 158,
		Swamplands5 = 159,
		Swamplands6 = 160,
		Swamplands7 = 161,
		Swamplands8 = 162,
		WaspKingdom1 = 163,
		WaspKingdom2 = 164,
		WaspKingdom3 = 165,
		WaspKingdom4 = 166,
		WaspKingdom5 = 167,
		WaspKingdomPrison = 168,
		WaspKingdomJayde = 169,
		WaspKingdomMainHall = 170,
		WaspKingdomThrone = 171,
		WaspKingdomQueen = 172,
		TermiteOutside = 173,
		TermiteMainPlaza = 174,
		TermiteRoyalChamber = 175,
		TermiteIndustrial = 176,
		TermitePier = 177,
		TermiteColiseum1 = 178,
		TermiteColiseum2 = 179,
		BarrenLandsBeefly = 180,
		BarrenLandsAntTunnel = 181,
		BarrenLandsMiniboss = 182,
		MetalLake = 183,
		SnakemouthTop = 184,
		CaveOfTrials = 185,
		WizardTowerBasement = 186,
		WizardTowerStairs = 187,
		WizardTowerAttic = 188,
		BarrenLandsPinkSpider = 189,
		BarrenLandsTanks = 190,
		BarrenLandsMushrooms = 191,
		AbandonedCity = 192,
		BarrenLandsPumpkins = 193,
		BarrenLandsCloud = 194,
		BarrenLandsRock = 195,
		AbandonedCityTent = 196,
		PowerPlant = 197,
		BroodmotherLair = 198,
		BarrenLandsSideGPT = 199,
		GoldenPathTunnel2 = 200,
		FGClearing = 201,
		StreamMountain4 = 202,
		GoldenPitcher1 = 203,
		StreamMountain5 = 204,
		GoldenPitcher2 = 205,
		MysteryIsland = 206,
		MysteryIslandInside = 207,
		UpperSnekEntrance = 208,
		UpperSnekTransition = 209,
		UpperSnekSwitchPuzzle = 210,
		UpperSnekBeforeBoss = 211,
		UpperSnekPressurePlateRoom = 212,
		UpperSnekBossRoom = 213,
		UpperSnekMiddleRoom = 214,
		UpperSnekPlatformRoom = 215,
		UpperSnekRiverPuzzle = 216,
		UpperSnekGeizerRoom = 217,
		RubberPrisonPier = 218,
		RubberPrisonCheckpointCorridor = 219,
		RubberPrisonSpikeRoom = 220,
		RubberPrisonCells1 = 221,
		RubberPrisonCells2 = 222,
		RubberPrisonLibrary = 223,
		RubberPrisonCafeteria = 224,
		RubberPrisonGym = 225,
		RubberPrisonSecurity = 226,
		HermitCave = 227,
		MetalIslandAuditorium = 228,
		RubberPrisonOffice = 229,
		RubberPrisonThirdFloor = 230,
		RubberPrisonGiantLairBridge = 231,
		GiantLairEntrance = 232,
		GiantLairDeadLands1 = 233,
		GiantLairDeadLands2 = 234,
		GiantLairFridgeOutside = 235,
		GiantLairFridgeInside = 236,
		GiantLairRoachVillage = 237,
		GiantLairSaplingPlains = 238,
		PitcherPlantArena = 239,
		BugariaEndPlaza = 240,
		BugariaEndBridge = 241,
		BugariaEndThrone = 242,
		WaspKingdomDrillRoom = 243,
		GiantLairBeforeBoss = 244,
		GiantLairBeforeBoss2 = 245
	}

	public enum Items
	{
		None = -1,
		CrunchyLeaf = 0,
		HoneyDrop = 1,
		VitalitySeed = 2,
		GenerousSeed = 3,
		VigorousSeed = 4,
		BurlySeed = 5,
		MoneySmall = 6,
		MoneyMedium = 7,
		Mistake = 8,
		CookedLeaf = 9,
		HoneydLeaf = 10,
		MagicDrops = 11,
		ClearWater = 12,
		Mushroom = 13,
		CookedShroom = 14,
		GlazedShroom = 15,
		LeafSalad = 16,
		AphidEgg = 17,
		Omelet = 18,
		HeartyBreakfast = 19,
		GlazedHoney = 20,
		HoneyShroom = 21,
		HoneyDanger = 22,
		HardSeed = 23,
		DotBall = 24,
		GBugRangerPlushie = 25,
		DangerShroom = 26,
		ExplorerPermit = 27,
		CookedDanger = 28,
		SweetCrystal = 29,
		SpicyBomb = 30,
		PoisonBomb = 31,
		KingDinner = 32,
		MushroomStick = 33,
		RoastBerry = 34,
		Abomihoney = 35,
		ClearBomb = 36,
		AntCompass = 37,
		ChomperSeed = 38,
		BerryJuice = 39,
		NumbDart = 40,
		Map = 41,
		Ice = 42,
		Battery = 43,
		FrostBomb = 44,
		NumbBomb = 45,
		SleepBomb = 46,
		ShavedIce = 47,
		AphidMilk = 48,
		HoneyIceCream = 49,
		HoneyMilk = 50,
		IceCream = 51,
		LoreBook = 52,
		FrozenSalad = 53,
		FlowerKey = 54,
		OfferingA = 55,
		OfferingB = 56,
		MothivaDoll = 57,
		GHCrank = 58,
		CrankHalfA = 59,
		MainCrank = 60,
		CrankHalfB = 61,
		Abombhoney = 62,
		SpyData = 63,
		PoisonSpud = 64,
		BakedYam = 65,
		FrenchFries = 66,
		BurlyChips = 67,
		FlourBag = 68,
		YamBread = 69,
		SpicyCandy = 70,
		BurlyCandy = 71,
		DryBread = 72,
		NutCake = 73,
		PoisonCake = 74,
		ShockCandy = 75,
		PlainTea = 76,
		TangyBerry = 77,
		TangyJam = 78,
		TangyJuice = 79,
		SpicyTea = 80,
		BurlyTea = 81,
		FrostPie = 82,
		HeartBerry = 83,
		BellBerry = 84,
		TangyPie = 85,
		Donut = 86,
		TangyCarpaccio = 87,
		PoisonDart = 88,
		BedBug = 89,
		JellyBean = 90,
		CookedJellyBean = 91,
		DesertKey = 92,
		QuestBook = 93,
		ChomperRibbon = 94,
		BeeCard = 95,
		ShockShroom = 96,
		ShellOil = 97,
		CrimsonOre = 98,
		BeeHat = 99,
		BlankCard = 100,
		SpadeCard = 101,
		DualCard = 102,
		TriadCard = 103,
		FullCard = 104,
		YinKey = 105,
		YangKey = 106,
		LonglegSummoner = 107,
		StolenSilk = 108,
		TrialKey = 109,
		GameToken = 110,
		HideoutKey = 111,
		TanjerinHorn = 112,
		SandCastleKey = 113,
		SandCastleSmallKey = 114,
		SandCastleBossKey = 115,
		SnakemouthKey = 116,
		BombyHat = 117,
		SeedlingCrystal = 118,
		WaspKey = 119,
		JaydeStew = 120,
		BlackCherry = 121,
		CherryPie = 122,
		MiracleShake = 123,
		BerrySmoothie = 124,
		Squash = 125,
		SquashCandy = 126,
		SophiePetal = 127,
		SucculentCookie = 128,
		Pudding = 129,
		SquashSoda = 130,
		HPPotion = 131,
		ATKPotion = 132,
		DEFPotion = 133,
		TPPotion = 134,
		SuperHPPotion = 135,
		SuperTPPotion = 136,
		MPPotion = 137,
		ShadyNote = 138,
		BlackPaint = 139,
		RedPaint = 140,
		DefRootCloak = 141,
		AntToy = 142,
		MIToy = 143,
		MushroomCandy = 144,
		TermiteLunch = 145,
		CrownCrystal = 146,
		DrowsyCake = 147,
		LeafCroisant = 148,
		Package = 149,
		RainbowCrystal = 150,
		FangCrystal = 151,
		CherryBomb = 152,
		SquashPuree = 153,
		MiteBurg = 154,
		ProteinShake = 155,
		Guarana = 156,
		SmallGear = 157,
		MedGear = 158,
		BigGear = 159,
		LabCard = 160,
		PrisonKey = 161,
		HustleSeed = 162,
		PrisonBookA = 163,
		PrisonBookB = 164,
		PrisonBookC = 165,
		PrisonBookD = 166,
		LeafUmbrella = 167,
		PoisonRibbon = 168,
		NumbRibbon = 169,
		SleepRibbon = 170,
		BigMistake = 171,
		BurlyBomb = 172,
		BerryShake = 173,
		BadBook = 174,
		MechArm = 175,
		PlatinumCard = 176,
		Coffee = 177,
		CoffeeCandy = 178,
		SquashPie = 179,
		PlumplingPie = 180,
		DangerDish = 181,
		HoneyPancake = 182,
		FlameRock = 183,
		CardTrophy = 184,
		RedRibbon = 185,
		MoneyBig = 186
	}

	public enum AnimIDs
	{
		None = 0,
		Bee = 1,
		Beetle = 2,
		Moth = 3,
		LadybugKnight = 4,
		ButterflyGirl = 5,
		MessengerAnt = 6,
		MinerAnt1 = 7,
		SleepyMinerAnt = 8,
		OverseerMinerAnt = 9,
		BeeMinerAnt = 10,
		Mothiva = 11,
		FuzzyMoth = 12,
		MaskedMoth = 13,
		CordycepsAnt = 14,
		Mushroom = 15,
		TestCube = 16,
		TestButton = 17,
		OldBoringBeetle = 18,
		FlyChef = 19,
		Jaune = 20,
		Zasp = 21,
		DrNeolith = 22,
		Cactus = 23,
		TestSign = 24,
		SavePoint = 25,
		DigMound = 26,
		AntInnkeeper = 27,
		AntKid = 28,
		MothKid = 29,
		Pillbug = 30,
		OGBeetle = 31,
		GenericAnt = 32,
		AntSoldier1 = 33,
		Samira = 34,
		BadgeBeetle = 35,
		BounceShroom = 36,
		SwitchCrystal = 37,
		SodaCap = 38,
		Armorpillar = 39,
		AncientPressurePlate = 40,
		PushRock = 41,
		CoilyVine = 42,
		Spuder = 43,
		CrystalBerry = 44,
		Seedling = 45,
		Kina = 46,
		Maki = 47,
		Gen = 48,
		Eri = 49,
		ShielderAnt = 50,
		TrappedMoth = 51,
		MantisAccountant = 52,
		AncientPlatform = 53,
		LongAncientPlatform = 54,
		BigCrystalSwitch = 55,
		SmallAncientPlatform = 56,
		Chubee = 57,
		Thief = 58,
		Bandit = 59,
		SneilEnemy = 60,
		Crickerly = 61,
		CaravanSmolBug = 62,
		LadybugGirl = 63,
		LadybugBoy = 64,
		Mar = 65,
		Genow = 66,
		Trist = 67,
		OrangeBeetle = 68,
		Acornling = 69,
		Weevil = 70,
		FlyTrapPlatform = 71,
		CommonSticcBug = 72,
		KungFuMantis = 73,
		MrTester = 74,
		AngryPlant = 75,
		OmaBug = 76,
		Madeleine = 77,
		FlyTrap = 78,
		MantisAcolyte = 79,
		CurledVineGround = 80,
		Venus = 81,
		Bae = 82,
		Barkeeper = 83,
		EdgeBeetle = 84,
		OrangeBarBug = 85,
		Shades = 86,
		RoyalGuard = 87,
		MosquitoGal = 88,
		StickShopkeeper = 89,
		BeetleInnkeeper = 90,
		ShyBee = 91,
		ArrogantBee = 92,
		BeeKid = 93,
		ContestBee = 94,
		AntSoldier2 = 95,
		AntCapitain = 96,
		AntQueen = 97,
		Libraryant1 = 98,
		Libraryant2 = 99,
		WaspTrooper = 100,
		ScrewSwitch = 101,
		Aphid = 102,
		WoolyAphid = 103,
		Cochinael = 104,
		FatMinerAnt = 105,
		WoodenSwitch = 106,
		WoodenPlatform = 107,
		MantisBarkeeper = 108,
		SticcGirl = 109,
		LadybugGHRed = 110,
		LadybugGHOrange = 111,
		SleepyStickBug = 112,
		MantisChef = 113,
		FarmerAnt1 = 114,
		FarmerAnt2 = 115,
		FarmerAnt3 = 116,
		BeeGuard = 117,
		ProfHoneycomb = 118,
		Hawk = 119,
		Mayor = 120,
		Midge = 121,
		VenusGuardian = 122,
		OldAnt = 123,
		GenericAnt2 = 124,
		ButterflyGuy = 125,
		FortuneTeller = 126,
		Stratos = 127,
		Delilah = 128,
		BlackStickBug = 129,
		OfferingAltar = 130,
		Underling = 131,
		Scarlet = 132,
		Tanjerin = 133,
		TanjerinHorn = 134,
		GoldenSeedling = 135,
		Pseudoscorp = 136,
		Sandworm = 137,
		Carmina = 138,
		Burglar = 139,
		MotherChomper = 140,
		Sirfy = 141,
		Isau = 142,
		CricketShopkeeper = 143,
		FakeLegStickBug = 144,
		DragonflyGuy = 145,
		CricketGuy1 = 146,
		CricketGuy2 = 147,
		FortuneSister = 148,
		BeetleMerchantBag = 149,
		MothMerchantBag = 150,
		ButterflyGuyDR = 151,
		StickBugDR = 152,
		TermiteGirl = 153,
		WaspInnkeeper = 154,
		MuseumMoth = 155,
		BumbleBarkeeper = 156,
		CoolMosquito = 157,
		RoachShaman = 158,
		QueenBee = 159,
		DocHB = 160,
		HBAssistant = 161,
		FashionBee = 162,
		ToughBee = 163,
		SmugBeeKid = 164,
		TolBee = 165,
		EdgeArtBee = 166,
		FashionMoth = 167,
		GuideBee = 168,
		GiftShopBee = 169,
		ChompyChan = 170,
		BeeOverseer = 171,
		WorkerBee1 = 172,
		WorkerBee2 = 173,
		Maldibee = 174,
		GlassesMantis = 175,
		BeeBot = 176,
		SmallMothGuy = 177,
		Abomihoney = 178,
		Turret = 179,
		Denmuki = 180,
		BeeBoss = 181,
		WaspScout = 182,
		BigSailorGuy = 183,
		SmallSailorGuy = 184,
		SailorGirl = 185,
		PierMantisCook = 186,
		FatScubaAnt = 187,
		ThinScubaAnt = 188,
		Ahoneynation = 189,
		Menderbot = 190,
		ElectroPlatform = 191,
		HoneyGrate = 192,
		MadeleineButler = 193,
		BLANK = 194,
		NerdyCicada = 195,
		Astotheles = 196,
		Eophi = 197,
		SmallMinerAnt = 198,
		FarmerMinerAnt = 199,
		ButterflyCMaster = 200,
		ArcadeTermite = 201,
		Scorpion = 202,
		BankerAnt = 203,
		Bulkbee = 204,
		MantisMerchant = 205,
		Krawler = 206,
		Cape = 207,
		CursedSkull = 208,
		IcePillarObj = 209,
		OldMoth = 210,
		PinkMoth = 211,
		Watcher = 212,
		icepillar = 213,
		SandPillar = 214,
		WaspKing = 215,
		Eremi = 216,
		RollingRock = 217,
		Abombhoney = 218,
		WormBeetle = 219,
		SnailBeetle = 220,
		Kali = 221,
		Bomby = 222,
		DragonflyLady = 223,
		CardGuard = 224,
		HotelRecep = 225,
		MIMosquito = 226,
		Alex = 227,
		Butomo = 228,
		HaughtyAnt = 229,
		SeedlingKing = 230,
		Yin = 231,
		Flowering = 232,
		Plumpling = 233,
		JumpingSpider = 234,
		LeafbugNinja = 235,
		LeafbugArcher = 236,
		LeafbugClubber = 237,
		Patton = 238,
		MimicSpider = 239,
		SkullCaterpillar = 240,
		LongLegs = 241,
		Centipede = 242,
		Lilypad = 243,
		ChomperBrute = 244,
		WoodenPPlate = 245,
		RopePlatform = 246,
		Mantidfly = 247,
		WaspDriller = 248,
		WaspBomber = 249,
		Jayde = 250,
		WaspGeneral = 251,
		WaspQueen = 252,
		Futes = 253,
		HungryAnt = 254,
		TraitorWasp = 255,
		WildChomper = 256,
		Submarine = 257,
		TermiteScientist = 258,
		GazingTermite = 259,
		EdgeTermite = 260,
		SwordTermite = 261,
		TermiteInnkeeper = 262,
		TermiteSoldier = 263,
		TermiteNasute = 264,
		Zombee = 265,
		Zombeetle = 266,
		Bloatshroom = 267,
		TermiteShopkeeper = 268,
		TermiteBarkeeper = 269,
		TermiteQuestgiver = 270,
		TermiteKing = 271,
		TermiteQueen = 272,
		ButterflyGirl2 = 273,
		TiredLadybug = 274,
		StickBug2 = 275,
		WeirdTermite = 276,
		ShortTermite = 277,
		ShortTermite2 = 278,
		TermiteGirl2 = 279,
		BandanaTermite = 280,
		CherryMerchant = 281,
		WorkingTermite = 282,
		PierTermite = 283,
		PoorTermite = 284,
		PoorTermiteSister = 285,
		ScarfTermite = 286,
		TermiteCashier = 287,
		TrialRoach = 288,
		ScientistRoach = 289,
		PrimalWeevil = 290,
		TangySeller = 291,
		ToyMerchant = 292,
		Wizard = 293,
		ColiseumTermite = 294,
		RoyalTermiteWorker = 295,
		SittingAnt = 296,
		TermiteOwner = 297,
		TermiteScientist2 = 298,
		Gachapon = 299,
		MaskedMF1 = 300,
		MaskedMF2 = 301,
		MaskedMF3 = 302,
		MaskedMF4 = 303,
		FalseMonarch = 304,
		Mothfly = 305,
		MothflyCluster = 306,
		Ironclad = 307,
		ToeBiter = 308,
		MidgeBroodmother = 309,
		FLMinerAnt = 310,
		FGMinerAnt = 311,
		RPMinerAnt = 312,
		CloakAnt = 313,
		CicadaGuy2 = 314,
		StickbugGuy2 = 315,
		VeilBee = 316,
		BumblebeeGirl = 317,
		SmallBeetleGuy = 318,
		LeafAnt = 319,
		BookAnt = 320,
		SmugWeakGuy = 321,
		Layna = 322,
		LaynaPet = 323,
		BrotherTermite = 324,
		SeedlingTermite = 325,
		JojoTermite = 326,
		WrappedTermite = 327,
		Strider = 328,
		DivingSpider = 329,
		Cenn = 330,
		Pisci = 331,
		Ruffian = 332,
		SandWyrm = 333,
		SandWyrmTail = 334,
		StagBeetle = 335,
		PisciWall = 336,
		Riz = 337,
		RizSister = 338,
		RizGrandpa = 339,
		BackerStickBug = 340,
		Zommoth = 341,
		FitTermite = 342,
		TermiteNasuteSmol = 343,
		PeacockSpider = 344,
		PrisonGate = 345,
		PrisonGateLocal = 346,
		SteelSwitch = 347,
		MoleCricketGuy = 348,
		MoleCricketGirl = 349,
		UltimaxTank = 350,
		Kenny = 351,
		DeadLanderA = 352,
		DeadLanderB = 353,
		DeadLanderC = 354,
		RoachElder = 355,
		BuffRoachGuy = 356,
		WalkingRoachGuy = 357,
		BeeButler = 358,
		BigBeetle = 359,
		CowboyStickbug = 360,
		RichAnt = 361,
		RichKid = 362,
		ShadyLadybug = 363,
		RichMoth = 364,
		TeaMoth = 365,
		MaskStickbug = 366,
		WindUp = 367,
		PierGirl = 368,
		KeyR = 369,
		KeyL = 370,
		Tablet = 371,
		EverlastingKing = 372,
		YinMoth = 373,
		Pitcher = 374,
		PitcherSummon = 375,
		Poppy = 376,
		BookWaspGuy = 377,
		CardBumblebee = 378,
		Effo = 379,
		DragonflyBlacksmith = 380,
		WaspTwinA = 381,
		WaspTwinB = 382,
		WaspBoyfriend = 383,
		BombMaster = 384,
		BombFanatic = 385,
		OldSailorMantis = 386,
		CardWasp = 387,
		CardBumble = 388,
		CardBee = 389,
		CardJudge = 390,
		Comfy = 391,
		Soto = 392,
		RoachGirl = 393,
		RichMantis = 394,
		RichStagBeetle = 395,
		RichRhinoBeetle = 396,
		CardStickbug = 397,
		MasterSlice = 398,
		BounceShroom2 = 399,
		FireKrawler = 400,
		FireCape = 401,
		FireWarden = 402,
		TermiteGirl3 = 403,
		Cerise = 404,
		WoodenPPlate2 = 405,
		RecipeGuy = 406,
		Roy = 407
	}

	private enum Commands
	{
		String = 0,
		Var = 1,
		Anstring = 2,
		Checkitem = 3,
		Prompt = 4,
		Getitem = 5,
		Line = 6,
		Next = 7,
		End = 8,
		Break = 9,
		Blank = 10,
		Lock = 11,
		Cancelaction = 12,
		Center = 13,
		Halfline = 14,
		Stopskip = 15,
		Noskip = 16,
		Hide = 17,
		Rainbow = 18,
		Shaky = 19,
		Wavy = 20,
		Glitchy = 21,
		Buffer = 22,
		Overfollower = 23,
		Choicewave = 24,
		Spd = 25,
		Speed = 26,
		Color = 27,
		Anim = 28,
		Sort = 29,
		Save = 30,
		Parent = 31,
		Tail = 32,
		Flag = 33,
		Checkmoney = 34,
		Checkinvqtd = 35,
		Additemtoss = 36,
		Additem = 37,
		Money = 38,
		Goto = 39,
		Currency = 40,
		Kill = 41,
		Boxstyle = 42,
		Pickitem = 43,
		Checktrue = 44,
		Removeitem = 45,
		Getstorage = 46,
		Button = 47,
		Movewait = 48,
		Move = 49,
		Forcewait = 50,
		Wait = 51,
		Face = 52,
		Camtarget = 53,
		Flip = 54,
		Warp = 55,
		Transfer = 56,
		NumberPrompt = 57,
		Common = 58,
		Checkvar = 59,
		Fwait = 60,
		FadeIn = 61,
		FadeOut = 62,
		LeafIn = 63,
		LeafOut = 64,
		Align = 65,
		Discovery = 66,
		Size = 67,
		Minibubble = 68,
		Destroyminibubble = 69,
		Halt = 70,
		Waitminibubble = 71,
		Showmoney = 72,
		Hidemoney = 73,
		Innsleep = 74,
		Event = 75,
		Checkregional = 76,
		Checkflag = 77,
		Regionalflag = 78,
		Createitem = 79,
		Addboard = 80,
		Openboard = 81,
		Camangle = 82,
		Camoffset = 83,
		Completequest = 84,
		Activateselectedquest = 85,
		Resetcamera = 86,
		Quarterline = 87,
		Questprompt = 88,
		Heal = 89,
		Lockmovement = 90,
		Teleportparty = 91,
		Flagvalue = 92,
		Removebadgeshop = 93,
		Savecamera = 94,
		Loadcamera = 95,
		Camspeed = 96,
		Stars = 97,
		Giveitem = 98,
		Tailextra = 99,
		Setvar = 100,
		Gettail = 101,
		Jump = 102,
		Position = 103,
		Hidespeed = 104,
		Breakflag = 105,
		Bleep = 106,
		Exitgame = 107,
		Openpause = 108,
		Font = 109,
		Exp = 110,
		Icon = 111,
		Dropshadow = 112,
		Level = 113,
		Clonestring = 114,
		Pauseline = 115,
		Addfollower = 116,
		Fadeletter = 117,
		Setprize = 118,
		Libraryline = 119,
		Shopline = 120,
		Shakecamera = 121,
		Removemaplimits = 122,
		Resetmaplimits = 123,
		Music = 124,
		Sound = 125,
		Destroydescbox = 126,
		Resetregion = 127,
		Mapflag = 128,
		Checkmapflag = 129,
		Kinematicplayer = 130,
		Removefollower = 131,
		Shoppool = 132,
		Optiontovar = 133,
		Librarybreak = 134,
		Setbreak = 135,
		Librarybook = 136,
		Area = 137,
		Sstring = 138,
		Menu = 139,
		Battle = 140,
		Removestat = 141,
		Addstat = 142,
		Checkpos = 143,
		Triui = 144,
		Backline = 145,
		Cardbattle = 146,
		Boxspeed = 147,
		Battlewon = 148,
		Switch = 149,
		Checkminibubble = 150,
		Breakend = 151,
		DungeonGame = 152,
		BeeGame = 153,
		Copyvar = 154,
		Addvar = 155,
		Showtokens = 156,
		Call = 157,
		Igcolmove = 158,
		PartyGame = 159,
		LetterPrompt = 160,
		Define = 161,
		Loadmap = 162,
		Checkanim = 163,
		Camlimit = 164,
		Waitcn = 165,
		Faketail = 166,
		Unpauseline = 167,
		Cberrytotal = 168,
		Medaltotal = 169,
		Single = 170,
		Tab = 171,
		Singlebreak = 172,
		Librarysize = 173,
		Mothfly = 174,
		Chapterintro = 175,
		Backbox = 176,
		Testdiag = 177,
		Lore = 178,
		Follow = 179,
		Transitionsort = 180,
		Moveahead = 181,
		Addquest = 182,
		Textangle = 183,
		Particle = 184,
		Itemname = 185,
		Addprize = 186,
		Entityalive = 187,
		Scorecheck = 188,
		Fademusic = 189,
		Limit = 190,
		Termacadecheck = 191,
		Removeitemat = 192,
		Unpausesize = 193,
		Alwaysactive = 194,
		Lockbacktrack = 195,
		Fixchompy = 196,
		Updateanim = 197,
		Checkallquests = 198,
		Takeopenquests = 199,
		Deathsmoke = 200,
		Emoticon = 201,
		Checksum = 202,
		Questsize = 203,
		Questbreak = 204,
		Mapsize = 205,
		Caravanmedal = 206,
		Name = 207,
		Itemvalue = 208,
		Sizemulti = 209,
		Ignorenext = 210,
		Rerollshops = 211,
		Maxmedals = 212,
		Battlesize = 213,
		Pausesize = 214,
		GetFromMap = 215,
		Listsize = 216,
		Plural = 217,
		Layer = 218
	}

	public struct ItemUse
	{
		public ItemUsage[] usetype;

		public int[] values;
	}

	public struct BattleData
	{
		public int hp;

		public int maxhp;

		public int basehp;

		public int lv;

		public int atk;

		public int def;

		public int exp;

		public int freezeres;

		public int poisonres;

		public int numbres;

		public int sleepres;

		public int animid;

		public int money;

		public int hpt;

		public int id;

		public int baseatk;

		public int basedef;

		public int cantmove;

		public int tired;

		public int charge;

		public int trueid;

		public int eventondeath;

		public int moves;

		public int deathtype;

		public int hardhp;

		public int hardatk;

		public int harddef;

		public int holditem;

		public int defenseonhit;

		public int eventonfall;

		public int onhitaction;

		public int turnssincedeath;

		public int turnsalive;

		public int moreturnnextturn;

		public int pointer;

		public int turnsnodamage;

		public int blockTimes;

		public int[] chargeonotherenemy;

		public float size;

		public float weight;

		public float sizeonfreeze;

		public float initialsize;

		public Vector3 cursoroffset;

		public Vector3 battlepos;

		public Vector3 itemoffset;

		public List<int[]> condition;

		public List<int> skills;

		public List<int> delayedcondition;

		public int[] data;

		public SpriteRenderer helditem;

		public EntityControl entity;

		public EntityControl battleentity;

		public EntityControl ate;

		public EntityControl eatenby;

		public BattleControl.BattlePosition position;

		public List<BattleControl.AttackProperty> weakness;

		public Transform[] extrastuff;

		public string entityname;

		public ParticleSystem tiredpart;

		public ParticleSystem frostbitep;

		public bool lockskills;

		public bool lockitems;

		public bool locktri;

		public bool haspassed;

		public bool lockrelayreceive;

		public bool cantfall;

		public bool notaunt;

		public bool noblock;

		public bool fixedexp;

		public bool notired;

		public bool hidehp;

		public bool isdefending;

		public bool fled;

		public bool isasleep;

		public bool notattle;

		public bool hitaction;

		public bool actimmobile;

		public bool isnumb;

		public bool didnothing;

		public bool diebyitself;

		public bool noexpatstart;

		public bool atkdownonloseatkup;

		public bool lockposition;

		public bool plating;

		public bool alreadycounted;

		public bool destroyentity;

		public bool frozenlastturn;

		public bool lockcantmove;
	}

	public struct Entity_Data
	{
		public Vector3 freezesize;

		public Vector3 freezeoffset;

		public Vector3 freezeflipoffset;

		public Vector3 modeloffset;

		public Vector3 startscale;

		public Vector3 modelscale;

		public bool ismodel;

		public bool diganim;

		public bool freezenofall;

		public bool shakeondrop;

		public bool noshadow;

		public bool dontoverridejump;

		public bool hasiceanim;

		public bool noflyanim;

		public bool forceshadow;

		public bool Object;

		public int bleepid;

		public int basestate;

		public int basewalk;

		public float shadowsize;

		public float bleeppitch;

		public float minheight;

		public float startheight;

		public float startbobspd;

		public float startbobfreq;

		public string[] preloaddata;

		public EntityControl.WalkType walktype;
	}

	public enum TPDir
	{
		Right = 0,
		Left = 1,
		Up = 2,
		Down = 3,
		Away = 4,
		Center = 5
	}

	public struct LoadData
	{
		public Vector3 loadpos;

		public int mapid;

		public int areaid;

		public int level;

		public int times;

		public int timem;

		public int timeh;

		public int progression;

		public string filename;

		public bool[] challenges;
	}

	public static readonly string[] languagenames = new string[7] { "English (US)", "Español (LA)", "Português (BR)", "日本語 (Japanese)", "Deutsch (German)", "한국어 (Korean)", "Русский (Russian)" };

	public static string[] languagehelp;

	public static MainManager instance;

	public static bool[] secretunlocks = new bool[5];

	public static MapControl map;

	public static Camera GUICamera;

	public static Camera MainCamera;

	public static PlayerControl player;

	public static BattleControl battle;

	public SpriteRenderer cursor;

	public SpriteRenderer blinker;

	public SpriteRenderer switchicon;

	public static Coroutine transition;

	public static Coroutine hudmovement;

	public static Coroutine musiccoroutine;

	public static Shader fakelight;

	public static Shader shadowcaster;

	public static PauseMenu pausemenu;

	private static TextMesh[] letterpool;

	private static float[] keyhold = new float[2];

	public static int[] joybinds = new int[10] { -55, -55, -55, -55, -55, -55, -55, -55, -55, -55 };

	public static int[] caravanorder;

	public static Font[] fonts;

	public static Material[] fontmat;

	public BattleData[] playerdata;

	private static SpriteRenderer[] letterbox;

	private static SpriteRenderer[] hudsprites;

	public List<BattleData> reserveplayers;

	public static Material spritemat;

	public static Material holosprite;

	public static Material spritematlit;

	public static Material spritedefaultunity;

	public static Material grayscale;

	public static Material outlinemain;

	public static Material Main3D;

	public static Material Fade3D;

	public static Material mainPlane;

	public static Material fadePlane;

	public static Material windShader;

	public static Material emptymat;

	public static PhysicMaterial defaultpmat;

	public Transform camtarget;

	public Transform promptbox;

	public Transform globalcamdir;

	public Transform texttail;

	public Transform tailtarget;

	public Transform itemlist;

	public Transform npromptholder;

	public Transform questboardobj;

	public Transform discoverymessage;

	public Transform[] hud;

	public Transform[] transitionobj;

	private DynamicFont[] hudfont;

	public static ParticleSystem hitpart;

	public static ParticleSystem deathpart;

	public NPCControl boardcaller;

	public List<int>[] items;

	public List<int>[] boardquests;

	public List<int>[] badgeshops;

	public List<int>[] avaliablebadgepool;

	public List<int> extrafollowers;

	public List<int> lastdefeated;

	public List<int> multiselect;

	public List<int[]> badges;

	public List<int[]> samiramusics;

	public List<int[]> statbonus;

	public static int[] overridedlist = null;

	public static int[] discoveryicons;

	public static int[] achiveicons;

	public static int[] librarylimit = new int[5] { 50, 92, 70, 30, 100 };

	public static int[] listvar;

	public static int[] badgeorder;

	public static int[] musicids;

	public static int[] settingsindex = new int[26]
	{
		33, 34, 160, 28, 29, 32, 116, 30, 31, 147,
		140, 156, 80, 157, 35, 36, 183, 222, 231, 239,
		245, 261, 270, 255, 256, 282
	};

	public static int[,] libraryorder;

	public static int[,] recipedata;

	public static int[,] termacadeprize;

	private static int[][] questchecks;

	public static string[] menutext;

	public static string[] enemynames;

	public static string[] commondialogue;

	public static string[] commandhelptext;

	public static string[] areanames;

	public static string[] musicnames;

	public static string[,] skilldata;

	public static string[,] badgedata;

	public static string[,] boardquestdata;

	public static string[,] enemydata;

	public static string[,,] itemdata;

	public static string[,,] librarydata;

	public static Sprite[] guisprites;

	public static Sprite[] cursorsprite;

	public static Sprite[] grasssprite;

	public static Sprite[] leafsprites;

	public static Sprite[] battlemessage;

	public static Sprite[] librarysprites;

	private static Sprite[] textboxsprites;

	public static Sprite[,] itemsprites;

	public Sprite[] joybuttons;

	public Sprite[] joybuttonsps;

	public Sprite[] dynamicfont;

	public Sprite[] conditionsprites;

	public Sprite[] projectilepsrites;

	public static Sprite shadowsprite;

	public Color[] textcolors = new Color[7]
	{
		Color.black,
		Color.red,
		Color.green,
		Color.blue,
		Color.white,
		Color.gray,
		Color.yellow
	};

	public Color[] charcolor = new Color[3]
	{
		Color.yellow,
		Color.green,
		new Color(0f, 0.75f, 0.9f, 1f)
	};

	public Color[] menucolors;

	public Color[] questcolors = new Color[3]
	{
		Color.green,
		Color.yellow,
		Color.red
	};

	private static char[] letters;

	public static GameObject maintextbox;

	public static AudioSource[] sounds;

	public static AudioSource[] music;

	private static AudioSource bleeps;

	public const int totalenemies = 4;

	public const int flaglimit = 10;

	public const int diaglimit = 20;

	public const int maxdata = 10;

	public const int max9box = 5;

	public const int maxitemtypes = 1;

	public const int totalitems = 256;

	public const int moneymax = 999;

	public const int hpmax = 999;

	public const int tpmax = 99;

	public const int regionalammount = 100;

	public const int maxdamage = 99;

	public const int chargelimit = 3;

	public const int maxfpsv = 2;

	public const int saveslots = 3;

	public const int maxletters = 500;

	public const int itemtime = 600;

	public const int levelcap = 27;

	public const int lowhp = 4;

	public const int defaultpartqueue = 3000;

	public const int beehs = 4500;

	public const int mkhs = 9500;

	public const int maxmedals = 120;

	public const float kerning = -2f;

	public const float lineheight = 0.7f;

	public const float spacing = 0.3f;

	public const float punctuationdelay = 0.15f;

	public const float defaultcamspeed = 0.1f;

	public const float defaultcamoffsetspeed = 0.1f;

	public const float defaultcamanglespeed = 0.1f;

	public const float letterfixer = 0.07f;

	public const float wordmulti = 25f;

	public const float cookspeed = 2.5f;

	public static Vector2[] resolution = new Vector2[8]
	{
		new Vector2(1024f, 576f),
		new Vector2(1152f, 648f),
		new Vector2(1280f, 720f),
		new Vector2(1366f, 768f),
		new Vector2(1600f, 900f),
		new Vector2(1920f, 1080f),
		new Vector2(2560f, 1440f),
		new Vector2(3840f, 2160f)
	};

	public static Vector2[] leafpos;

	public static Vector2 defaultlistpos = new Vector2(4f, 0.5f);

	public static Vector2 listpos;

	public static Vector2 letteroffset = new Vector2(0f, -0.1f);

	public static Vector3 bubblepos = new Vector3(0f, 4.15f, 10f);

	public static Vector3 battlecampos = new Vector3(0f, 3f, -9.25f);

	public static Vector3 battlecamangle = new Vector3(5f, 0f, 0f);

	public static Vector3 tempcamangleoffset;

	public static Vector3 tempcamoffset;

	public static Vector3 defaultcamoffset = new Vector3(0f, 2.25f, -8.25f);

	public static Vector3 defaultcamangle = new Vector3(10f, 0f);

	public static Vector3 screenshake;

	public static Vector3 entityactive = new Vector3(0.6f, 1.25f, -1f);

	public static Vector3 tempmaplp;

	public static Vector3 tempmapln;

	public static Vector3? tempcampos;

	public static Transform tempcamtarget;

	public static Transform listdescbox;

	public static Transform gamemonitor;

	public static EventControl events;

	public CardGame cardgame;

	private static int lastmusic;

	public static int languageid = -1;

	public static int saveslot;

	public static int resolutionindex;

	public static int listlow;

	public static int listmax;

	public static int listammount;

	public static int storeid;

	public static int listcancel;

	public static int listcursor;

	public static int listtype;

	public static int joyid;

	public static int fps = 1;

	public static int musicchannel;

	public static int listoption;

	public static int enableoutline = 2;

	public static int eventtoss = -1;

	public static int downsample;

	public static int lastinside;

	public static int particlelevel = 2;

	public static int lastsoundid;

	public static int vsync = 0;

	public static int basefont = 0;

	public static int listY;

	public static int usejoystick = 1;

	public static int forcejoystick = -1;

	public static int lastitemuser;

	public static int analog = 2;

	public static int lastevent;

	public static int? listredirect;

	public static bool savelastlist;

	public static bool notextbacktrack;

	public static bool listdesc;

	public static bool noskip;

	public static bool listsell;

	public static bool joystick;

	public static bool stickholdx;

	public static bool stickholdy;

	public static bool fullscreen;

	public static bool lowshadows;

	public static bool lowtexture;

	public static bool halt;

	public static bool nowindeffect;

	public static bool listcanceled;

	public static bool battleresult;

	public static bool battlelossevent;

	public static bool battlefled;

	public static bool battlenoexp;

	public static bool haltbattleload;

	public static bool battleenemyfled;

	public static bool forcecontrollerupdate;

	public static bool keepmusicafterbattle = true;

	public static bool mashcommandalt;

	public static bool debugenalbed;

	public static bool monoaudio;

	public static bool pauseonfocus;

	public static bool roomtransition;

	public static bool hudvisible;

	public static bool snapTo8 = true;

	public static float lasttextcenter;

	public static float textwidth;

	public static float tempcamspeed;

	public static float musicvolume = 0.4f;

	public static float soundvolume = 0.5f;

	public static float bleepvolume = 0.5f;

	public static float framestep;

	public static float musicresume = -1f;

	public static float messagebreak = 10.5f;

	public static float itemdescbreak = 10.5f;

	private static float[] hudvalue;

	public int tp;

	public int tpt;

	public int maxtp;

	public int basetp;

	public int partylevel = 1;

	public int partyexp;

	public int neededexp = 100;

	public int areaid;

	public int insideid;

	public int option;

	public int maxoptions;

	public int maxitems;

	public int maxstorage = 35;

	public int money;

	public int bp;

	public int maxbp;

	public int promptpick;

	public int battlestage;

	public int moneyt;

	public int inmusicrange = -1;

	public int entitytouchevent = -1;

	public int letterprompt = -1;

	public int lastPrompt;

	public int[] flagvar;

	public int[] promptpointers;

	public int[] partyorder = new int[3] { 0, 1, 2 };

	public bool[] flags;

	public bool[] regionalflags;

	public bool[] crystalbflags;

	public bool[,] librarystuff;

	public string[] flagstring;

	public Vector3 camoffset;

	public Vector3 camangleoffset;

	public Vector3 camoffset2;

	public Vector3[] vectorflags;

	public Vector3? camtargetpos;

	public bool prompt;

	public bool pause;

	public bool minipause;

	public bool message;

	public bool waitinput;

	public bool inbattle;

	public bool skiptext;

	public bool started;

	public bool intransition;

	public bool inlist;

	public bool inevent;

	public bool overridefollower;

	public bool hudstats;

	public bool numberprompt;

	public bool speedup;

	public bool camanglechange;

	public bool changecamspeed;

	public bool isholdingskip;

	public bool firstbattleaction;

	public bool itempicked;

	public float hudcooldown;

	public float camspeed;

	public float showmoney;

	public float discoveryhud;

	public float globalcooldown;

	public float camoffsetspeed;

	public float camanglespeed;

	public float inputcooldown;

	public int[,] enemyencounter;

	public static float[] downsamples = new float[7] { 1f, 0.9f, 0.8f, 0.75f, 0.6f, 0.5f, 0.4f };

	private static List<string> diagstring;

	private static int currentdialogue;

	private RenderTexture rtex;

	private static AudioClip[] asounds;

	private static AudioClip[] dsounds;

	private static AudioClip[] msounds;

	private static Texture[] parttex;

	private static Sprite[] spritepart;

	private static GameObject[] partfab;

	public static bool basicload;

	public static Entity_Data[] endata;

	public int[] prizeflags;

	public int[] prizeids;

	public int[] prizeenemyids;

	private static bool joytest;

	public int clocksec;

	public int clockmin;

	public int clockhour;

	public static bool timeddemo;

	private int demotimer;

	private Transform textbox;

	public static readonly string asiansize = "|size,0.8,0.9|";

	private static string tempdiag;

	private static float linebr;

	private static float fontdsize;

	private static int fontdtype;

	private static bool backtracking;

	public static Coroutine templetter;

	private static int[] bountyquests = new int[5] { 9, 23, 8, 21, 10 };

	public static int[] preconfigjoy = new int[9] { 0, 1, 2, 3, 4, 5, 6, 7, 8 };

	public static int[] precjoystring = new int[9] { 225, 226, 227, 232, 233, 228, 229, 242, 269 };

	public static float[][] musicloop;

	private static List<int> existCheck = new List<int>();

	private static int[] tphp;

	private static int[] ptmhp;

	private static int[] tmtp = new int[2];

	private static int tempmoneh;

	public const float distancemultiplier = 1f;

	public const float holdkeytime = 7f;

	private const float colormultiplier = 2f;

	private const float deadzone = 0.5f;

	public const float defaultcolorspeed = 5.9f;

	public const float defaultsounddistance = 25f;

	public static Coroutine chaptername;

	private static List<string[]> define;

	private static readonly Vector2Int[] koreanLimit = new Vector2Int[3]
	{
		new Vector2Int(3, 21),
		new Vector2Int(24, 44),
		new Vector2Int(45, 72)
	};

	private static int[] koreanHL = new int[2] { -1, -1 };

	private static readonly string[] letterPromptHelp = new string[6] { "ひらがな", "カタカナ", "Русский", "한국어", "✰ ♡ ♩ $", "ABC/123/?!ß" };

	private static Vector3 camposshake;

	private static readonly Color[] itemlistbg = new Color[2]
	{
		new Color(1f, 1f, 1f, 0.75f),
		new Color(0f, 1f, 0f, 0.75f)
	};

	public int[] multilist;

private void Start()
{
		Application.runInBackground = !InputIO.IsConsole;
		Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
		if (!basicload)
		{
			InputIO.StartUp();
		}
		Time.timeScale = 1f;
		MainCamera = Camera.main;
		musicresume = -1f;
		instance = this;
		StartCoroutine(LoadEverything());
		if (!InputIO.IsEditor)
		{
			Debug.Log(Application.version);
		}
	}

	private void FontSet()
	{
		string text = "";
		for (int i = 0; i < fonts.Length; i++)
		{
			text = text + "|font," + i + "|";
			for (int j = 0; j < letterPromptHelp.Length; j++)
			{
				text += Resources.Load<TextAsset>("Data/LetterPrompt" + j).ToString();
			}
		}
		StartCoroutine(SetText("|single|" + text, Vector3.back, MainCamera.transform));
	}

	private static IEnumerator LoadEverything()
	{
		StartMenu s = UnityEngine.Object.FindObjectOfType<StartMenu>();
		s.enabled = false;
		Cursor.visible = false;
		MainCamera.depthTextureMode = DepthTextureMode.Depth;
		RenderSettings.ambientLight = Color.gray;
		basicload = false;
		yield return null;
		events = instance.gameObject.AddComponent<EventControl>();
		yield return null;
		chaptername = instance.StartCoroutine(instance.LoadEssentials());
		while (chaptername != null)
		{
			yield return null;
		}
		yield return null;
		if (languageid > -1)
		{
			instance.SetVariables();
		}
		else
		{
			InputIO.LoadSettings(false);
		}
		yield return null;
		SetRenderTexture(0);
		yield return null;
		instance.FontSet();
		yield return null;
		basicload = true;
		s.enabled = true;
	}

	public static void LoadLangSpecific()
	{
		if (languageid == 4)
		{
			guisprites[61] = guisprites[224];
			guisprites[109] = guisprites[225];
			guisprites[75] = guisprites[226];
		}
	}

public static void SetRenderTexture(int downsampleindex)
{
    downsample = downsampleindex;

    if (downsample == 0)
    {
        RenderTexture oldTexture = MainCamera.targetTexture;

        MainCamera.targetTexture = null;
        MainCamera.rect = new Rect(0f, 0f, 1f, 1f);

        GUICamera.transform.GetChild(0).gameObject.SetActive(false);

        MainCamera.transform.GetChild(1).GetComponent<Camera>().targetTexture = null;

if (oldTexture != null)
{
    oldTexture.Release();
}
    }
    else
    {
        GUICamera.transform.GetChild(0).gameObject.SetActive(true);

        Material material = GUICamera.transform.GetChild(0)
            .GetComponentInChildren<MeshRenderer>().material;

        MainCamera.rect = new Rect(
            0f,
            0f,
            downsamples[downsample],
            downsamples[downsample]
        );

        RenderTexture renderTexture = new RenderTexture(
            (int)(1920f * downsamples[downsample]),
            (int)(1080f * downsamples[downsample]),
            24
        );

        MainCamera.transform.GetChild(1)
            .GetComponent<Camera>()
            .targetTexture = renderTexture;

        renderTexture.filterMode = FilterMode.Bilinear;

        MainCamera.targetTexture = renderTexture;

        material.mainTexture = renderTexture;

        RefreshRenderTex();
    }
}

	public static void RefreshRenderTex()
	{
		GUICamera.transform.GetChild(0).GetComponent<Renderer>().material.mainTextureScale = Vector2.one * (MainCamera.GetComponent<FXAA>().enabled ? 1f : downsamples[downsample]);
	}

	public static bool AllOnGround(EntityControl[] entities)
	{
		for (int i = 0; i < entities.Length; i++)
		{
			if (!entities[i].onground)
			{
				return false;
			}
		}
		return true;
	}

	public static string GetDialogueFromMap(Maps mapid, int lineid, int boxid)
	{
		string text = Resources.Load<TextAsset>("Data/Dialogues" + languageid + "/Maps/" + mapid).ToString().Replace("\r\n", "\n")
			.Split('\n')[lineid];
		if (boxid == -1)
		{
			return text;
		}
		return text.Split(new string[1] { "|next|" }, StringSplitOptions.None)[boxid];
	}

	public static Entity_Data[] LoadEntityData()
	{
		string[] array = Resources.Load<TextAsset>("Data/EntityValues").ToString().Split('\n');
		Entity_Data[] array2 = new Entity_Data[Enum.GetNames(typeof(AnimIDs)).Length];
		for (int i = 0; i < array2.Length; i++)
		{
			if (i < array.Length)
			{
				string[] array3 = array[i].Split(',');
				array2[i].shadowsize = Convert.ToSingle(array3[0]);
				array2[i].startscale = new Vector3(Convert.ToSingle(array3[1].Replace("(", "")), Convert.ToSingle(array3[2]), Convert.ToSingle(array3[3].Replace(")", "")));
				array2[i].bleeppitch = Convert.ToSingle(array3[4]);
				array2[i].bleepid = Convert.ToInt32(array3[5]);
				array2[i].ismodel = Convert.ToBoolean(array3[6]);
				array2[i].modelscale = new Vector3(Convert.ToSingle(array3[7].Replace("(", "")), Convert.ToSingle(array3[8]), Convert.ToSingle(array3[9].Replace(")", "")));
				array2[i].modeloffset = new Vector3(Convert.ToSingle(array3[10].Replace("(", "")), Convert.ToSingle(array3[11]), Convert.ToSingle(array3[12].Replace(")", "")));
				array2[i].freezesize = new Vector3(Convert.ToSingle(array3[13].Replace("(", "")), Convert.ToSingle(array3[14]), Convert.ToSingle(array3[15].Replace(")", "")));
				array2[i].freezeoffset = new Vector3(Convert.ToSingle(array3[16].Replace("(", "")), Convert.ToSingle(array3[17]), Convert.ToSingle(array3[18].Replace(")", "")));
				array2[i].freezeflipoffset = new Vector3(Convert.ToSingle(array3[19].Replace("(", "")), Convert.ToSingle(array3[20]), Convert.ToSingle(array3[21].Replace(")", "")));
				string[] array4 = null;
				if (array3[22].Length > 0)
				{
					array4 = array3[22].Split('?');
					array2[i].preloaddata = new string[array4.Length];
					for (int j = 0; j < array4.Length; j++)
					{
						array2[i].preloaddata[j] = array4[j];
					}
				}
				array2[i].shakeondrop = Convert.ToBoolean(array3[23]);
				array2[i].diganim = Convert.ToBoolean(array3[24]);
				array2[i].dontoverridejump = Convert.ToBoolean(array3[25]);
				array2[i].freezenofall = Convert.ToBoolean(array3[26]);
				array2[i].noshadow = Convert.ToBoolean(array3[27]);
				array2[i].walktype = (EntityControl.WalkType)Convert.ToInt32(array3[28]);
				array2[i].basestate = Convert.ToInt32(array3[29]);
				array2[i].basewalk = Convert.ToInt32(array3[30]);
				array2[i].minheight = Convert.ToSingle(array3[31]);
				array2[i].startheight = Convert.ToSingle(array3[32]);
				array2[i].startbobspd = Convert.ToSingle(array3[33]);
				array2[i].startbobfreq = Convert.ToSingle(array3[34]);
				array2[i].hasiceanim = Convert.ToBoolean(array3[35]);
				if (array3.Length > 36)
				{
					array2[i].noflyanim = Convert.ToBoolean(array3[36]);
					array2[i].forceshadow = Convert.ToBoolean(array3[37]);
					array2[i].Object = Convert.ToBoolean(array3[38]);
				}
			}
			else
			{
				array2[i].bleeppitch = 1f;
				array2[i].startscale = Vector3.one;
				array2[i].shadowsize = 1f;
			}
		}
		return array2;
	}

	public static void EndMiniGame(bool antialias, int score)
	{
		ResetCamera(true);
		SetRenderTexture(0);
		MainCamera.GetComponent<FXAA>().enabled = antialias;
		GetRewardTokens(score);
		instance.flagvar[0] = score;
	}

	private IEnumerator LoadEssentials()
{
    Debug.LogError("!!! NEW MAINMANAGER CODE IS RUNNING !!!");

    letterpool = new TextMesh[500];
    for (int i = 0; i < letterpool.Length; i++)
    {
        letterpool[i] = NewLetter(i.ToString());
    }

    Debug.LogError("=== LETTERPOOL COMPLETE ===");

screenshake = Vector3.zero;
yield return null;

Debug.LogError("=== BEFORE TEXTBOX ===");

textboxsprites = Resources.LoadAll<Sprite>("Sprites/GUI/textbox");

Debug.LogError("=== AFTER TEXTBOX ===");

yield return null;

		for (int i = 0; i < letterpool.Length; i++)
		{
			letterpool[i] = NewLetter(i.ToString());
		}
		screenshake = Vector3.zero;
		yield return null;
		textboxsprites = Resources.LoadAll<Sprite>("Sprites/GUI/textbox");
		yield return null;
		partfab = Resources.LoadAll<GameObject>("Prefabs/Particles");
		yield return null;
		parttex = Resources.LoadAll<Texture>("Sprites/Particles");
		yield return null;
		spritepart = Resources.LoadAll<Sprite>("Sprites/Particles");
		yield return null;
		asounds = Resources.LoadAll<AudioClip>("Audio/Sounds");
		yield return null;
		if (InputIO.IsConsole)
		{
			string[] names = Enum.GetNames(typeof(Musics));
			msounds = new AudioClip[names.Length];
			for (int j = 0; j < names.Length; j++)
			{
				msounds[j] = Resources.Load<AudioClip>("Audio/Music/" + names[j]);
			}
		}
		dsounds = Resources.LoadAll<AudioClip>("Audio/Sounds/Dialogue");
		yield return null;
		MonoBehaviour.print("preloaded " + partfab.Length + " particle prefabs + " + (parttex.Length + spritepart.Length) + " textures AND " + (asounds.Length + dsounds.Length) + " sounds");
		GUICamera = MainCamera.transform.GetChild(0).GetComponent<Camera>();
		defaultpmat = Resources.Load<PhysicMaterial>("Materials/DefaultPhysis");
		spritemat = Resources.Load<Material>("Materials/SpriteMat");
		holosprite = Resources.Load<Material>("Materials/SpriteHologram");
		emptymat = Resources.Load<Material>("Materials/Empty");
		spritematlit = Resources.Load<Material>("Materials/SpriteLit");
		outlinemain = Resources.Load<Material>("Materials/OutlineMain");
		spritedefaultunity = Resources.Load<Material>("Materials/SpriteDefault");
		windShader = Resources.Load<Material>("Materials/WindShader");
		Main3D = Resources.Load<Material>("Materials/3DMain");
		Fade3D = Resources.Load<Material>("Materials/3DFade");
		grayscale = Resources.Load<Material>("Materials/Grayscale");
		letters = Resources.Load<TextAsset>("Data/Letters").ToString().ToCharArray();
		grasssprite = Resources.LoadAll<Sprite>("Sprites/Objects/grass");
		yield return null;
		leafsprites = Resources.LoadAll<Sprite>("Sprites/GUI/battleleaves");
		yield return null;
		fakelight = Resources.Load<Material>("Materials/FakeLight").shader;
		shadowsprite = Resources.Load<Sprite>("Sprites/Misc/shadow");
		mainPlane = Resources.Load<Material>("Materials/MainPlane");
		fadePlane = Resources.Load<Material>("Materials/FadePlane");
		languagehelp = Resources.Load<TextAsset>("Data/LanguageHelp").ToString().Split('\n');
		endata = LoadEntityData();
		yield return null;
		hitpart = (UnityEngine.Object.Instantiate(Resources.Load("Prefabs/Particles/HitPart"), new Vector3(0f, -999f), Quaternion.identity) as GameObject).GetComponent<ParticleSystem>();
		deathpart = (UnityEngine.Object.Instantiate(Resources.Load("Prefabs/Particles/deathsmoke"), new Vector3(0f, -999f), Quaternion.Euler(-90f, 0f, 0f)) as GameObject).GetComponent<ParticleSystem>();
		globalcamdir = new GameObject("CamDir").transform;
		globalcamdir.transform.parent = MainCamera.transform;
		globalcamdir.transform.localPosition = Vector3.zero;
		librarysprites = Resources.LoadAll<Sprite>("Sprites/Items/EnemyPortraits");
		yield return null;
		extrafollowers = new List<int>();
		sounds = new AudioSource[15];
		music = new AudioSource[1];
		musicids = new int[music.Length];
		string[] array = Resources.Load<TextAsset>("Data/LeafPos").ToString().Split('\n');
		string[] array3 = new string[1];
		leafpos = new Vector2[array.Length];
		for (int k = 0; k < array.Length; k++)
		{
			string[] array2 = array[k].Split(',');
			leafpos[k] = new Vector2(Convert.ToSingle(array2[0]), Convert.ToSingle(array2[1]));
		}
		List<int[]> list = new List<int[]>();
		array = Resources.Load<TextAsset>("Data/QuestChecks").ToString().Split('\n');
		for (int l = 0; l < array.Length; l++)
		{
			string[] array2 = array[l].Split('@');
			List<int> list2 = new List<int>();
			for (int m = 0; m < array2.Length; m++)
			{
				list2.Add(Convert.ToInt32(array2[m]));
			}
			list.Add(list2.ToArray());
		}
		questchecks = list.ToArray();
		yield return null;
		array = Resources.Load<TextAsset>("Data/Termacade").ToString().Split('\n');
		termacadeprize = new int[array.Length, array[0].Split(',').Length];
		for (int n = 0; n < array.Length; n++)
		{
			string[] array2 = array[n].Split(',');
			for (int num = 0; num < array2.Length; num++)
			{
				termacadeprize[n, num] = Convert.ToInt32(array2[num]);
			}
		}
		array = Resources.Load<TextAsset>("Data/EnemyData").ToString().Split('\n');
		enemydata = new string[array.Length, array[0].Split(',').Length];
		for (int num2 = 0; num2 < array.Length; num2++)
		{
			string[] array2 = array[num2].Split(',');
			for (int num3 = 0; num3 < array2.Length; num3++)
			{
				enemydata[num2, num3] = array2[num3];
			}
		}
		yield return null;
		fonts = new Font[Enum.GetNames(typeof(Fonts)).Length];
		fontmat = new Material[fonts.Length];
		for (int num4 = 0; num4 < fonts.Length; num4++)
		{
			if (num4 != 2)
			{
				fonts[num4] = Resources.Load<Font>("Fonts/" + (Fonts)num4);
				fontmat[num4] = Resources.Load<Material>("Fonts/" + (Fonts)num4);
			}
		}
		bleeps = base.gameObject.AddComponent<AudioSource>();
		for (int num5 = 0; num5 < sounds.Length; num5++)
		{
			sounds[num5] = base.gameObject.AddComponent<AudioSource>();
			sounds[num5].velocityUpdateMode = AudioVelocityUpdateMode.Fixed;
			sounds[num5].volume = soundvolume;
		}
		yield return null;
		for (int num6 = 0; num6 < music.Length; num6++)
		{
			music[num6] = base.gameObject.AddComponent<AudioSource>();
			if (num6 > 0)
			{
				music[num6].volume = 0f;
			}
			music[num6].loop = true;
		}
		yield return null;
        List<Sprite> list3 = new List<Sprite>();

        Sprite[] guiSprites = Resources.LoadAll<Sprite>("sprites/gui");
        Sprite[] gui2Sprites = Resources.LoadAll<Sprite>("sprites/gui2");

        Debug.LogError("GUI1 = " + guiSprites.Length);
        Debug.LogError("GUI2 = " + gui2Sprites.Length);

        list3.AddRange(guiSprites);
        list3.AddRange(gui2Sprites);

        guisprites = list3.ToArray();

		guisprites[1] = guiSprites.First(s => s.name == "gui_1");
		guisprites[59] = guiSprites.First(s => s.name == "gui_59");
        guisprites[42] = guiSprites.First(s => s.name == "gui_42");
		guisprites[145] = guiSprites.First(s => s.name == "gui_145");
		guisprites[0] = guiSprites.First(s => s.name == "gui_0");

        Debug.LogError("TOTAL GUI = " + guisprites.Length);

        if (guisprites.Length <= 145)
        {
        Debug.LogError("STOP: GUI HAS TOO FEW SPRITES");
        yield break;
        }

        cursorsprite = new Sprite[1] { guisprites[145] };

        Debug.LogError("=== GUI COMPLETE ===");

        yield return null; 

		if (!basicload && Application.version.Contains("Beta"))
		{
			NewUIObject("betawatermark", GUICamera.transform, new Vector3(7.8f, -4.4f, 1f), Vector3.one, guisprites[169], 9999).GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.1f);
		}
		yield return null;
		letterbox = new SpriteRenderer[2];
		for (int num7 = 0; num7 < 2; num7++)
		{
			letterbox[num7] = NewSolidColor("letterbox" + num7, Color.black, 0.2f, default(Vector3), Vector2.one / 2f);
			letterbox[num7].color = Color.clear;
			letterbox[num7].gameObject.layer = 5;
			letterbox[num7].transform.parent = GUICamera.transform;
			letterbox[num7].transform.localEulerAngles = Vector3.zero;
			letterbox[num7].transform.localScale = new Vector3(5f, 0.5f, 1f);
			letterbox[num7].sortingOrder = -40;
		}
		letterbox[0].transform.localPosition = new Vector3(0f, 5.75f, 10f);
		letterbox[1].transform.localPosition = new Vector3(0f, -5.75f, 10f);

		Debug.LogError("=== LETTERBOX COMPLETE ===");

		array = Resources.Load<TextAsset>("Data/RecipeData").ToString().Split('\n');

recipedata = new int[array.Length - 1, 3];

	for (int num8 = 0; num8 < array.Length - 1; num8++)
{
    string[] array2 = array[num8].Split(',');
    for (int num9 = 0; num9 < array2.Length; num9++)
    {
        recipedata[num8, num9] = Convert.ToInt32(array2[num9]);
    }

    if (recipedata[num8, 0] > recipedata[num8, 1] && recipedata[num8, 1] > -1)
    {
        int num10 = recipedata[num8, 1];
        recipedata[num8, 0] = recipedata[num8, 1];
        recipedata[num8, 1] = num10;
    }
}

Debug.LogError("=== RECIPEDATA COMPLETE ===");
yield return null;
		instance.projectilepsrites = Resources.LoadAll<Sprite>("Sprites/Misc/projectiles");

		Debug.LogError("=== PROJECTILES COMPLETE ===");

		yield return null;
		Physics.gravity = new Vector3(0f, -40f, 0f);
		InputIO.GetJoyButtons();
		yield return null;
		InputIO.SetDefaultKeys();
		yield return null;
		chaptername = null;
	}

private static void LoadItemSprites()
{
    Sprite[] items0 = Resources.LoadAll<Sprite>("sprites/items/items0");
    Sprite[] items1 = Resources.LoadAll<Sprite>("sprites/items/items1");

    itemsprites = new Sprite[2, 256];

    for (int i = 0; i < 176; i++)
    {
        if (items0 != null && i < items0.Length)
        {
            itemsprites[0, i] = items0[i];
        }
    }

    for (int i = 176; i < 256; i++)
    {
        int index = i - 176;

        if (items1 != null && index >= 0 && index < items1.Length)
        {
            itemsprites[0, i] = items1[index];
        }
    }
}

private static Sprite GetMoreItem(int i, int offset = 176)
{
    Sprite[] items1 = Resources.LoadAll<Sprite>("Sprites/Items/items1");

    int index = Mathf.Abs(offset - i);

    Debug.Log("GetMoreItem: i=" + i +
              " offset=" + offset +
              " index=" + index +
              " items1 count=" + items1.Length);

    if (items1 == null || items1.Length == 0)
    {
        Debug.LogWarning("GetMoreItem: items1 has 0 sprites. Returning null.");
        return null;
    }

    if (index < 0 || index >= items1.Length)
    {
        Debug.LogWarning(
            "GetMoreItem: index " + index +
            " is outside items1 range 0-" + (items1.Length - 1) +
            ". Returning null."
        );
        return null;
    }

    return items1[index];
}

	private static TextMesh NewLetter()
	{
		return NewLetter("");
	}

	public static Maps CurrentMap()
	{
		return map.mapid;
	}

	private static TextMesh NewLetter(string id)
	{
		TextMesh textMesh = new GameObject("letter" + id).AddComponent<TextMesh>();
		textMesh.transform.parent = instance.transform;
		textMesh.transform.localPosition = new Vector3(0f, 0f, 10f);
		textMesh.gameObject.layer = 5;
		textMesh.richText = false;
		textMesh.fontStyle = FontStyle.Normal;
		textMesh.anchor = TextAnchor.LowerLeft;
		MeshRenderer component = textMesh.GetComponent<MeshRenderer>();
		component.shadowCastingMode = ShadowCastingMode.Off;
		component.allowOcclusionWhenDynamic = false;
		return textMesh;
	}

	public static IEnumerator LateSound(string sound, float delay)
	{
		yield return new WaitForSeconds(delay);
		PlaySound(sound);
	}

	public static bool IsPaused()
	{
		if (!instance.minipause && !instance.pause && !instance.inevent)
		{
			if (battle != null)
			{
				return battle.inevent;
			}
			return false;
		}
		return true;
	}

	public static SpriteRenderer Dimmer(int fadetype)
	{
		return Dimmer(fadetype, 0.1f, Color.black);
	}

	public static SpriteRenderer Dimmer(int fadetype, float fadespeed)
	{
		return Dimmer(fadetype, fadespeed, Color.black);
	}

	public static SpriteRenderer Dimmer(int fadetype, float fadespeed, Color color)
	{
		PlayTransition(fadetype, 0, fadespeed, color);
		return instance.transitionobj[0].GetComponent<SpriteRenderer>();
	}

	public static void SetCamera(Transform target, Vector3? targetpos, float speed)
	{
		SetCamera(target, targetpos, speed, defaultcamoffset);
	}

	public static void SetCamera(Vector3 targetpos, float speed)
	{
		SetCamera(null, targetpos, speed, defaultcamoffset);
	}

	public static void SetCamera(Vector3 targetpos, Vector3 angle, Vector3 offset, float speed)
	{
		SetCamera(null, targetpos, speed, offset, angle);
	}

	public static void SetCamera(Vector3 targetpos, Vector3 angle, float speed)
	{
		SetCamera(null, targetpos, speed, defaultcamoffset, angle);
	}

	public static void SetCamera(Transform target, Vector3? targetpos, float speed, Vector3 offset)
	{
		SetCamera(target, targetpos, speed, offset, (battle != null) ? new Vector3(5f, 0f) : defaultcamangle);
	}

	public static void SetCamera(Transform target, Vector3? targetpos, float speed, Vector3 offset, Vector3 angle)
	{
		instance.camtarget = target;
		instance.camtargetpos = targetpos;
		instance.camoffset = offset;
		instance.camangleoffset = angle;
		instance.camspeed = speed;
		if (speed >= 1f)
		{
			if (target != null)
			{
				MainCamera.transform.parent.position = target.transform.position;
			}
			else if (targetpos.HasValue)
			{
				MainCamera.transform.parent.position = targetpos.Value;
			}
			MainCamera.transform.parent.localEulerAngles = angle;
			MainCamera.transform.localPosition = offset;
		}
	}

	public void SetVariables()
	{
		battlemessage = Resources.LoadAll<Sprite>("Sprites/GUI/BattleMessage/battlem" + languageid);
		if (battlemessage == null || battlemessage.Length == 0)
		{
			battlemessage = Resources.LoadAll<Sprite>("Sprites/GUI/BattleMessage/battlem0");
		}
		menutext = Resources.Load<TextAsset>("Data/Dialogues" + languageid + "/MenuText").ToString().Replace("\r\n", "\n")
			.Split('\n');
		commondialogue = Resources.Load<TextAsset>("Data/Dialogues" + languageid + "/CommonDialogue").ToString().Split('\n');
		musicnames = Resources.Load<TextAsset>("Data/Dialogues" + languageid + "/MusicList").ToString().Split('\n');
		commandhelptext = Resources.Load<TextAsset>("Data/Dialogues" + languageid + "/ActionCommands").ToString().Split('\n');
		areanames = Resources.Load<TextAsset>("Data/Dialogues" + languageid + "/AreaNames").ToString().Split('\n');
		samiramusics = new List<int[]>();
		prizeflags = new int[23]
		{
			13, 17, 18, 19, 20, 21, 25, 33, 34, 36,
			44, 45, 46, 48, 51, 52, 57, 61, 63, 30,
			31, 64, 65
		};
		prizeids = new int[23]
		{
			5, 24, 14, 15, 1, 32, 10, 73, 37, 26,
			66, 67, 65, 60, 55, 61, 0, 63, 68, 79,
			58, 70, 68
		};
		prizeenemyids = new int[23]
		{
			2, 0, 24, 3, 15, 31, 46, 49, 54, 69,
			76, 42, 41, 51, 34, 0, -1, 72, 36, 97,
			96, 40, 90
		};
		badgeshops = new List<int>[2];
		avaliablebadgepool = new List<int>[badgeshops.Length];
		for (int i = 0; i < badgeshops.Length; i++)
		{
			badgeshops[i] = new List<int>();
			avaliablebadgepool[i] = new List<int>();
		}
		items = new List<int>[3];
		for (int j = 0; j < items.Length; j++)
		{
			items[j] = new List<int>();
		}
		itemdata = new string[1, 256, 7];
		string[] array = Resources.Load<TextAsset>("Data/Dialogues" + languageid + "/Items").ToString().Replace("\r\n", "\n")
			.Split('\n');
		string[] array2 = Resources.Load<TextAsset>("Data/ItemData").ToString().Split('\n');
		for (int k = 0; k < 1; k++)
		{
			for (int l = 0; l < array.Length; l++)
			{
				string[] array3 = array[l].Split('@');
				if (array3.Length > 1)
				{
					itemdata[k, l, 0] = array3[0];
					itemdata[k, l, 1] = array3[1];
					itemdata[k, l, 2] = array3[2];
					if (array3.Length > 3)
					{
						itemdata[k, l, 3] = array3[3];
					}
					if (k == 0)
					{
						array3 = array2[l].Split('@');
						itemdata[k, l, 4] = array3[0];
						itemdata[k, l, 5] = array3[1];
						itemdata[k, l, 6] = array3[2];
					}
				}
			}
		}
		array = Resources.Load<TextAsset>("Data/Dialogues" + languageid + "/Skills").ToString().Split('\n');
		array2 = Resources.Load<TextAsset>("Data/SkillData").ToString().Split('\n');
		skilldata = new string[array.Length - 1, 13];
		for (int m = 0; m < skilldata.GetLength(0); m++)
		{
			string[] array4 = array[m].Split('@');
			skilldata[m, 0] = array4[0];
			skilldata[m, 1] = array4[1];
			array4 = array2[m].Split('@');
			for (int n = 2; n < skilldata.GetLength(1); n++)
			{
				skilldata[m, n] = array4[n - 2];
			}
		}
		array = Resources.Load<TextAsset>("Data/Dialogues" + languageid + "/BadgeName").ToString().Split('\n');
		array2 = Resources.Load<TextAsset>("Data/BadgeData").ToString().Split('\n');
		badgedata = new string[array.Length - 1, 9];
		for (int num = 0; num < badgedata.GetLength(0); num++)
		{
			string[] array5 = array[num].Split('@');
			badgedata[num, 0] = array5[0];
			badgedata[num, 1] = array5[1];
			badgedata[num, 6] = array5[2];
			array5 = array2[num].Split('@');
			badgedata[num, 2] = array5[0];
			badgedata[num, 3] = array5[1];
			badgedata[num, 4] = array5[2];
			badgedata[num, 5] = array5[3];
			badgedata[num, 7] = array5[4];
			badgedata[num, 8] = array5[5];
		}
		array = Resources.Load<TextAsset>("Data/BadgeOrder").ToString().Split('\n');
		badgeorder = new int[array.Length];
		for (int num2 = 0; num2 < array.Length; num2++)
		{
			badgeorder[num2] = Convert.ToInt32(array[num2]);
		}
		array = Resources.Load<TextAsset>("Data/Dialogues" + languageid + "/BoardQuests").ToString().Split('\n');
		array2 = Resources.Load<TextAsset>("Data/BoardData").ToString().Split('\n');
		boardquestdata = new string[array.Length, array[0].Split('@').Length + array2[0].Split('@').Length];
		for (int num3 = 0; num3 < array.Length; num3++)
		{
			string[] array6 = array[num3].Split('@');
			int num4 = 0;
			for (int num5 = 0; num5 < array6.Length; num5++)
			{
				boardquestdata[num3, num5] = array6[num5];
				num4++;
			}
			array6 = array2[num3].Split('@');
			for (int num6 = 0; num6 < array6.Length; num6++)
			{
				boardquestdata[num3, num6 + num4] = array6[num6];
			}
		}
		librarydata = new string[5, 256, 10];
		libraryorder = new int[librarydata.GetLength(0), librarydata.GetLength(1)];
		for (int num7 = 0; num7 < librarydata.GetLength(0); num7++)
		{
			string[] array7 = new string[1] { "" };
			switch (num7)
			{
			case 0:
			{
				array7 = Resources.Load<TextAsset>("Data/DiscoveryOrder").ToString().Split('\n');
				array = Resources.Load<TextAsset>("Data/Dialogues" + languageid + "/Discoveries").ToString().Split('\n');
				List<string> list2 = new List<string>();
				discoveryicons = new int[librarylimit[0]];
				for (int num9 = 0; num9 < librarylimit[0]; num9++)
				{
					string[] array9 = array7[num9].Split(',');
					discoveryicons[num9] = Convert.ToInt32(array9[1]);
					list2.Add(array9[0]);
				}
				array7 = list2.ToArray();
				break;
			}
			case 1:
				array7 = Resources.Load<TextAsset>("Data/TattleList").ToString().Split('\n');
				array = Resources.Load<TextAsset>("Data/Dialogues" + languageid + "/EnemyTattle").ToString().Split('\n');
				break;
			case 2:
				array7 = Resources.Load<TextAsset>("Data/CookOrder").ToString().Split('\n');
				array = Resources.Load<TextAsset>("Data/CookLibrary").ToString().Split('\n');
				break;
			case 3:
			{
				array7 = Resources.Load<TextAsset>("Data/SynopsisOrder").ToString().Split('\n');
				array = Resources.Load<TextAsset>("Data/Dialogues" + languageid + "/Synopsis").ToString().Split('\n');
				List<string> list = new List<string>();
				achiveicons = new int[librarylimit[3]];
				for (int num8 = 0; num8 < librarylimit[3]; num8++)
				{
					string[] array8 = array7[num8].Split(',');
					achiveicons[num8] = Convert.ToInt32(array8[1]);
					list.Add(array8[0]);
				}
				array7 = list.ToArray();
				break;
			}
			case 4:
				array7 = new string[0];
				break;
			}
			for (int num10 = 0; num10 < array.Length; num10++)
			{
				array2 = array[num10].Split('@');
				for (int num11 = 0; num11 < array2.Length; num11++)
				{
					librarydata[num7, num10, num11] = array2[num11];
				}
			}
			for (int num12 = 0; num12 < array7.Length; num12++)
			{
				libraryorder[num7, num12] = Convert.ToInt32(array7[num12]);
			}
		}
		boardquests = new List<int>[3];
		for (int num13 = 0; num13 < boardquests.Length; num13++)
		{
			boardquests[num13] = new List<int>();
			boardquests[num13].Add(0);
		}
		array = Resources.Load<TextAsset>("Data/Dialogues" + languageid + "/EnemyTattle").ToString().Split('\n');
		enemynames = new string[array.Length];
		for (int num14 = 0; num14 < array.Length; num14++)
		{
			enemynames[num14] = array[num14].Split('@')[0];
		}
		badges = new List<int[]>();
		SetUpBadges();
		statbonus = new List<int[]>();
		ChangeParty(new int[2] { 0, 1 }, true);
		enemyencounter = new int[256, 2];
		insideid = -1;
		tp = 10;
		maxtp = 10;
		basetp = 10;
		bp = 5;
		maxbp = bp;
		partylevel = 1;
		maxitems = 10;
		maxstorage = 35;
		camspeed = 0.1f;
		camanglespeed = 0.1f;
		flagvar = new int[70];
		flagvar[28] = 9500;
		flagvar[29] = 4500;
		flags = new bool[750];
		crystalbflags = new bool[50];
		vectorflags = new Vector3[10];
		regionalflags = new bool[100];
		flagstring = new string[15];
		camoffset = defaultcamoffset;
		camoffset2 = Vector3.zero;
		camangleoffset = defaultcamangle;
		librarystuff = new bool[librarydata.GetLength(0), librarydata.GetLength(1)];
		musicvolume = 1f;
		soundvolume = 1f;
		halt = false;
		instance.switchicon = NewUIObject("SwitchIcon", GUICamera.transform, new Vector3(-8f, 0f, 10f)).AddComponent<SpriteRenderer>();
		instance.switchicon.transform.localScale = Vector3.one * 0.75f;
		promptpick = -1;
		LoadItemSprites();
		InputIO.LoadSettings(false);
	}

	public static bool IsWithin(int value, int min, int max)
	{
		if (value >= min)
		{
			return value <= max;
		}
		return false;
	}

	public static void ChangeParty(int[] ids, bool fromscratch)
	{
		ChangeParty(ids, fromscratch, true);
	}

	public static void AddStatBonus(StatBonus type, int ammount, int to)
	{
		instance.statbonus.Add(new int[3]
		{
			(int)type,
			ammount,
			to
		});
	}

	public static void PushAway(Transform obj, Vector3 otherobj)
	{
		PushAway(obj, otherobj, 0.05f);
	}

	public static void PushAway(Transform obj, Vector3 otherobj, float value)
	{
		obj.transform.position += (obj.transform.position - otherobj).normalized * value;
	}

	private static void ResetStats()
	{
		for (int i = 0; i < instance.playerdata.Length; i++)
		{
			if (instance.playerdata[i].trueid == 1)
			{
				instance.playerdata[i].basehp = 9;
			}
			else
			{
				instance.playerdata[i].basehp = 7;
			}
			instance.playerdata[i].baseatk = 2;
			instance.playerdata[i].basedef = 0;
		}
		instance.basetp = 10;
	}

	public static void ApplyStatBonus()
	{
		ResetStats();
		if (instance.statbonus.Count <= 0)
		{
			return;
		}
		int[][] array = instance.statbonus.ToArray();
		int count = instance.statbonus.Count;
		for (int i = 0; i < count; i++)
		{
			if (array[i][2] == -1)
			{
				switch ((StatBonus)array[i][0])
				{
				case StatBonus.HP:
				{
					for (int k = 0; k < instance.playerdata.Length; k++)
					{
						instance.playerdata[k].basehp += array[i][1];
					}
					break;
				}
				case StatBonus.Attack:
				{
					for (int l = 0; l < instance.playerdata.Length; l++)
					{
						instance.playerdata[l].baseatk += array[i][1];
					}
					break;
				}
				case StatBonus.Defense:
				{
					for (int j = 0; j < instance.playerdata.Length; j++)
					{
						instance.playerdata[j].basedef += array[i][1];
					}
					break;
				}
				case StatBonus.TP:
					instance.basetp += array[i][1];
					break;
				}
				continue;
			}
			for (int m = 0; m < instance.playerdata.Length; m++)
			{
				if (instance.playerdata[m].trueid == array[i][2])
				{
					switch ((StatBonus)array[i][0])
					{
					case StatBonus.HP:
						instance.playerdata[m].basehp += array[i][1];
						break;
					case StatBonus.Attack:
						instance.playerdata[m].baseatk += array[i][1];
						break;
					case StatBonus.Defense:
						instance.playerdata[m].basedef += array[i][1];
						break;
					}
				}
			}
		}
		ApplyBadges();
	}

	public static bool HasPlayer(int id)
	{
		for (int i = 0; i < instance.playerdata.Length; i++)
		{
			if (instance.playerdata[i].trueid == id)
			{
				return true;
			}
		}
		return false;
	}

	public static void ChangeParty(int[] ids, bool fromscratch, bool destroyoldentity)
	{
		EntityControl[] array = null;
		if (instance.playerdata != null && instance.playerdata.Length != 0)
		{
			array = new EntityControl[instance.playerdata.Length];
			for (int i = 0; i < instance.playerdata.Length; i++)
			{
				array[i] = instance.playerdata[i].entity;
			}
		}
		int[,] array2 = null;
		if (!fromscratch)
		{
			array2 = new int[instance.playerdata.Length, 8];
			for (int j = 0; j < instance.playerdata.Length; j++)
			{
				array2[j, 0] = instance.playerdata[j].trueid;
				array2[j, 1] = instance.playerdata[j].hp;
				array2[j, 2] = instance.playerdata[j].maxhp;
				array2[j, 3] = instance.playerdata[j].basehp;
				array2[j, 4] = instance.playerdata[j].atk;
				array2[j, 5] = instance.playerdata[j].baseatk;
				array2[j, 6] = instance.playerdata[j].def;
				array2[j, 7] = instance.playerdata[j].basedef;
			}
		}
		else
		{
			instance.partyorder = ids;
		}
		List<BattleData> list = new List<BattleData>();
		if (fromscratch)
		{
			for (int k = 0; k < ids.Length; k++)
			{
				list.Add(SetDefaultStats(ids[k]));
			}
		}
		else if (array2 != null)
		{
			for (int l = 0; l < ids.Length; l++)
			{
				for (int m = 0; m < 0; m++)
				{
					BattleData item = SetDefaultStats(ids[l]);
					if (item.trueid == array2[m, 0])
					{
						item.hp = array2[m, 1];
						item.maxhp = array2[m, 2];
						item.basehp = array2[m, 3];
						item.atk = array2[m, 4];
						item.baseatk = array2[m, 5];
						item.def = array2[m, 6];
						item.basedef = array2[m, 7];
						break;
					}
					list.Add(item);
				}
			}
		}
		instance.partyorder = ids;
		instance.playerdata = list.ToArray();
		if (array != null)
		{
			for (int n = 0; n < array.Length; n++)
			{
				bool flag = false;
				for (int num = 0; num < instance.playerdata.Length; num++)
				{
					if (!(array[n] != null))
					{
						continue;
					}
					if (instance.playerdata[num].animid == array[n].animid)
					{
						instance.playerdata[num].entity = array[n];
						if (num == 0)
						{
							instance.playerdata[num].entity.tag = "Player";
							instance.playerdata[num].entity.gameObject.layer = 10;
						}
						else
						{
							instance.playerdata[num].entity.tag = "PFollower";
							instance.playerdata[num].entity.gameObject.layer = 9;
						}
						flag = true;
						break;
					}
					if (flag)
					{
						continue;
					}
					if (destroyoldentity)
					{
						UnityEngine.Object.Destroy(array[n].gameObject);
					}
					else if (map != null && array[n] != null && map != null)
					{
						array[n].transform.parent = map.transform;
						array[n].tag = "Untagged";
						EntityControl[] array3 = new EntityControl[map.entities.Length + 1];
						for (int num2 = 0; num2 < map.entities.Length; num2++)
						{
							array3[num2] = map.entities[num2];
						}
						array3[map.entities.Length] = array[n];
						map.entities = array3;
					}
				}
			}
			if (instance.playerdata != null && instance.playerdata.Length != 0 && instance.playerdata[0].entity != null && instance.playerdata[0].entity.GetComponent<PlayerControl>() == null)
			{
				instance.playerdata[0].entity.gameObject.AddComponent<PlayerControl>();
				PlayerControl player2 = player;
				UnityEngine.Object.Destroy(player);
			}
		}
		ApplyBadges();
		ApplyStatBonus();
		if (fromscratch)
		{
			for (int num3 = 0; num3 < instance.playerdata.Length; num3++)
			{
				instance.playerdata[num3].hp = instance.playerdata[num3].maxhp;
			}
		}
		RebuildHUD();
	}

	public static bool PartyLowHP()
	{
		for (int i = 0; i < instance.playerdata.Length; i++)
		{
			if (instance.playerdata[i].hp <= 4)
			{
				return true;
			}
		}
		return false;
	}

	public static void RefreshWind(Renderer t)
	{
		t.material.SetFloat("_ShakeDisplacement", UnityEngine.Random.Range(map.windspeed / 2f, map.windspeed));
		t.material.SetFloat("_ShakeBending", UnityEngine.Random.Range(map.windintensity / 2f, map.windintensity));
		t.material.SetFloat("_ShakeTime", UnityEngine.Random.Range(0.075f, 0.25f));
	}

	private static BattleData SetDefaultStats(int id)
	{
		BattleData result = new BattleData
		{
			animid = id
		};
		if (result.animid == 1)
		{
			result.hp = 9;
		}
		else
		{
			result.hp = 7;
		}
		result.trueid = result.animid;
		result.atk = 2;
		result.maxhp = result.hp;
		result.basehp = result.hp;
		result.baseatk = result.atk;
		result.skills = new List<int>();
		result.cursoroffset = new Vector3(0f, 2.3f);
		result.entityname = menutext[46 + result.animid];
		result.condition = new List<int[]>();
		return result;
	}

	public static void HurtParticle(Vector3 pos, bool playsound)
	{
		HitPart(pos);
		if (playsound)
		{
			PlaySound("Hurt");
		}
	}

	public static BattleData GetPlayerData(int id, bool frombattleentity)
	{
		if (frombattleentity)
		{
			for (int i = 0; i < instance.playerdata.Length; i++)
			{
				if (instance.playerdata[i].battleentity != null && instance.playerdata[i].battleentity.battleid == id)
				{
					return instance.playerdata[i];
				}
			}
		}
		return GetPlayerData(id);
	}

	public static BattleData GetPlayerData(int id)
	{
		for (int i = 0; i < instance.playerdata.Length; i++)
		{
			if (instance.playerdata[i].trueid == id)
			{
				return instance.playerdata[i];
			}
		}
		return instance.playerdata[0];
	}

	public static BattleData? GetPlayerDataNullable(int id)
	{
		for (int i = 0; i < instance.playerdata.Length; i++)
		{
			if (instance.playerdata[i].trueid == id)
			{
				return instance.playerdata[i];
			}
		}
		return null;
	}

	public static void AddPrizeMedal(int id)
	{
		if (BadgeIsEquipped(11) || instance.flags[614])
		{
			instance.flagvar[instance.prizeflags[id]] = 1;
			instance.flags[56] = true;
		}
		else
		{
			instance.flagvar[instance.prizeflags[id]] = 2;
		}
		instance.flagvar[55]++;
	}

	public static int HasBadge(int badgeid)
	{
		int num = 0;
		for (int i = 0; i < instance.badges.Count; i++)
		{
			if (instance.badges[i][0] == badgeid)
			{
				num++;
			}
		}
		return num;
	}

	public void SetUpBadges()
	{
		badgeshops[0].AddRange(new int[10] { 0, 1, 7, 12, 30, 86, 84, 87, 88, 81 });
		badgeshops[1].AddRange(new int[5] { 19, 6, 9, 43, 42 });
		for (int i = 0; i < badgeshops.Length; i++)
		{
			int[] array = badgeshops[i].ToArray();
			for (int j = 0; j < array.Length; j++)
			{
				avaliablebadgepool[i].Add(array[j]);
			}
		}
	}

	public static void AddQuest(int id)
	{
		ChangeBoardQuest(id, 0);
	}

	public static void CheckQuests()
	{
		for (int i = 0; i < questchecks.Length; i++)
		{
			if (HasQuest(i) || questchecks[i][0] == 0)
			{
				continue;
			}
			bool[] array = new bool[questchecks[i].Length];
			for (int j = 0; j < array.Length; j++)
			{
				if (questchecks[i][j] > 0)
				{
					array[j] = instance.flags[questchecks[i][j]];
				}
				else
				{
					array[j] = instance.librarystuff[4, Mathf.Abs(questchecks[i][j])];
				}
			}
			if (CheckAllBool(array, true))
			{
				AddQuest(i);
			}
		}
	}

	public static IEnumerator TempIgnoreCollision(Collider a, Collider b, float seconds)
	{
		Physics.IgnoreCollision(a, b, true);
		yield return new WaitForSeconds(seconds);
		if (a != null && b != null)
		{
			Physics.IgnoreCollision(a, b, false);
		}
	}

	public static void LaunchObject(Transform obj, Vector3 push)
	{
		LaunchObject(obj.GetComponent<Rigidbody>(), push, true);
	}

	public static void LaunchObject(Rigidbody r, Vector3 push, bool gravity)
	{
		r.velocity = push;
		r.isKinematic = false;
		r.useGravity = gravity;
	}

	public static void UpdateArea(int newarea)
	{
		if (instance.areaid == 15 && instance.flags[596])
		{
			instance.flags[597] = true;
		}
		instance.regionalflags = new bool[100];
		instance.areaid = newarea;
		instance.librarystuff[4, newarea] = true;
	}

	public static void UpdateShops()
	{
		for (int i = 0; i < instance.avaliablebadgepool.Length; i++)
		{
			instance.avaliablebadgepool[i] = instance.badgeshops[i];
			RandomSort(ref instance.avaliablebadgepool[i]);
			if (i == 0 && instance.flags[587] && instance.badgeshops[i].Count > 0)
			{
				instance.flags[587] = false;
			}
		}
	}

	public static bool ObjectsAreActive(int[] objects, bool any)
	{
		List<NPCControl> list = new List<NPCControl>();
		for (int i = 0; i < objects.Length; i++)
		{
			list.Add(map.entities[objects[i]].npcdata);
		}
		return ObjectsAreActive(list.ToArray(), any);
	}

	public static bool ObjectsAreActive(NPCControl[] objects, bool any)
	{
		if (any)
		{
			for (int i = 0; i < objects.Length; i++)
			{
				if (objects[i].hit)
				{
					return true;
				}
			}
			return false;
		}
		for (int j = 0; j < objects.Length; j++)
		{
			if (!objects[j].hit)
			{
				return false;
			}
		}
		return true;
	}

	public static void RandomSort(ref List<int> array)
	{
		int[] array2 = array.ToArray();
		for (int i = 0; i < array2.Length; i++)
		{
			int num = array2[i];
			int num2 = UnityEngine.Random.Range(i, array2.Length);
			array2[i] = array2[num2];
			array2[num2] = num;
		}
		array = new List<int>(array2);
	}

	public static void RandomSort(ref int[] array)
	{
		for (int i = 0; i < array.Length; i++)
		{
			int num = array[i];
			int num2 = UnityEngine.Random.Range(i, array.Length);
			array[i] = array[num2];
			array[num2] = num;
		}
	}

	public static IEnumerator FlipSpriteBool(SpriteRenderer sprite, bool x, bool y, float everyxframes, float duringxframes)
	{
		float a = 0f;
		float b = 0f;
		do
		{
			b += framestep;
			if (b >= everyxframes)
			{
				b = 0f;
				if (x)
				{
					sprite.flipX = !sprite.flipX;
				}
				if (y)
				{
					sprite.flipY = !sprite.flipY;
				}
			}
			if (duringxframes > 0f)
			{
				a += framestep;
			}
			yield return null;
		}
		while (duringxframes == -1f || a < duringxframes);
	}

	public void DoClock()
	{
		if (instance.flagvar[37] == 0)
		{
			instance.flagvar[37] = 8;
		}
		clocksec++;
		if (clocksec % 5 == 0 && !roomtransition)
		{
			Resources.UnloadUnusedAssets();
			GC.Collect();
		}
		if (clocksec == 60)
		{
			clockmin++;
			clocksec = 0;
			if ((clockmin == 60 || clockmin == 30) && flags[254] && flagvar[26] > 0)
			{
				flagvar[26] = Mathf.Clamp(flagvar[26] + Mathf.Clamp(Mathf.FloorToInt((float)flagvar[26] * (flags[630] ? 0.04f : 0.02f)), 1, 75), 0, 10000);
			}
			if (clockmin == 60)
			{
				if (clockhour < 9999)
				{
					clockhour++;
				}
				clockmin = 0;
			}
		}
		if (timeddemo)
		{
			if (demotimer >= 900 && FreePlayer() && battle == null)
			{
				events.StartEvent(187, null);
			}
			else
			{
				demotimer++;
			}
		}
		if (player != null && battle == null)
		{
			if (FreePlayer())
			{
				RefreshPlayer();
			}
			if (!instance.flags[377] && items[0].Contains(121))
			{
				instance.flags[377] = true;
			}
		}
	}

	public void RefreshPlayer(bool onlycollider = false)
	{
		player.ceiling = false;
		if (!onlycollider)
		{
			for (int i = 0; i < instance.playerdata.Length; i++)
			{
				if (!instance.playerdata[i].entity.noclock)
				{
					instance.playerdata[i].entity.onground = false;
					instance.playerdata[i].entity.transform.parent = null;
				}
			}
		}
		if (battle == null && !inevent)
		{
			player.entity.ForceHitWall();
		}
	}

	public static bool IsParty(Transform obj)
	{
		for (int i = 0; i < instance.playerdata.Length; i++)
		{
			if (instance.playerdata[i].entity.transform == obj)
			{
				return true;
			}
		}
		return false;
	}

	public static bool FreePlayer(bool getfly)
	{
		if (player != null && !instance.minipause && !instance.inevent && !instance.message && !instance.pause && !player.digging)
		{
			if (getfly)
			{
				return !player.flying;
			}
			return true;
		}
		return false;
	}

	public static bool FreePlayer()
	{
		return FreePlayer(true);
	}

	public void CheckAchievement()
	{
		if (!started)
		{
			return;
		}
		if (!flags[63] && HowManyTrue(GetLibraryBools(3)) >= librarylimit[3] - 1)
		{
			UpdateJounal(Library.Logbook, 27);
			flags[63] = true;
		}
		if (!librarystuff[3, 11] && instance.flags[410])
		{
			UpdateJounal(Library.Logbook, 11);
		}
		if (!librarystuff[3, 5] && boardquests[2].Contains(11))
		{
			UpdateJounal(Library.Logbook, 5);
		}
		if (!librarystuff[3, 12] && boardquests[2].Contains(12))
		{
			UpdateJounal(Library.Logbook, 12);
		}
		if (!librarystuff[3, 14] && boardquests[2].Contains(13))
		{
			UpdateJounal(Library.Logbook, 14);
		}
		if (!librarystuff[3, 16] && boardquests[2].Contains(14))
		{
			UpdateJounal(Library.Logbook, 16);
		}
		if (!librarystuff[3, 18] && boardquests[2].Contains(15))
		{
			UpdateJounal(Library.Logbook, 18);
		}
		if (!librarystuff[3, 20] && boardquests[2].Contains(16))
		{
			UpdateJounal(Library.Logbook, 20);
		}
		if (!librarystuff[3, 22] && boardquests[2].Contains(17))
		{
			UpdateJounal(Library.Logbook, 22);
		}
		if (!librarystuff[3, 29] && flagvar[28] > 9500 && flagvar[29] > 4500)
		{
			UpdateJounal(Library.Logbook, 29);
		}
		if (!librarystuff[3, 26] && boardquests[2].Contains(26))
		{
			UpdateJounal(Library.Logbook, 26);
		}
		if (!librarystuff[3, 25] && boardquests[2].Contains(30))
		{
			UpdateJounal(Library.Logbook, 25);
		}
		if (!librarystuff[3, 1] && CrystalBerryAmmount() >= 50)
		{
			UpdateJounal(Library.Logbook, 1);
		}
		if (!librarystuff[3, 2] && badges.Count >= 120)
		{
			UpdateJounal(Library.Logbook, 2);
		}
		if (!librarystuff[3, 3] && SamiraGotAll())
		{
			UpdateJounal(Library.Logbook, 3);
		}
		if (!librarystuff[3, 4] && instance.partylevel >= 27)
		{
			UpdateJounal(Library.Logbook, 4);
		}
		if (!librarystuff[3, 7] && boardquests[2].Count >= Enum.GetNames(typeof(BoardQuests)).Length - 1)
		{
			UpdateJounal(Library.Logbook, 7);
		}
		if (!librarystuff[3, 8] && HowManyTrue(GetLibraryBools(2)) >= librarylimit[2])
		{
			UpdateJounal(Library.Logbook, 8);
		}
		if (!librarystuff[3, 9] && HowManyTrue(GetLibraryBools(1)) >= librarylimit[1])
		{
			UpdateJounal(Library.Logbook, 9);
		}
		if (!librarystuff[3, 10] && HowManyTrue(GetLibraryBools(0)) >= librarylimit[0])
		{
			UpdateJounal(Library.Logbook, 10);
		}
		if (!librarystuff[3, 28] && instance.flags[612])
		{
			UpdateJounal(Library.Logbook, 28);
		}
		if (!librarystuff[3, 0] && instance.flags[610])
		{
			UpdateJounal(Library.Logbook, 0);
		}
		if (!librarystuff[3, 24] && instance.flags[341])
		{
			UpdateJounal(Library.Logbook, 24);
		}
		if (instance.flags[546] && !instance.librarystuff[0, 36])
		{
			UpdateJounal(Library.Discovery, 36);
		}
		for (int i = 0; i < librarylimit[3]; i++)
		{
			if (librarystuff[3, i])
			{
				InputIO.Achivement(i);
			}
		}
		if (librarystuff[3, 7])
		{
			instance.flags[671] = true;
		}
	}

	public static bool SamiraGotAll()
	{
		return PurchasedMusicAmmount() >= Enum.GetNames(typeof(Musics)).Length - 8;
	}

	public static int PurchasedMusicAmmount()
	{
		int num = 0;
		int[][] array = instance.samiramusics.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i][1] > -1)
			{
				num++;
			}
		}
		return num;
	}

	public static bool[] GetLibraryBools(int page)
	{
		bool[] array = new bool[instance.librarystuff.GetLength(1)];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = instance.librarystuff[page, i];
		}
		return array;
	}

	public static int HowManyTrue(bool[] array)
	{
		int num = 0;
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				num++;
			}
		}
		return num;
	}

	public static bool InCameraRange(Transform obj)
	{
		return InCameraRange(MainCamera.WorldToViewportPoint(obj.transform.position));
	}

	public static bool InCameraRange(Vector3 t)
	{
		if (t.x > 0f - entityactive.x && t.x < entityactive.x + 1f && t.y > 0f - entityactive.y && t.y < entityactive.y + 1f)
		{
			return t.z > entityactive.z;
		}
		return false;
	}

	public static AudioSource PlaySound(AudioClip soundclip, int id)
	{
		return PlaySound(soundclip, id, 1f);
	}

	public static AudioSource PlaySound(AudioClip soundclip, int id, float pitch)
	{
		return PlaySound(soundclip, id, pitch, 1f, false);
	}

	public static AudioSource PlaySound(AudioClip soundclip, int id, float pitch, float volume)
	{
		return PlaySound(soundclip, id, pitch, volume, false);
	}

	public static AudioSource PlaySound(string soundclip, int id, float pitch, float volume)
	{
		return PlaySound(Resources.Load<AudioClip>("Audio/Sounds/" + soundclip), id, pitch, volume, false);
	}

	public static AudioSource PlaySound(string clip, float pitch, float volume)
	{
		return PlaySound(Resources.Load<AudioClip>("Audio/Sounds/" + clip), -1, pitch, volume, false);
	}

	public static AudioSource PlaySound(string clip, float volume)
	{
		return PlaySound(Resources.Load<AudioClip>("Audio/Sounds/" + clip), -1, 1f, volume, false);
	}

	public static AudioSource PlaySound(string soundclip, int id, float pitch, float volume, bool loop)
	{
		return PlaySound(Resources.Load<AudioClip>("Audio/Sounds/" + soundclip), id, pitch, volume, loop);
	}

	private static int GetFreeSound()
	{
		int result = sounds.Length - 1;
		for (int i = 0; i < sounds.Length - 1; i++)
		{
			if (!sounds[i].isPlaying)
			{
				result = i;
				break;
			}
		}
		return result;
	}

	public static AudioSource PlaySound(AudioClip soundclip, int id, float pitch, float volume, bool loop)
	{
		if (SoundVolume())
		{
			if (id == -1)
			{
				id = GetFreeSound();
			}
			sounds[id].clip = soundclip;
			sounds[id].volume = soundvolume * volume;
			sounds[id].pitch = pitch;
			sounds[id].loop = loop;
			sounds[id].Play();
			lastsoundid = id;
			return sounds[id];
		}
		return null;
	}

	public static void StopSound(int clip)
	{
		StopSound(clip, 0.1f);
	}

	public static void StopSound(int clip, float delay)
	{
		if (SoundVolume())
		{
			if (delay > 0f)
			{
				instance.StartCoroutine(FadeSound(sounds[clip], delay));
			}
			else
			{
				sounds[clip].Stop();
			}
		}
	}

	public static bool SoundVolume()
	{
		if (!(pausemenu == null) || !(soundvolume > 0f))
		{
			if (pausemenu != null)
			{
				if (pausemenu.windowid == 4)
				{
					return pausemenu.svolume > 0f;
				}
				return true;
			}
			return false;
		}
		return true;
	}

	public static void StopSound(AudioClip clip, float delay)
	{
		if (!SoundVolume())
		{
			return;
		}
		for (int i = 0; i < sounds.Length; i++)
		{
			if (sounds[i].clip == clip)
			{
				if (delay > 0f)
				{
					instance.StartCoroutine(FadeSound(sounds[i], delay));
				}
				else
				{
					sounds[i].Stop();
				}
			}
		}
	}

	public static void StopSound(AudioClip clip)
	{
		StopSound(clip, 0f);
	}

	public static void StopSound(string clipname)
	{
		StopSound(Resources.Load<AudioClip>("Audio/Sounds/" + clipname), 0f);
	}

	public static void StopSound(string clipname, float delay)
	{
		StopSound(Resources.Load<AudioClip>("Audio/Sounds/" + clipname), delay);
	}

	public static Vector3 ClampVectorBox(Vector3 input, Vector3 limitspos, Vector3 limitsneg)
	{
		return new Vector3(Mathf.Clamp(input.x, limitsneg.x, limitspos.x), Mathf.Clamp(input.y, limitsneg.y, limitspos.y), Mathf.Clamp(input.z, limitsneg.z, limitspos.z));
	}

	public static Vector3 ClampVectorBox(Vector3 input, Vector3 limits)
	{
		return ClampVectorBox(input, limits, -limits);
	}

	public static AudioSource PlaySound(AudioClip soundclip)
	{
		return PlaySound(soundclip, -1);
	}

	public static AudioSource PlaySound(string soundclip, int id)
	{
		return PlaySound(Resources.Load<AudioClip>("Audio/Sounds/" + soundclip), id);
	}

	public static AudioSource PlaySound(string soundclip)
	{
		return PlaySound(Resources.Load<AudioClip>("Audio/Sounds/" + soundclip));
	}

	public static Vector3 ClampMagnitude(Vector3 v, float max, float min)
	{
		double num = v.sqrMagnitude;
		if (num > (double)max * (double)max)
		{
			return v.normalized * max;
		}
		if (num < (double)min * (double)min)
		{
			return v.normalized * min;
		}
		return v;
	}

	public static IEnumerator LerpObject(Transform obj, Vector3 position, float speed, bool destroyonend)
	{
		Vector3 start = obj.transform.position;
		float a = 0f;
		while (a < 1f && obj != null)
		{
			obj.transform.position = Vector3.Lerp(start, position, a);
			a += TieFramerate(speed);
			yield return null;
		}
		if (destroyonend && obj != null)
		{
			UnityEngine.Object.Destroy(obj.gameObject);
		}
	}

	public static void ChangeMusicVolume(float frametime)
	{
		ChangeMusicVolume(musicchannel, musicvolume, frametime);
	}

	public static void ChangeMusicVolume(float targetvolume, float frametime)
	{
		ChangeMusicVolume(musicchannel, targetvolume, frametime);
	}

	public static void ChangeMusicVolume(int clipid, float targetvolume, float frametime)
	{
		if (musiccoroutine != null)
		{
			instance.StopCoroutine(musiccoroutine);
		}
		musiccoroutine = instance.StartCoroutine(ChangeMVolume(clipid, targetvolume, frametime));
	}

	private static IEnumerator ChangeMVolume(int clipid, float volume, float frametime)
	{
		float sv = music[clipid].volume;
		for (float a = 0f; a < frametime; a += TieFramerate(1f))
		{
			music[clipid].volume = Mathf.Lerp(sv, volume, a / frametime);
			yield return null;
		}
		yield return null;
		music[clipid].volume = volume;
	}

	private static IEnumerator SwitchMusic(AudioClip musicclip, float fadespeed, int id)
	{
		musiccoroutine = instance.StartCoroutine(SwitchMusic(musicclip, fadespeed, id, false));
		yield return null;
	}

	private static IEnumerator SwitchMusic(AudioClip musicclip, float fadespeed, int id, bool seamless)
	{
		if (music[id].clip != musicclip)
		{
			float t = 1f;
			int sid = -1;
			if (seamless)
			{
				sid = GetFreeSound();
				sounds[sid].clip = musicclip;
				sounds[sid].time = music[id].time;
				sounds[sid].volume = 0f;
				sounds[sid].loop = true;
				sounds[sid].Play();
			}
			if (t > 0f && music[id].clip != null)
			{
				float failsafe = 0f;
				while (t > 0.045f && failsafe < 500f)
				{
					t = Mathf.Lerp(t, 0f, framestep * fadespeed);
					music[id].volume = musicvolume * t;
					switch ((Musics)Enum.Parse(typeof(Musics), music[id].clip.name))
					{
					case Musics.Theater:
					case Musics.Tension:
					case Musics.Venus:
					case Musics.Battle3:
					case Musics.Bee:
					case Musics.TermiteLoop:
					case Musics.Pier:
						music[id].volume *= 0.9f;
						break;
					}
					if (seamless)
					{
						sounds[sid].volume = musicvolume - music[id].volume;
						if (Mathf.Abs(sounds[sid].time - music[id].time) > 0.15f)
						{
							sounds[sid].time = music[id].time;
						}
					}
					failsafe += framestep;
					yield return null;
				}
				music[id].volume = 0f;
			}
			yield return new WaitForSeconds(0.1f);
			if (musicclip != null)
			{
				music[id].clip = musicclip;
				if (!seamless)
				{
					music[id].time = 0f;
					yield return null;
				}
				else
				{
					music[id].time = sounds[sid].time;
					sounds[sid].Stop();
					sounds[sid].time = 0f;
					sounds[sid].loop = false;
				}
				if (musicresume >= 0f)
				{
					music[id].time = musicresume;
					musicresume = -1f;
				}
				music[id].volume = musicvolume;
				music[id].Play();
			}
			else
			{
				music[id].clip = null;
				music[id].Stop();
			}
		}
		if ((bool)battle || instance.inevent || (map != null && map.musicid > -1 && map.musicid < musicids.Length))
		{
			CheckSamira(music[id].clip);
		}
		yield return null;
		musiccoroutine = null;
	}

	public static void CheckSamira(AudioClip music)
	{
		if (!(music != null))
		{
			return;
		}
		int num = (int)Enum.Parse(typeof(Musics), music.name);
		int[][] array = instance.samiramusics.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i][0] == num)
			{
				return;
			}
		}
		instance.samiramusics.Add(new int[2] { num, -1 });
	}

	public static void CheckSamira(string music)
	{
		if (music == null)
		{
			return;
		}
		int num = (int)Enum.Parse(typeof(Musics), music);
		int[][] array = instance.samiramusics.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i][0] == num)
			{
				return;
			}
		}
		instance.samiramusics.Add(new int[2] { num, -1 });
	}

	public static void ChangeMusic(AudioClip musicclip, float fadespeed, int id)
	{
		ChangeMusic(musicclip, fadespeed, id, false);
	}

	public static void ChangeMusic(string musicclip, float fadespeed, int id, bool seamless)
	{
		ChangeMusic(Resources.Load<AudioClip>("Audio/Music/" + musicclip), fadespeed, id, seamless);
	}

	public static void ChangeMusic(AudioClip musicclip, float fadespeed, int id, bool seamless)
	{
		if (musiccoroutine != null)
		{
			instance.StopCoroutine(musiccoroutine);
		}
		musiccoroutine = instance.StartCoroutine(SwitchMusic(musicclip, fadespeed, id, seamless));
		if (musicclip != null)
		{
			musicids[id] = (int)Enum.Parse(typeof(Musics), musicclip.name);
		}
		else
		{
			musicids[id] = -1;
		}
		if (musicids[id] > -1)
		{
			lastmusic = musicids[id];
		}
	}

	public static void ChangeMusic(string musicclip, float fadespeed)
	{
		ChangeMusic(Resources.Load<AudioClip>("Audio/Music/" + musicclip), fadespeed, 0);
	}

	public static int SoundIsPlaying(string name)
	{
		for (int i = 0; i < sounds.Length; i++)
		{
			if (sounds[i].isPlaying && sounds[i].clip != null && sounds[i].clip.name == name)
			{
				return i;
			}
		}
		return -1;
	}

	public static void ChangeMusic(AudioClip musicclip, float fadespeed)
	{
		ChangeMusic(musicclip, fadespeed, 0);
	}

	public static void ChangeMusic(string musicclip, float fadespeed, int id)
	{
		ChangeMusic(Resources.Load<AudioClip>("Audio/Music/" + musicclip), fadespeed, id);
	}

	public static void ChangeMusic()
	{
		if (map != null && map.music != null && map.music.Length != 0 && map.musicid > -1)
		{
			ChangeMusic(map.music[map.musicid]);
		}
		else
		{
			ChangeMusic((AudioClip)null);
		}
	}

	public static void ChangeMusic(int mapid)
	{
		ChangeMusic(map.music[mapid]);
	}

	public static void ChangeMusic(string musicclip)
	{
		if (musicclip == null)
		{
			ChangeMusic((AudioClip)null, 0.1f);
		}
		else
		{
			ChangeMusic(Resources.Load<AudioClip>("Audio/Music/" + musicclip), 0.1f, 0);
		}
	}

	public static void ChangeMusic(AudioClip musicclip)
	{
		ChangeMusic(musicclip, 0.075f);
	}

	private static bool AnyJoyKey(bool hold)
	{
		for (int i = 0; i < 20; i++)
		{
			if (!hold && Input.GetKeyDown((KeyCode)(330 + i)))
			{
				return true;
			}
			if (hold && Input.GetKey((KeyCode)(330 + i)))
			{
				return true;
			}
		}
		return false;
	}

	private static bool AnyJoyStick()
	{
		if (InputIO.JoyStick(0) == 0f && InputIO.JoyStick(2) == 0f && InputIO.JoyStick(1) == 0f)
		{
			return InputIO.JoyStick(3) != 0f;
		}
		return true;
	}

	private void GetJoystick()
	{
		if (usejoystick > 0 || InputIO.IsConsole)
		{
			if (!InputIO.IsConsole && Input.anyKeyDown && !AnyJoyKey(false) && !AnyJoyStick())
			{
				joystick = false;
			}
			else if (forcecontrollerupdate || (!joystick && (InputIO.IsConsole || AnyJoyKey(false) || AnyJoyStick())))
			{
				forcecontrollerupdate = false;
				joystick = true;
				if (usejoystick == 4)
				{
					joyid = forcejoystick;
				}
				else if (usejoystick == 2)
				{
					joyid = 0;
				}
				else if (usejoystick < 5)
				{
					switch (Application.platform)
					{
					default:
					{
						string[] array = Controllers();
						for (int i = 0; i < array.Length; i++)
						{
							if (usejoystick < 3 || i == forcejoystick)
							{
								if (array[i].Contains("Fight Pad Pro"))
								{
									joyid = 7;
								}
								else if (array[i] == "Bluetooth Gamepad   " || array[i].Contains("PC/PS3/Android"))
								{
									joyid = 6;
								}
								else if (array[i].Contains("ðñò"))
								{
									joyid = 5;
								}
								else if (array[i] == "Wireless Gamepad")
								{
									joyid = 2;
								}
								else if (!array[i].Contains("Xbox") && !array[i].Contains("XBOX") && (array[i].Contains("Wireless") || array[i].Contains("PS4") || array[i].Contains("Sony") || array[i].Contains("tation") || array[i].Contains("PLAYSTATION") || array[i].Contains("3 TURBO")))
								{
									joyid = 1;
								}
								else
								{
									joyid = 0;
								}
								MonoBehaviour.print(array[i]);
								break;
							}
						}
						break;
					}
					case RuntimePlatform.Switch:
						joyid = 2;
						break;
					case RuntimePlatform.PS4:
						joyid = 1;
						break;
					case RuntimePlatform.XboxOne:
						joyid = 0;
						break;
					}
				}
				InputIO.GetJoyButtons();
			}
		}
		else
		{
			joystick = false;
		}
		if (joystick && !InputIO.IsConsole)
		{
			Screen.sleepTimeout = -1;
		}
		else
		{
			Screen.sleepTimeout = -2;
		}
	}

	private static void ShowBackDialogue()
	{
		DestroyAllChildren(instance.textbox);
		string text = diagstring[currentdialogue];
		if (battle != null)
		{
			text = text.Replace("   ", "").Replace("  ", "");
		}
		if (text[0] == ' ')
		{
			text = text.Remove(0);
		}
		instance.StartCoroutine(SetText("|color,7|" + text, Vector3.zero, instance.textbox));
	}

	private static bool HasSkillCost(int tpcost, int playerid)
	{
		if (BadgeIsEquipped(72, instance.playerdata[playerid].trueid) || tpcost < 0)
		{
			return instance.playerdata[playerid].hp - 1 >= Mathf.Abs(tpcost);
		}
		return instance.tp >= tpcost;
	}

	public static IEnumerator Spin(Transform obj, Vector3 target, float frametime, bool smooth)
	{
		float a = 0f;
		Vector3 s = obj.eulerAngles;
		do
		{
			if (smooth)
			{
				obj.eulerAngles = SmoothLerp(s, target, a / frametime);
			}
			else
			{
				obj.eulerAngles = Vector3.Lerp(s, target, a / frametime);
			}
			a += TieFramerate(1f);
			yield return null;
		}
		while (a < frametime + 1f);
	}

	public static bool KeyHold(Directions direction)
	{
		if (GetKey((int)direction, true))
		{
			if (keyhold[0] > 15f)
			{
				if (keyhold[1] >= 7f)
				{
					keyhold[1] = 0f;
					return true;
				}
				keyhold[1] += framestep;
			}
			else
			{
				keyhold[0] += framestep;
			}
		}
		return false;
	}

	public static void ResetKeyHold()
	{
		if (!GetKey(0, true) && !GetKey(1, true) && !GetKey(2, true) && !GetKey(3, true))
		{
			keyhold[0] = 0f;
			keyhold[1] = 0f;
		}
	}

	private static bool MultiItem()
	{
		if (listtype == 2 || (listtype == 0 && instance.flags[349]) || listsell)
		{
			return instance.multiselect.Count > 0;
		}
		return false;
	}

	private static bool IsInMultiList()
	{
		if (listtype != 2 && (listtype != 0 || !instance.flags[349]))
		{
			return listsell;
		}
		return true;
	}

	public static bool IsPlayerInPos(int pos, Transform entity)
	{
		return instance.playerdata[battle.partypointer[pos]].battleentity.transform == entity;
	}

	private void Update()
	{
		if (!basicload)
		{
			return;
		}
		if (message)
		{
			if (!prompt && itemlist == null)
			{
				if (blinker != null)
				{
					if (waitinput)
					{
						blinker.enabled = true;
					}
					else
					{
						blinker.enabled = false;
					}
				}
				if (GetKey(5, true))
				{
					if (inputcooldown <= 0f && !noskip && !inlist && currentdialogue == diagstring.Count)
					{
						isholdingskip = true;
						if (waitinput)
						{
							PlaySound(Resources.Load<AudioClip>("Audio/Sounds/Confirm1"), -1, 0.6f, 0.5f);
							inputcooldown = 16f;
						}
						else
						{
							inputcooldown = 4f;
						}
						waitinput = false;
						skiptext = true;
					}
				}
				else if ((GetKey(4, false) || GetKey(5, false)) && !isholdingskip)
				{
					if (currentdialogue == diagstring.Count)
					{
						if (inputcooldown <= 0f)
						{
							if (waitinput)
							{
								waitinput = false;
								PlaySound(Resources.Load<AudioClip>("Audio/Sounds/Confirm1"), -1, 0.4f, 0.5f);
								inputcooldown = 16f;
							}
							else
							{
								skiptext = true;
							}
						}
					}
					else
					{
						inputcooldown = 16f;
						PlaySound(Resources.Load<AudioClip>("Audio/Sounds/Confirm1"), -1, 0.6f, 0.5f);
						currentdialogue++;
						if (currentdialogue == diagstring.Count)
						{
							DestroyText(instance.textbox);
							waitinput = false;
						}
						else
						{
							ShowBackDialogue();
						}
					}
				}
				else if (GetKey(6, false) && !notextbacktrack)
				{
					if (currentdialogue > 0 && waitinput && textbox != null && inputcooldown <= 0f)
					{
						inputcooldown = 16f;
						PlaySound("FunnyStep2", -1, 1f, 0.5f);
						if (currentdialogue == diagstring.Count && !backtracking)
						{
							diagstring.Add(OrganizeLines(tempdiag.Replace("  ", ""), linebr, fontdsize, fontdtype));
							ResetDiag();
							backtracking = true;
						}
						currentdialogue--;
						ShowBackDialogue();
					}
				}
				else if (!GetKey(5, true) && inputcooldown <= 0f)
				{
					isholdingskip = false;
				}
			}
			else if (prompt && inputcooldown <= 0f)
			{
				if (!numberprompt && cursor != null)
				{
					cursor.transform.localPosition = Vector3.Lerp(cursor.transform.localPosition, new Vector3(promptbox.GetChild(1).localPosition.x - 0.25f, promptbox.GetChild(1 + option).localPosition.y + 0.25f, 10f), TieFramerate(0.2f));
				}
				if (blinker != null)
				{
					blinker.enabled = false;
				}
				if (GetKey(0, false) || KeyHold(Directions.Up))
				{
					PlayScrollSound();
					if (!numberprompt)
					{
						option--;
						if (option < 0)
						{
							option = maxoptions - 1;
						}
					}
					else
					{
						if (instance.flagvar[0] == -555)
						{
							option = Mathf.Clamp(option - flagvar[1], 0, maxoptions - 1);
						}
						else
						{
							option = 0;
						}
						flagvar[5] = 0;
					}
				}
				else if (GetKey(1, false) || KeyHold(Directions.Down))
				{
					PlayScrollSound();
					if (!numberprompt)
					{
						option++;
						if (option >= maxoptions)
						{
							option = 0;
						}
					}
					else
					{
						if (instance.flagvar[0] == -555)
						{
							option = Mathf.Clamp(option + flagvar[1], 0, maxoptions - 1);
						}
						else
						{
							option = maxoptions - 1;
						}
						flagvar[5] = 1;
					}
				}
				else if (!GetKey(2, true) && !GetKey(3, true))
				{
					ResetKeyHold();
				}
				if (numberprompt)
				{
					if (GetKey(8) && inputcooldown <= 0f)
					{
						option = maxoptions - 1;
					}
					if (GetKey(6, false) && letterprompt > -1 && inputcooldown <= 0f)
					{
						ChangeLetterPrompt();
					}
					else if (GetKey(2, false) || KeyHold(Directions.Left))
					{
						PlayScrollSound();
						if (option > 0)
						{
							option--;
							if (instance.flagvar[0] == -555 && (option + 1) % flagvar[1] == 0 && option < maxoptions - 4)
							{
								option += flagvar[1];
							}
						}
						else if (instance.flagvar[0] == -555)
						{
							option += flagvar[1] - 1;
						}
						flagvar[5] = 2;
					}
					else if (GetKey(3, false) || KeyHold(Directions.Right))
					{
						PlayScrollSound();
						if (option < maxoptions + ((letterprompt > -1) ? (-1) : 0))
						{
							option++;
							if (instance.flagvar[0] == -555 && option % flagvar[1] == 0 && option < maxoptions - 1)
							{
								option -= flagvar[1];
							}
						}
						flagvar[5] = 3;
					}
					else if (!GetKey(0, true) && !GetKey(1, true))
					{
						ResetKeyHold();
					}
				}
				if (numberprompt && flagvar[0] == -555 && flagvar[5] > -1)
				{
					while (option < flagstring[1].Length - 1 && flagstring[1][option] == ' ')
					{
						switch (flagvar[5])
						{
						case 0:
							option -= flagvar[1];
							break;
						case 1:
							option += flagvar[1];
							break;
						case 2:
							option--;
							break;
						case 3:
							option++;
							break;
						}
						option = Mathf.Clamp(option, 0, maxoptions - 1);
					}
					flagvar[5] = -1;
				}
				else if (GetKey(4, false))
				{
					listcanceled = false;
					PlaySound("Confirm", -1);
					inputcooldown = 5f;
					lastPrompt = option;
					if (!numberprompt)
					{
						promptpick = option;
						prompt = false;
						skiptext = false;
					}
					else
					{
						promptpick = 0;
						if (flagvar[0] == -555)
						{
							if (option < maxoptions - 3)
							{
								if (flagstring[listtype].Length < flagvar[10])
								{
									if (letterprompt == 4)
									{
										if (option + 3 < koreanLimit[flagvar[6]].x || option + 3 > koreanLimit[flagvar[6]].y)
										{
											PlayBuzzer();
										}
										else
										{
											flagvar[6]++;
											if (flagvar[6] == 3)
											{
												flagvar[6] = 0;
												flagstring[listtype] += GetKoreanChar(new int[3]
												{
													koreanHL[0] - koreanLimit[0].x,
													koreanHL[1] - koreanLimit[1].x,
													option - koreanLimit[2].x + 3
												});
												koreanHL = new int[2] { -1, -1 };
												option = 0;
											}
											else
											{
												koreanHL[flagvar[6] - 1] = option + 3;
											}
											UpdateKoreanPrompt(flagvar[6]);
											RefreshNumberPrompt();
										}
									}
									else
									{
										flagstring[listtype] += flagstring[1][option];
										RefreshNumberPrompt();
									}
								}
								else
								{
									PlayBuzzer();
								}
							}
							else if (option == maxoptions - 3)
							{
								if (flagstring[listtype].Length > 0)
								{
									if (letterprompt == 4 && flagvar[6] > 0)
									{
										flagvar[6] = 0;
										koreanHL = new int[2] { -1, -1 };
										UpdateKoreanPrompt(0);
									}
									else
									{
										flagstring[listtype] = flagstring[listtype].Remove(flagstring[listtype].Length - 1);
									}
									RefreshNumberPrompt();
								}
								else
								{
									PlayBuzzer();
								}
							}
							else if (option == maxoptions - 2)
							{
								if (flagstring[listtype].Length < flagvar[10])
								{
									if (letterprompt == 4 && flagvar[6] > 0)
									{
										PlayBuzzer();
									}
									else
									{
										flagstring[listtype] += " ";
										RefreshNumberPrompt();
									}
								}
								else
								{
									PlayBuzzer();
								}
							}
							else if (option == maxoptions - 1)
							{
								if (flagstring[listtype].Length > 0)
								{
									promptpointers = new int[1] { listredirect.Value };
									prompt = false;
									skiptext = false;
								}
								else
								{
									PlayBuzzer();
								}
							}
						}
						else if (option < 10)
						{
							if (flagstring[0].Length < flagvar[10])
							{
								flagstring[0] += option;
								RefreshNumberPrompt();
							}
							else
							{
								PlayBuzzer();
							}
						}
						else if (option == 10)
						{
							if (flagstring[0].Length == 0)
							{
								promptpointers = new int[1] { listcancel };
							}
							else
							{
								flagvar[listtype] = Convert.ToInt32(flagstring[0]);
								promptpointers = new int[1] { listredirect.Value };
							}
							prompt = false;
							skiptext = false;
						}
						else if (flagstring[0].Length > 0)
						{
							flagstring[0] = flagstring[0].Remove(flagstring[0].Length - 1);
							RefreshNumberPrompt();
						}
						else
						{
							PlayBuzzer();
						}
					}
					ResetDiag(false);
				}
				else if (GetKey(5, false))
				{
					if (numberprompt)
					{
						listcanceled = true;
						PlaySound("Cancel", 10);
						if (instance.flagvar[0] != -555)
						{
							promptpick = 0;
							promptpointers = new int[1] { listcancel };
							prompt = false;
							inputcooldown = 15f;
							skiptext = false;
							ResetDiag(false);
						}
						else if (flagstring[listtype].Length > 0)
						{
							if (letterprompt == 4 && flagvar[6] > 0)
							{
								flagvar[6] = 0;
								koreanHL = new int[2] { -1, -1 };
								UpdateKoreanPrompt(0);
							}
							else
							{
								flagstring[listtype] = flagstring[listtype].Remove(flagstring[listtype].Length - 1);
							}
							RefreshNumberPrompt();
						}
						else
						{
							PlayBuzzer();
						}
					}
					else if (listcancel > -1)
					{
						PlaySound("Cancel", 10);
						skiptext = false;
						inputcooldown = 10f;
						promptpick = listcancel;
						option = listcancel;
						prompt = false;
						skiptext = false;
					}
				}
			}
		}
		if (instance.flagvar[3] == 5353 && inevent && !message && !noskip && lastevent == 3 && skiptext && inputcooldown <= 0f && (GetKey(4, true) || GetKey(5, true)))
		{
			sounds[5].pitch = 2.5f;
			sounds[6].pitch = 2.5f;
			Time.timeScale = 2.5f;
			noskip = true;
		}
		if (itemlist != null && ((!inbattle && !pause) || inbattle) && inputcooldown <= 0f)
		{
			if (listsell)
			{
				showmoney = 10f;
			}
			if (cursor != null)
			{
				if (questboardobj == null)
				{
					if (listtype == 20)
					{
						cursor.transform.localPosition = Vector3.Lerp(cursor.transform.localPosition, new Vector3(-2f, 0f - (float)listcursor * 0.7f, 10f), TieFramerate(0.2f));
					}
					else
					{
						cursor.transform.localPosition = Vector3.Lerp(cursor.transform.localPosition, new Vector3(2f, 0.75f - (float)listcursor * 0.7f, 10f), TieFramerate(0.2f));
					}
				}
				else
				{
					cursor.transform.localPosition = Vector3.Lerp(cursor.transform.localPosition, new Vector3(-7.3f, 2.8f - (float)listcursor * 0.7f, 10f), TieFramerate(0.2f));
				}
			}
			int num = option;
			if (GetKey(0, false) || KeyHold(Directions.Up))
			{
				UpdateList(true);
				if (option != num)
				{
					ShowItemList(listtype, listpos, listdesc, listsell);
				}
			}
			else if (GetKey(1, false) || KeyHold(Directions.Down))
			{
				UpdateList(false);
				if (option != num)
				{
					ShowItemList(listtype, listpos, listdesc, listsell);
				}
			}
			else if (GetKey(2, false))
			{
				if (questboardobj == null)
				{
					for (int i = 0; i < listammount; i++)
					{
						UpdateList(Directions.Up);
					}
					if (option != num)
					{
						ShowItemList(listtype, listpos, listdesc, listsell);
					}
				}
				else
				{
					PlaySound("PageFlip");
					ResetList();
					listtype--;
					if (listtype < 14)
					{
						listtype = 16;
					}
					UpdateQuestBoard();
					ShowItemList(listtype, listpos, true, false);
				}
			}
			else if (GetKey(3, false))
			{
				if (questboardobj == null)
				{
					for (int j = 0; j < listammount; j++)
					{
						UpdateList(Directions.Down);
					}
					if (option != num)
					{
						ShowItemList(listtype, listpos, listdesc, listsell);
					}
				}
				else
				{
					PlaySound("PageFlip");
					ResetList();
					listtype++;
					if (listtype > 16)
					{
						listtype = 14;
					}
					UpdateQuestBoard();
					ShowItemList(listtype, listpos, true, false);
				}
			}
			else
			{
				ResetKeyHold();
			}
			if (((listtype == 0 && instance.flags[349]) || listtype == 2 || listsell) && GetKey(7))
			{
				bool flag = false;
				if (multiselect.Contains(option))
				{
					multiselect.Remove(option);
					PlaySound("BadgeDequip");
					flag = true;
				}
				else if (listsell || multiselect.Count < ((listtype == 0) ? (maxstorage - items[2].Count) : (maxitems - items[0].Count)))
				{
					multiselect.Add(option);
					PlaySound("BadgeEquip");
					flag = true;
				}
				if (flag)
				{
					listY = -1;
					ShowItemList(listtype, listpos, listdesc, listsell);
				}
				else
				{
					PlayBuzzer();
				}
			}
			else if (GetKey(4, false))
			{
				PlaySound("Confirm", 10);
				listcanceled = false;
				lastPrompt = option;
				if (savelastlist)
				{
					overridedlist = SaveList();
					savelastlist = false;
				}
				if (questboardobj != null)
				{
					if (listtype == 14 && boardcaller != null)
					{
						if (listvar[option] != 0)
						{
							CloseQuestBoard();
							EntityControl entity = GetEntity(boardcaller.data[0]);
							for (int k = 0; k < instance.playerdata.Length; k++)
							{
								instance.playerdata[k].entity.FaceTowards(entity.transform.position);
							}
							StartCoroutine(SetText("|questprompt|" + GetDialogueText(boardcaller.data[1]), true, Vector3.zero, entity.transform, entity.npcdata));
							flagvar[0] = listvar[option];
							DestroyList();
						}
						else
						{
							PlayBuzzer();
						}
					}
					else if (Convert.ToInt32(map.name) == 0)
					{
						instance.boardquests[listtype - 14].Remove(listvar[option]);
						CloseQuestBoard();
						DestroyList();
					}
				}
				else if (listtype == 20)
				{
					languageid = listvar[option];
					InputIO.LoadSettings(true);
					SetVariables();
					DestroyList();
					StartCoroutine(UnityEngine.Object.FindObjectOfType<StartMenu>().Intro());
					LoadLangSpecific();
				}
				else if (listtype < 0)
				{
					int num2 = Mathf.Abs(listtype) - 1;
					int tPCost = GetTPCost(num2, option);
					instance.flagvar[0] = tPCost;
					bool flag2 = HasSkillCost(tPCost, num2);
					if (battle.CanSkill(listvar[option]) && flag2)
					{
						battle.SetItem(listvar[option]);
						DestroyList();
					}
					else
					{
						if (!flag2 && listvar[instance.option] != 48)
						{
							hudsprites[BadgeIsEquipped(72, num2) ? num2 : 3].color = Color.red;
						}
						PlayBuzzer();
					}
				}
				else if (listtype != 18 && listtype != 17)
				{
					if (instance.inbattle && listtype != 35)
					{
						if (battle.currentaction != BattleControl.Pick.StrategyList || (battle.currentaction == BattleControl.Pick.StrategyList && ((option == 2 && !battle.disablespy) || ((option == 0 || option == 1) && (AllPartyFree() || GetAlivePlayerAmmount() == 1)) || option == 4 || option == 3)))
						{
							battle.SetItem(listvar[option]);
							DestroyList();
						}
						else
						{
							PlayBuzzer();
						}
					}
					else
					{
						flagvar[storeid] = listvar[option];
						int num3 = listtype;
						if (num3 == 2 || num3 == 1)
						{
							num3 = 0;
						}
						if (listvar[option] > -1 && listtype < 22)
						{
							flagstring[0] = ((listtype == 3 || listtype == 32) ? badgedata[listvar[option], 0] : itemdata[num3, listvar[option], 0]);
							if (listsell)
							{
								if (multiselect.Count > 0)
								{
									flagstring[0] = menutext[279];
									flagvar[10] = 0;
									for (int l = 0; l < multiselect.Count; l++)
									{
										flagvar[10] += Mathf.Clamp(Mathf.FloorToInt(Convert.ToInt32(itemdata[num3, listvar[multiselect[l]], 4]) / 2), 1, 999);
									}
									instance.flags[349] = true;
								}
								else
								{
									flagvar[10] = Mathf.Clamp(Mathf.FloorToInt(Convert.ToInt32(itemdata[num3, listvar[option], 4]) / 2), 1, 999);
								}
							}
						}
						DestroyList();
					}
				}
				else
				{
					DestroyList();
				}
			}
			else if (GetKey(5, false) && listtype != 20)
			{
				instance.multiselect = new List<int>();
				PlaySound("Cancel", 10);
				listcanceled = true;
				inputcooldown = 15f;
				instance.skiptext = false;
				if (questboardobj != null)
				{
					CloseQuestBoard();
					SaveCameraPosition(false);
				}
				else if (battle != null)
				{
					battle.CancelList();
				}
				else
				{
					listredirect = listcancel;
				}
				DestroyList();
			}
		}
		GetJoystick();
	}

	public static IEnumerator TempColor(Color color, float frametime, SpriteRenderer render)
	{
		float a = 0f;
		Color c = render.material.color;
		do
		{
			if (render != null)
			{
				render.material.color = Color.Lerp(c, color, a / frametime);
				yield return null;
				a += TieFramerate(1f);
				continue;
			}
			yield break;
		}
		while (a < frametime + 1f);
		a = 0f;
		yield return null;
		do
		{
			if (render != null)
			{
				render.material.color = Color.Lerp(color, c, a / frametime);
				yield return null;
				a += TieFramerate(1f);
				continue;
			}
			yield break;
		}
		while (a < frametime + 1f);
		if (render != null)
		{
			render.material.color = c;
		}
		templetter = null;
	}

	public static void SetFont(TextMesh letter, int fontid)
	{
		fontid = FontID(fontid);
		if (fontid < fonts.Length)
		{
			letter.font = fonts[fontid];
		}
		MeshRenderer component = letter.GetComponent<MeshRenderer>();
		component.material = fontmat[fontid];
		component.material.shader = fontmat[0].shader;
	}

	public static IEnumerator TempLetters(string text, int[] spacebreaks, bool rainbow, Vector3 posLeave0ToCenter, float showtime)
	{
		TextMesh[] roundletter = new TextMesh[text.Length];
		if (spacebreaks == null)
		{
			spacebreaks = new int[1] { -1 };
		}
		float space = 0f;
		List<int> s = new List<int>(spacebreaks);
		for (int i = 0; i < roundletter.Length; i++)
		{
			if (text[i] != ' ')
			{
				if (s.Contains(i))
				{
					space += 1f;
				}
				roundletter[i] = GetEmptyLetter();
				roundletter[i].text = text[i].ToString() ?? "";
				DialogueAnim dialogueAnim = roundletter[i].gameObject.AddComponent<DialogueAnim>();
				if (rainbow)
				{
					roundletter[i].gameObject.AddComponent<FontEffects>().SetEffects(false, false, true, false, false, 2, i);
				}
				dialogueAnim.targetscale = Vector3.one * 2f;
				dialogueAnim.shrinkspeed = 0.15f;
				yield return new WaitForSeconds(0.05f);
			}
		}
		yield return new WaitForSeconds(showtime);
		for (int i = 0; i < roundletter.Length; i++)
		{
			if (roundletter[i] != null)
			{
				roundletter[i].GetComponent<DialogueAnim>().shrink = true;
				UnityEngine.Object.Destroy(roundletter[i].gameObject, 2f);
				yield return new WaitForSeconds(0.05f);
			}
		}
		templetter = null;
	}

	public static IEnumerator GradualColor(Renderer obj, Color target, float frametime)
	{
		Color s = obj.material.color;
		float a = 0f;
		do
		{
			obj.material.color = Color.Lerp(s, target, a / frametime);
			a += TieFramerate(1f);
			yield return null;
		}
		while (a < frametime + 1f);
	}

	public static IEnumerator GradualColor(SpriteRenderer obj, Color target, float frametime, bool sprite)
	{
		Color s = obj.color;
		float a = 0f;
		do
		{
			obj.color = Color.Lerp(s, target, a / frametime);
			a += TieFramerate(1f);
			yield return null;
		}
		while (a < frametime + 1f);
	}

	private static void UpdateQuestBoard()
	{
		instance.questboardobj.GetChild(5).gameObject.SetActive(listtype == 14);
		Transform child = instance.questboardobj.GetChild(1);
		Renderer[] componentsInChildren = child.GetComponentsInChildren<Renderer>();
		int num = 0;
		for (int i = 1; i < componentsInChildren.Length; i++)
		{
			if (componentsInChildren[i].transform.parent == child)
			{
				if (num == listtype - 14)
				{
					componentsInChildren[i].sortingOrder = 5;
				}
				else if (listtype - 14 == 0)
				{
					componentsInChildren[i].sortingOrder = -20 - num * 2;
				}
				else
				{
					componentsInChildren[i].sortingOrder = -20 + num * 2;
				}
				Renderer[] componentsInChildren2 = componentsInChildren[i].transform.GetChild(0).GetComponentsInChildren<Renderer>();
				for (int j = 0; j < componentsInChildren2.Length; j++)
				{
					componentsInChildren2[j].sortingOrder = componentsInChildren[i].sortingOrder + 1;
				}
				num++;
			}
		}
	}

	private void CloseQuestBoard()
	{
		UnityEngine.Object.Destroy(questboardobj.GetChild(3).gameObject);
		UnityEngine.Object.Destroy(questboardobj.GetChild(4).gameObject);
		for (int i = 0; i < questboardobj.childCount; i++)
		{
			if (questboardobj.GetChild(i) != null)
			{
				DestroyText(questboardobj.GetChild(i));
			}
		}
		ButtonSprite[] componentsInChildren = questboardobj.GetComponentsInChildren<ButtonSprite>();
		for (int j = 0; j < componentsInChildren.Length; j++)
		{
			componentsInChildren[j].transform.parent = questboardobj.GetChild(0);
		}
		DialogueAnim[] componentsInChildren2 = questboardobj.GetComponentsInChildren<DialogueAnim>();
		for (int k = 0; k < componentsInChildren2.Length; k++)
		{
			componentsInChildren2[k].shrink = true;
		}
		UnityEngine.Object.Destroy(questboardobj.gameObject, 0.1f);
		player.actioncooldown = 20f;
		minipause = false;
		inlist = false;
	}

	private void DestroyList()
	{
		itemlist.gameObject.AddComponent<DialogueAnim>().shrink = true;
		UnityEngine.Object.Destroy(itemlist.gameObject, 0.5f);
		itemlist = null;
		UnityEngine.Object.Destroy(instance.cursor.gameObject);
		skiptext = false;
		listoption = option;
		option = -1;
		listY = -1;
	}

	private void RefreshNumberPrompt()
	{
		DestroyText(npromptholder);
		instance.flagvar[4] = 0;
		string text = flagstring[0].PadRight(flagvar[10], '_');
		if (instance.flagvar[0] == -555)
		{
			if (flagstring[listtype] == null)
			{
				flagstring[listtype] = "";
			}
			text = flagstring[listtype].PadRight(flagvar[10], '_');
		}
		StartCoroutine(SetText("|center|" + text, 0, null, false, false, Vector3.zero, Vector3.zero, Vector2.one, npromptholder, null));
	}

	public static float ClampedAngle(float input, float max)
	{
		while (input < 0f)
		{
			input += max;
		}
		while (input > max)
		{
			input = max - input;
		}
		return input;
	}

	public static BattleData GetEnemyData(int id)
	{
		return GetEnemyData(id, false, false);
	}

	public static BattleData GetEnemyData(int id, bool createentity)
	{
		return GetEnemyData(id, createentity, false);
	}

	public static float GetSoundDistance(Vector3 position)
	{
		return GetSoundDistance(position, 25f);
	}

	public static float GetSoundDistance(Vector3 position, float maxdistance)
	{
		return 1f - Mathf.Clamp(Vector3.Distance(position, MainCamera.transform.position), 0f, maxdistance) / maxdistance;
	}

	public static float GetSoundDistance(float distance, float maxdistance)
	{
		return 1f - Mathf.Clamp(distance, 0f, maxdistance) / maxdistance;
	}

	public static float GetSoundDistance(float distance)
	{
		return GetSoundDistance(distance, 25f);
	}

	public static void PlayMoveSound(EntityControl entity)
	{
		PlayMoveSound(entity.originalid, 9, entity.height > 0.1f);
	}

	public static void PlayMoveSound(int animid, int soundid, bool fly)
	{
		string text = null;
		float volume = 1f;
		float pitch = 1f;
		switch ((AnimIDs)(animid + 1))
		{
		case AnimIDs.Scorpion:
			text = "Step";
			break;
		case AnimIDs.DivingSpider:
			text = "WetStep";
			break;
		case AnimIDs.FlyTrap:
		case AnimIDs.Krawler:
			text = "Scuttle3";
			break;
		case AnimIDs.Armorpillar:
			text = "FunnyStep";
			pitch = 0.9f;
			volume = 0.3f;
			break;
		case AnimIDs.Mushroom:
			if (fly)
			{
				text = "Fly";
				pitch = 0.5f;
			}
			break;
		case AnimIDs.Seedling:
		case AnimIDs.Flowering:
			if (fly)
			{
				text = "Toss2";
				volume = 0.5f;
				pitch = 1.1f;
			}
			break;
		case AnimIDs.Thief:
		case AnimIDs.Midge:
		case AnimIDs.Mantidfly:
			if (fly)
			{
				text = "BugWing";
			}
			break;
		case AnimIDs.Spuder:
			text = "Scuttle2";
			volume = 0.8f;
			pitch = 0.9f;
			break;
		case AnimIDs.MimicSpider:
			text = "Scuttle2";
			volume = 0.8f;
			pitch = 1.1f;
			break;
		case AnimIDs.Plumpling:
			text = "ThumpSoft";
			volume = 0.65f;
			pitch = 0.65f;
			break;
		case AnimIDs.CordycepsAnt:
			text = "Scuttle";
			volume = 0.8f;
			pitch = 0.5f;
			break;
		case AnimIDs.Abomihoney:
			text = "PuddleMove";
			pitch = 1.15f;
			break;
		}
		if (text != null)
		{
			PlaySound(text, soundid, pitch, volume, true);
		}
	}

	public static BattleData GetEnemyData(int id, bool createentity, bool noexp)
	{
		BattleData result = default(BattleData);
		int num = Convert.ToInt32(enemydata[id, 25]);
		result.eventondeath = Convert.ToInt32(enemydata[id, 26]);
		bool flag = false;
		result.animid = id;
		if (createentity)
		{
			result.battleentity = EntityControl.CreateNewEntity("enemy" + id, 0, new Vector3(0f, -10f));
			result.battleentity.height = Convert.ToSingle(enemydata[id, 20]);
			result.battleentity.bobspeed = Convert.ToSingle(enemydata[id, 21]);
			result.battleentity.bobrange = Convert.ToSingle(enemydata[id, 22]);
			switch ((Enemies)id)
			{
			case Enemies.FireCape:
				result.battleentity.forcefire = true;
				id = 58;
				if (map.mapid == Maps.CaveOfTrials && !instance.flags[664])
				{
					flag = true;
				}
				break;
			case Enemies.FireWarden:
				result.battleentity.forcefire = true;
				id = 61;
				if (map.mapid == Maps.CaveOfTrials && !instance.flags[664])
				{
					flag = true;
				}
				break;
			case Enemies.FireKrawler:
				result.battleentity.forcefire = true;
				id = 57;
				if (map.mapid == Maps.CaveOfTrials && !instance.flags[664])
				{
					flag = true;
				}
				break;
			case Enemies.IceKrawler:
				result.battleentity.inice = true;
				id = 57;
				break;
			case Enemies.IceWarden:
				id = 61;
				result.battleentity.inice = true;
				break;
			}
		}
		result.moves = Convert.ToInt32(enemydata[id, 27]);
		result.notaunt = Convert.ToBoolean(enemydata[id, 28]);
		result.cantfall = Convert.ToBoolean(enemydata[id, 29]);
		result.notired = Convert.ToBoolean(enemydata[id, 31]);
		result.fixedexp = Convert.ToBoolean(enemydata[id, 30]);
		result.position = (BattleControl.BattlePosition)Enum.Parse(typeof(BattleControl.BattlePosition), enemydata[id, 19]);
		if (num > -1)
		{
			result.animid = num;
			id = num;
		}
		if (map.mapid == Maps.CaveOfTrials && instance.enemyencounter[id, 0] == 0)
		{
			flag = true;
		}
		result.entityname = (flag ? menutext[59] : enemynames[id]);
		result.holditem = -1;
		result.hp = Convert.ToInt32(enemydata[id, 1]);
		result.def = Convert.ToInt32(enemydata[id, 2]);
		if (instance.partylevel < 27 && !instance.flags[613])
		{
			result.exp = Convert.ToInt32(enemydata[id, 3]);
			if (!result.fixedexp && !noexp)
			{
				result.exp = GetEXP(result.exp, (Enemies)result.animid);
			}
			else if (noexp)
			{
				result.exp = 0;
			}
		}
		result.money = Convert.ToInt32(enemydata[id, 4]);
		result.cursoroffset = new Vector3(Convert.ToSingle(enemydata[id, 5]), Convert.ToSingle(enemydata[id, 6]), Convert.ToSingle(enemydata[id, 7]));
		result.itemoffset = new Vector3(Convert.ToSingle(enemydata[id, 39]), Convert.ToSingle(enemydata[id, 40]), Convert.ToSingle(enemydata[id, 41]));
		result.poisonres = Convert.ToInt32(enemydata[id, 8]);
		result.freezeres = Convert.ToInt32(enemydata[id, 9]);
		result.numbres = Convert.ToInt32(enemydata[id, 10]);
		result.sleepres = Convert.ToInt32(enemydata[id, 11]);
		result.size = Convert.ToSingle(enemydata[id, 12]);
		result.deathtype = Convert.ToInt32(enemydata[id, 33]);
		if (enemydata[id, 34] != "-1")
		{
			string[] array = enemydata[id, 34].Split(';');
			if (array.Length != 0)
			{
				result.chargeonotherenemy = new int[array.Length];
				for (int i = 0; i < result.chargeonotherenemy.Length; i++)
				{
					if (array[i].Length > 0)
					{
						result.chargeonotherenemy[i] = Convert.ToInt32(array[i]);
					}
					else
					{
						result.chargeonotherenemy[i] = -1;
					}
				}
			}
		}
		else
		{
			result.chargeonotherenemy = new int[0];
		}
		result.hidehp = Convert.ToBoolean(enemydata[id, 32]);
		result.defenseonhit = Convert.ToInt32(enemydata[id, 38]);
		result.eventonfall = Convert.ToInt32(enemydata[id, 45]);
		result.notattle = flag || Convert.ToBoolean(enemydata[id, 44]);
		result.onhitaction = Convert.ToInt32(enemydata[id, 46]);
		result.actimmobile = Convert.ToBoolean(enemydata[id, 47]);
		result.sizeonfreeze = Convert.ToSingle(enemydata[id, 48]);
		if (result.sizeonfreeze < 0.1f)
		{
			result.sizeonfreeze = result.size + 0.25f;
		}
		result.initialsize = result.size;
		if (BadgeIsEquipped(11) || instance.flags[166] || instance.flags[614])
		{
			result.hardatk += Convert.ToInt32(enemydata[id, 35]);
			result.hp += Convert.ToInt32(enemydata[id, 36]);
			result.def += Convert.ToInt32(enemydata[id, 37]);
			if (instance.flags[614])
			{
				result.hardatk++;
				result.hp = Mathf.CeilToInt((float)result.hp * 1.15f);
				if (instance.flags[300] && result.def >= 0)
				{
					result.def++;
				}
			}
			if (instance.flags[166] && id != 103 && id != 72 && id != 102 && id != 101)
			{
				result.hp = Mathf.CeilToInt((float)result.hp * 1.5f);
				if (result.hp > 90)
				{
					result.hp = Mathf.CeilToInt((float)result.hp * 0.85f);
				}
				result.hardatk = Mathf.CeilToInt((float)result.hardatk * 1.5f);
				result.harddef = Mathf.CeilToInt((float)result.harddef * 1.5f);
				result.def = Mathf.Clamp(result.def, 1, 99);
			}
		}
		if (instance.flags[162])
		{
			result.exp = Mathf.Clamp(Mathf.FloorToInt((float)result.exp * 0.2f), 0, 5);
		}
		result.maxhp = result.hp;
		if (result.position == BattleControl.BattlePosition.Random)
		{
			if (id != 1 || instance.flags[24])
			{
				result.position = (BattleControl.BattlePosition)UnityEngine.Random.Range(0, 2);
			}
			else
			{
				result.position = BattleControl.BattlePosition.Flying;
			}
		}
		if (createentity)
		{
			result.battleentity.animid = Convert.ToInt32(enemydata[id, 0]);
			result.battleentity.freezesize = new Vector3(Convert.ToSingle(enemydata[id, 13]), Convert.ToSingle(enemydata[id, 14]), Convert.ToSingle(enemydata[id, 15]));
			result.battleentity.freezeoffset = new Vector3(Convert.ToSingle(enemydata[id, 16]), Convert.ToSingle(enemydata[id, 17]), Convert.ToSingle(enemydata[id, 18]));
			result.battleentity.overrridejump = true;
			result.battleentity.battle = true;
			result.battleentity.alwaysactive = true;
			result.battleentity.onground = true;
			if (flag || result.animid == 110)
			{
				result.battleentity.name = result.battleentity.name.Insert(0, "COT");
				result.battleentity.hologram = true;
				result.battleentity.cotunknown = true;
				result.battleentity.Invoke("RefreshCOT", 0.1f);
			}
			result.battleentity.tag = "Enemy";
			result.battleentity.gameObject.layer = 9;
			result.entity = result.battleentity;
			result.battleentity.height = ((result.position == BattleControl.BattlePosition.Flying && result.battleentity.height < 2f) ? 2f : result.battleentity.height);
			result.battleentity.initialheight = result.battleentity.height;
			result.battleentity.CreateHPBar();
			result.battleentity.emoticonoffset = result.cursoroffset;
			if (result.position == BattleControl.BattlePosition.Underground)
			{
				result.battleentity.InstantDig();
				result.battleentity.height = 0f;
				result.battleentity.initialheight = 0f;
			}
			if (!instance.flags[166] && result.battleentity.hologram)
			{
				result.exp = Mathf.FloorToInt((float)result.exp * 0.1f);
			}
			if (Convert.ToBoolean(enemydata[id, 42]))
			{
				result.battleentity.basestate = 13;
			}
		}
		if (result.entity != null)
		{
			switch (result.deathtype)
			{
			case 0:
			case 3:
				result.entity.destroytype = NPCControl.DeathType.SpinSmoke;
				break;
			case 2:
			case 4:
				result.entity.destroytype = NPCControl.DeathType.KO;
				break;
			case 1:
				result.entity.destroytype = NPCControl.DeathType.SpinNoSmoke;
				break;
			case 5:
				result.entity.destroytype = NPCControl.DeathType.SpinKO;
				break;
			case 6:
				result.entity.destroytype = NPCControl.DeathType.Shrink;
				break;
			case 7:
				result.entity.destroytype = NPCControl.DeathType.ShrinkNoSmoke;
				break;
			case 8:
				result.entity.destroytype = NPCControl.DeathType.None;
				break;
			case 9:
				result.entity.destroytype = NPCControl.DeathType.Sink;
				break;
			case 10:
				result.entity.destroytype = NPCControl.DeathType.ExplodeAnim;
				break;
			case 11:
				result.entity.destroytype = NPCControl.DeathType.DropSprites;
				break;
			}
		}
		result.condition = new List<int[]>();
		result.weakness = new List<BattleControl.AttackProperty>();
		string[] array2 = enemydata[id, 23].Split('{');
		if (Convert.ToInt32(array2[0]) > 0)
		{
			for (int j = 1; j < array2.Length; j++)
			{
				if (array2[j] != "")
				{
					result.weakness.Add((BattleControl.AttackProperty)Enum.Parse(typeof(BattleControl.AttackProperty), array2[j]));
				}
			}
		}
		result.weight = Convert.ToSingle(enemydata[id, 24]);
		result.cantmove = -result.moves + 1;
		return result;
	}

	public static IEnumerator ForceFailsafe(Transform obj, Vector3 target, float cooldown)
	{
		while (cooldown > 0f)
		{
			cooldown -= framestep;
			yield return null;
		}
		if (GetDistance(obj.position, target) > 0.45000002f)
		{
			DeathSmoke(obj.position);
			obj.position = target;
			DeathSmoke(target);
		}
	}

	public static int GetEXP(int input)
	{
		return GetEXP(input, instance.partylevel, null);
	}

	public static int GetEXP(int input, int level)
	{
		return GetEXP(input, level, null);
	}

	public static int GetEXP(int input, Enemies enemy)
	{
		return GetEXP(input, instance.partylevel, enemy);
	}

	public static int GetEXP(int input, int lv, Enemies? enemy)
	{
		float num = ((map != null) ? map.expmulti : 1f);
		if (enemy.HasValue && (enemy.Value == Enemies.WaspTrooper || enemy.Value == Enemies.WaspHealer))
		{
			if (CurrentMap() == Maps.MetalLake || instance.areaid == 19)
			{
				input = Mathf.CeilToInt((float)input * 1.65f);
			}
			else if (instance.areaid == 14)
			{
				input = Mathf.CeilToInt((float)input * 2.5f);
				if (enemy.Value == Enemies.WaspHealer)
				{
					input += 10;
				}
			}
		}
		return Mathf.FloorToInt(Mathf.Clamp((float)input * num - ((lv > 1) ? ((float)(lv - 1) * 2.5f) : 0f), 1f, 99f)) - 1;
	}

	public static void PlayBuzzer()
	{
		PlaySound("Buzzer", 10);
	}

	public static void Heal(bool noparticle, bool nosound)
	{
		Heal(new Healing[1], instance.partyorder, noparticle, nosound);
	}

	public static void Heal()
	{
		Heal(new Healing[1], instance.partyorder, false, false);
	}

	public static void Heal(Healing[] parameters, int[] partyids, bool noparticle, bool nosound)
	{
		if (!nosound)
		{
			PlaySound("Heal");
		}
		instance.hudcooldown = 100f;
		for (int i = 0; i < parameters.Length; i++)
		{
			for (int j = 0; j < partyids.Length; j++)
			{
				if (parameters[i] == Healing.Full)
				{
					instance.playerdata[j].hp = instance.playerdata[j].maxhp;
					instance.tp = instance.maxtp;
				}
				else if (parameters[i] == Healing.TPOnly)
				{
					instance.tp = instance.maxtp;
				}
				else if (parameters[i] == Healing.FullHPOnly)
				{
					instance.playerdata[j].hp = instance.playerdata[j].maxhp;
				}
				if (parameters[i] != Healing.TPOnly && !noparticle)
				{
					if ((bool)battle)
					{
						HealParticle(instance.playerdata[j].battleentity.transform, Vector3.one, Vector3.up);
					}
					else
					{
						HealParticle(instance.playerdata[j].entity.transform, Vector3.one, Vector3.up);
					}
				}
			}
		}
	}

	public static int ConditionTurns(int playerid, int turns)
	{
		return turns;
	}

	public static bool HasWeakness(BattleControl.AttackProperty property, BattleData entity)
	{
		BattleControl.AttackProperty[] array = entity.weakness.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i] == property)
			{
				return true;
			}
		}
		return false;
	}

	public static void SetCondition(BattleCondition condition, ref BattleData entity, int turns, int fromplayer)
	{
		if (condition == BattleCondition.Shield)
		{
			entity.battleentity.shieldenabled = true;
		}
		for (int i = 0; i < entity.condition.Count; i++)
		{
			if (entity.condition[i][0] != (int)condition)
			{
				continue;
			}
			if (turns > 0 || ConditionChance(condition, entity))
			{
				switch (condition)
				{
				case BattleCondition.AttackUp:
				case BattleCondition.DefenseUp:
				case BattleCondition.AttackDown:
				case BattleCondition.DefenseDown:
				case BattleCondition.GradualHP:
				case BattleCondition.GradualTP:
					entity.condition[i][1] += Mathf.Abs(turns);
					break;
				case BattleCondition.Fire:
					entity.condition[i][1] += Mathf.CeilToInt(Mathf.Abs((float)turns) / 2f);
					break;
				case BattleCondition.Poison:
					if (!entity.battleentity.CompareTag("Player") || !BadgeIsEquipped(27, entity.trueid))
					{
						goto case BattleCondition.AttackUp;
					}
					turns = 99999;
					goto default;
				case BattleCondition.Freeze:
				case BattleCondition.Numb:
				case BattleCondition.Sleep:
					if (!entity.battleentity.CompareTag("Player"))
					{
						if (entity.isdefending)
						{
							entity.isdefending = false;
						}
					}
					else
					{
						if (battle != null && battle.currentchoice == BattleControl.Actions.Item)
						{
							goto case BattleCondition.AttackUp;
						}
						if (entity.condition[i][1] >= Mathf.Abs(turns))
						{
							break;
						}
					}
					goto default;
				default:
					entity.condition[i][1] = Mathf.Abs(turns);
					break;
				}
			}
			entity.condition[i][1] = Mathf.Clamp(entity.condition[i][1], 0, 999999);
			FixCondition(entity);
			return;
		}
		switch (condition)
		{
		case BattleCondition.Poison:
			if (entity.battleentity.CompareTag("Player") && BadgeIsEquipped(27, entity.trueid))
			{
				turns = 99999;
			}
			break;
		case BattleCondition.Freeze:
		case BattleCondition.Numb:
		case BattleCondition.Sleep:
			if (!entity.battleentity.CompareTag("Player") && entity.isdefending)
			{
				entity.isdefending = false;
			}
			break;
		}
		entity.condition.Add(new int[2]
		{
			(int)condition,
			turns
		});
		FixCondition(entity);
	}

	private static void FixCondition(BattleData entity)
	{
		int[][] array = entity.condition.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			int num = entity.condition[i][1];
			if (num < 0)
			{
				entity.condition[i][1] = Mathf.Abs(num);
			}
			switch ((BattleCondition)array[i][0])
			{
			case BattleCondition.Shield:
				if (entity.battleentity.CompareTag("Player"))
				{
					entity.condition[i][1] = 1;
				}
				break;
			case BattleCondition.Numb:
				entity.isnumb = true;
				break;
			}
		}
	}

	public static void SetCondition(BattleCondition condition, ref BattleData entity, int turns)
	{
		SetCondition(condition, ref entity, turns, -1);
	}

	public static bool ConditionChance(BattleCondition condition, BattleData entity)
	{
		float num = UnityEngine.Random.Range(0f, 100f);
		switch (condition)
		{
		case BattleCondition.Freeze:
			return num > (float)entity.freezeres;
		case BattleCondition.Numb:
			return num > (float)entity.numbres;
		case BattleCondition.Poison:
			return num > (float)entity.poisonres;
		case BattleCondition.Sleep:
			return num > (float)entity.sleepres;
		default:
			return true;
		}
	}

	public static int HasCondition(BattleData entity)
	{
		if (HasCondition(BattleCondition.Freeze, entity) > 0 || HasCondition(BattleCondition.Numb, entity) > 0 || HasCondition(BattleCondition.Sleep, entity) > 0)
		{
			return 1;
		}
		return -1;
	}

	public static int HasCondition(BattleCondition condition, BattleData entity)
	{
		for (int i = 0; i < entity.condition.Count; i++)
		{
			if (entity.condition[i][0] == (int)condition)
			{
				if (condition == BattleCondition.Sleep)
				{
					entity.isasleep = true;
				}
				return entity.condition[i][1];
			}
		}
		return -1;
	}

	public static void RemoveCondition(BattleCondition condition, BattleData entity)
	{
		int num = -1;
		for (int i = 0; i < entity.condition.Count; i++)
		{
			if (entity.condition[i][0] != (int)condition)
			{
				continue;
			}
			switch (condition)
			{
			case BattleCondition.Numb:
			case BattleCondition.Sleep:
				entity.battleentity.RefreshCondition();
				break;
			case BattleCondition.Freeze:
				if (entity.entity != null)
				{
					entity.entity.BreakIce();
				}
				break;
			}
			num = i;
			break;
		}
		if (num > -1)
		{
			entity.condition.RemoveAt(num);
		}
	}

	public static SpriteRenderer GetTransitionSprite()
	{
		return GetTransitionSprite(0);
	}

	public static SpriteRenderer GetTransitionSprite(int id)
	{
		return instance.transitionobj[id].GetComponent<SpriteRenderer>();
	}

	public static void PlayScrollSound()
	{
		if (!sounds[10].isPlaying)
		{
			PlaySound(Resources.Load<AudioClip>("Audio/Sounds/Scroll"), 10, 1f, 0.65f);
			if (pausemenu != null && pausemenu.windowid >= 4)
			{
				sounds[10].volume = pausemenu.svolume;
			}
		}
	}

	public void UpdateList(bool up)
	{
		UpdateList(up, 1);
	}

	public void UpdateList(bool up, int skip)
	{
		UpdateList(up, skip, false);
	}

	public void UpdateList(bool up, int skip, bool nosound)
	{
		if (up)
		{
			if (option == 0)
			{
				for (int i = 0; i < maxoptions; i++)
				{
					UpdateList(Directions.Down, nosound);
					listY = -1;
				}
			}
			else
			{
				for (int j = 0; j < skip; j++)
				{
					UpdateList(Directions.Up, nosound);
				}
			}
		}
		else if (option == maxoptions - 1)
		{
			for (int k = 0; k < maxoptions; k++)
			{
				UpdateList(Directions.Up, nosound);
			}
			listY = -1;
		}
		else
		{
			for (int l = 0; l < skip; l++)
			{
				UpdateList(Directions.Down, nosound);
			}
		}
	}

	public static void ReadSettings(string[] c)
	{
		int num = 0;
		for (num = 0; num < InputIO.keys.Length; num++)
		{
			InputIO.keys[num] = (KeyCode)Enum.Parse(typeof(KeyCode), c[num]);
		}
		resolutionindex = Convert.ToInt32(c[num]);
		num++;
		fullscreen = Convert.ToBoolean(c[num]);
		num++;
		fps = Convert.ToInt32(c[num]);
		num++;
		lowshadows = Convert.ToBoolean(c[num]);
		num++;
		lowtexture = Convert.ToBoolean(c[num]);
		num++;
		musicvolume = Convert.ToSingle(c[num]);
		num++;
		soundvolume = Convert.ToSingle(c[num]);
		num++;
		MainCamera.GetComponent<FXAA>().enabled = Convert.ToBoolean(c[num]);
		num++;
		languageid = Convert.ToInt32(c[num]);
		num++;
		nowindeffect = Convert.ToBoolean(c[num]);
		num++;
		enableoutline = Convert.ToInt32(c[num]);
		num++;
		downsample = Convert.ToInt32(c[num]);
		num++;
		particlelevel = Convert.ToInt32(c[num]);
		num++;
		string text = c[num];
		if (!(text == "True"))
		{
			if (text == "False")
			{
				c[num] = "0";
			}
		}
		else
		{
			c[num] = "1";
		}
		usejoystick = Convert.ToInt32(c[num]);
		num++;
		bleepvolume = Convert.ToSingle(c[num]);
		num++;
		vsync = Convert.ToInt32(c[num]);
		num++;
		if (usejoystick == 5 || usejoystick == 4 || Convert.ToInt32(c[num]) < c.Length - 1)
		{
			forcejoystick = Convert.ToInt32(c[num]);
		}
		num++;
		keepmusicafterbattle = Convert.ToBoolean(c[num]);
		num++;
		mashcommandalt = Convert.ToBoolean(c[num]);
		num++;
		if (num < c.Length)
		{
			joybinds = new int[10];
			for (int i = 0; i < 10; i++)
			{
				joybinds[i] = Convert.ToInt32(c[num + i]);
			}
			num += 10;
		}
		if (num < c.Length && c[num].Length > 0)
		{
			monoaudio = Convert.ToBoolean(c[num]);
		}
		num++;
		if (num < c.Length && c[num].Length > 0)
		{
			string[] array = c[num].Split(',');
			for (int j = 0; j < secretunlocks.Length; j++)
			{
				secretunlocks[j] = Convert.ToBoolean(array[j]);
			}
			num++;
		}
		if (num < c.Length && c[num].Length > 0)
		{
			analog = Convert.ToInt32(c[num]);
		}
		num++;
		if (num < c.Length && c[num].Length > 0)
		{
			pauseonfocus = Convert.ToBoolean(c[num]);
		}
		num++;
		if (num < c.Length && c[num].Length > 0)
		{
			snapTo8 = Convert.ToBoolean(c[num]);
		}
		num++;
	}

	public static string SaveSettings()
	{
		string text = "";
		for (int i = 0; i < InputIO.keys.Length; i++)
		{
			text = text + InputIO.keys[i].ToString() + "\n";
		}
		text = text + resolutionindex + "\n" + fullscreen.ToString() + "\n" + fps + "\n" + lowshadows.ToString() + "\n" + lowtexture.ToString() + "\n" + musicvolume + "\n" + soundvolume + "\n" + MainCamera.GetComponent<FXAA>().enabled.ToString() + "\n" + languageid + "\n" + nowindeffect.ToString() + "\n" + enableoutline + "\n" + downsample + "\n" + particlelevel + "\n" + usejoystick + "\n" + bleepvolume + "\n" + vsync + "\n" + forcejoystick + "\n" + keepmusicafterbattle.ToString() + "\n" + mashcommandalt.ToString() + "\n";
		for (int j = 0; j < 10; j++)
		{
			text = text + joybinds[j] + "\n";
		}
		return text + monoaudio.ToString() + "\n" + secretunlocks[0].ToString() + "," + secretunlocks[1].ToString() + "," + secretunlocks[2].ToString() + "," + secretunlocks[3].ToString() + "," + secretunlocks[4].ToString() + "\n" + analog + "\n" + pauseonfocus.ToString() + "\n" + snapTo8.ToString();
	}

	public static string SaveFile(Vector3? savepos)
	{
		string text = "";
		text = ((!savepos.HasValue) ? (text + player.transform.position.x + "," + player.transform.position.y + "," + player.transform.position.z + ",") : (text + savepos.Value.x + "," + savepos.Value.y + "," + savepos.Value.z + ","));
		text = text + instance.flags[613] + "," + instance.flags[614] + "," + instance.flags[615] + "," + instance.flags[616] + "," + instance.flags[656] + "," + instance.flags[681] + "," + instance.flagstring[10] + "\n";
		for (int i = 0; i < instance.playerdata.Length; i++)
		{
			text = text + instance.playerdata[i].trueid + "," + instance.playerdata[i].hp + "," + instance.playerdata[i].maxhp + "," + instance.playerdata[i].basehp + "," + instance.playerdata[i].atk + "," + instance.playerdata[i].baseatk + "," + instance.playerdata[i].def + "," + instance.playerdata[i].basedef;
			if (i < instance.playerdata.Length - 1)
			{
				text += "@";
			}
		}
		text += "\n";
		text = text + instance.partylevel + "," + instance.partyexp + "," + instance.neededexp + "," + instance.basetp + "," + instance.tp + "," + instance.money + "," + map.name + "," + instance.areaid + "," + instance.bp + "," + instance.maxbp + "," + instance.maxitems + "," + instance.maxstorage + "," + instance.clockhour + "," + instance.clockmin + "," + instance.clocksec + "," + SaveProgressIcons() + "\n";
		for (int j = 0; j < instance.avaliablebadgepool.Length; j++)
		{
			int[] array = instance.avaliablebadgepool[j].ToArray();
			for (int k = 0; k < array.Length; k++)
			{
				text += array[k];
				if (k < array.Length - 1)
				{
					text += ",";
				}
			}
			if (j < instance.avaliablebadgepool.Length - 1)
			{
				text += "@";
			}
		}
		text += "\n";
		for (int l = 0; l < instance.badgeshops.Length; l++)
		{
			int[] array2 = instance.badgeshops[l].ToArray();
			for (int m = 0; m < array2.Length; m++)
			{
				text += array2[m];
				if (m < array2.Length - 1)
				{
					text += ",";
				}
			}
			if (l < instance.badgeshops.Length - 1)
			{
				text += "@";
			}
		}
		text += "\n";
		for (int n = 0; n < instance.boardquests.Length; n++)
		{
			int[] array3 = instance.boardquests[n].ToArray();
			for (int num = 0; num < array3.Length; num++)
			{
				text += array3[num];
				if (num < array3.Length - 1)
				{
					text += ",";
				}
			}
			if (n < instance.boardquests.Length - 1)
			{
				text += "@";
			}
		}
		text += "\n";
		for (int num2 = 0; num2 < instance.items.Length; num2++)
		{
			int[] array4 = instance.items[num2].ToArray();
			for (int num3 = 0; num3 < array4.Length; num3++)
			{
				text += array4[num3];
				if (num3 < array4.Length - 1)
				{
					text += ",";
				}
			}
			if (num2 < instance.items.Length - 1)
			{
				text += "@";
			}
		}
		text += "\n";
		for (int num4 = 0; num4 < instance.badges.Count; num4++)
		{
			int[] array5 = instance.badges[num4];
			for (int num5 = 0; num5 < array5.Length; num5++)
			{
				text += array5[num5];
				if (num5 < array5.Length - 1)
				{
					text += ",";
				}
			}
			if (num4 < instance.badges.Count - 1)
			{
				text += "@";
			}
		}
		text += "\n";
		for (int num6 = 0; num6 < instance.samiramusics.Count; num6++)
		{
			int[] array6 = instance.samiramusics[num6];
			for (int num7 = 0; num7 < array6.Length; num7++)
			{
				text += array6[num7];
				if (num7 < array6.Length - 1)
				{
					text += ",";
				}
			}
			if (num6 < instance.samiramusics.Count - 1)
			{
				text += "@";
			}
		}
		text += "\n";
		for (int num8 = 0; num8 < instance.statbonus.Count; num8++)
		{
			int[] array7 = instance.statbonus[num8];
			for (int num9 = 0; num9 < array7.Length; num9++)
			{
				text += array7[num9];
				if (num9 < array7.Length - 1)
				{
					text += ",";
				}
			}
			if (num8 < instance.statbonus.Count - 1)
			{
				text += "@";
			}
		}
		text += "\n";
		for (int num10 = 0; num10 < instance.librarystuff.GetLength(0); num10++)
		{
			for (int num11 = 0; num11 < instance.librarystuff.GetLength(1); num11++)
			{
				text += instance.librarystuff[num10, num11];
				if (num11 < instance.librarystuff.GetLength(1) - 1)
				{
					text += ",";
				}
			}
			if (num10 < instance.librarystuff.GetLength(0) - 1)
			{
				text += "@";
			}
		}
		text += "\n";
		for (int num12 = 0; num12 < instance.flags.Length; num12++)
		{
			text += instance.flags[num12];
			if (num12 < instance.flags.Length - 1)
			{
				text += ",";
			}
		}
		text += "\n";
		for (int num13 = 0; num13 < instance.flagstring.Length; num13++)
		{
			text += instance.flagstring[num13];
			if (num13 < instance.flagstring.Length - 1)
			{
				text += "|SPLIT|";
			}
		}
		text += "\n";
		for (int num14 = 0; num14 < instance.flagvar.Length; num14++)
		{
			text += instance.flagvar[num14];
			if (num14 < instance.flagvar.Length - 1)
			{
				text += ",";
			}
		}
		text += "\n";
		for (int num15 = 0; num15 < instance.regionalflags.Length; num15++)
		{
			text += instance.regionalflags[num15];
			if (num15 < instance.regionalflags.Length - 1)
			{
				text += ",";
			}
		}
		text += "\n";
		for (int num16 = 0; num16 < instance.crystalbflags.Length; num16++)
		{
			text += instance.crystalbflags[num16];
			if (num16 < instance.crystalbflags.Length - 1)
			{
				text += ",";
			}
		}
		text += "\n";
		for (int num17 = 0; num17 < instance.extrafollowers.Count; num17++)
		{
			text += instance.extrafollowers[num17];
			if (num17 < instance.extrafollowers.Count - 1)
			{
				text += ",";
			}
		}
		text += "\n";
		for (int num18 = 0; num18 < instance.enemyencounter.GetLength(0); num18++)
		{
			text = text + instance.enemyencounter[num18, 0] + "," + instance.enemyencounter[num18, 1];
			if (num18 < instance.enemyencounter.GetLength(0) - 1)
			{
				text += "@";
			}
		}
		return text;
	}

	public void UpdateList(Directions dir)
	{
		UpdateList(dir, false);
	}

	public void UpdateList(Directions dir, bool nosound)
	{
		switch (dir)
		{
		case Directions.Down:
			if (option + 1 < maxoptions)
			{
				if (!nosound)
				{
					PlayScrollSound();
				}
				option++;
				if (listmax + 1 <= maxoptions && option == listmax)
				{
					listmax++;
					listlow++;
				}
				else
				{
					listcursor++;
				}
			}
			break;
		case Directions.Up:
			if (option - 1 >= 0)
			{
				if (!nosound)
				{
					PlayScrollSound();
				}
				option--;
				if (listlow - 1 >= 0 && option == listlow - 1)
				{
					listmax--;
					listlow--;
				}
				else
				{
					listcursor--;
				}
			}
			break;
		}
	}

	public static bool Interval(float value, float min, float max, bool inclusive)
	{
		if (inclusive)
		{
			if (value <= max)
			{
				return value >= min;
			}
			return false;
		}
		if (value < max)
		{
			return value > min;
		}
		return false;
	}

	private void RefreshDiscovery()
	{
		if (discoverymessage == null)
		{
			discoverymessage = NewUIObject("Discovery", GUICamera.transform, new Vector3(-8f, -6f)).transform;
			GameObject obj = UnityEngine.Object.Instantiate(Resources.Load("Prefabs/Objects/logbookicon")) as GameObject;
			obj.transform.parent = discoverymessage;
			obj.transform.localPosition = new Vector3(0f, -1f, 10f);
			instance.StartCoroutine(SetText("|single||color,4||dropshadow,1,-1|" + menutext[102], 1, null, false, false, new Vector3(1.2f, -1f, 10f), Vector3.zero, Vector2.one, discoverymessage, null));
			instance.StartCoroutine(SetText("|single||color,4||dropshadow,1,-1|" + menutext[154], 1, null, false, false, new Vector3(1.2f, -1f, 10f), Vector3.zero, Vector2.one, discoverymessage, null));
			instance.StartCoroutine(SetText("|single||color,4||dropshadow,1,-1|" + menutext[155], 1, null, false, false, new Vector3(1.2f, -1f, 10f), Vector3.zero, Vector2.one, discoverymessage, null));
			discoverymessage.GetChild(2).gameObject.SetActive(false);
			discoverymessage.GetChild(3).gameObject.SetActive(false);
		}
		if (discoveryhud > 0f)
		{
			discoverymessage.localPosition = Vector3.Lerp(discoverymessage.localPosition, new Vector3(-8f, -3f), TieFramerate(0.1f));
		}
		else
		{
			discoverymessage.localPosition = Vector3.Lerp(discoverymessage.localPosition, new Vector3(-8f, -6f), TieFramerate(0.1f));
		}
	}

	public static void UpdateOutlines()
	{
		if (map != null && map.areaid == Areas.MetalLake)
		{
			if (enableoutline == 0)
			{
				Shader.SetGlobalFloat("GlobalOutline", 0f);
			}
			else
			{
				Shader.SetGlobalFloat("GlobalOutline", 0.1f);
			}
			return;
		}
		switch (enableoutline)
		{
		case 0:
			Shader.SetGlobalFloat("GlobalOutline", 0f);
			break;
		case 1:
			Shader.SetGlobalFloat("GlobalOutline", 0.1f);
			break;
		case 2:
			Shader.SetGlobalFloat("GlobalOutline", 0.3f);
			break;
		}
	}

	private void LateUpdate()
	{
		messagebreak = ((languageid <= 0) ? 9.75f : 10.5f);
		itemdescbreak = ((languageid > 0) ? 9.9f : 10.5f);
		framestep = TieFramerate(1f);
		if (!basicload)
		{
			return;
		}
		UpdateOutlines();
		GUICamera.transform.localEulerAngles = Vector3.zero;
		if (globalcamdir != null)
		{
			globalcamdir.transform.eulerAngles = new Vector3(0f, MainCamera.transform.eulerAngles.y, 0f);
		}
		if (globalcooldown > 0f)
		{
			globalcooldown -= TieFramerate(1f);
		}
		if (instance.playerdata != null && instance.playerdata.Length != 0)
		{
			RefreshHUD();
			RefreshDiscovery();
		}
		if (texttail != null)
		{
			if (tailtarget != null)
			{
				texttail.gameObject.SetActive(true);
				texttail.transform.position = tailtarget.position;
				float num = texttail.transform.localEulerAngles.z;
				if (num > 180f)
				{
					num -= 360f;
				}
				num = Mathf.Clamp(-1f - Mathf.Abs(num) / 60f, -1.7f, -1.1f);
				texttail.transform.localPosition = new Vector3(Mathf.Clamp(texttail.transform.localPosition.x, -3f, 3f), num, 0f);
				Vector2 vector = new Vector2(MainCamera.WorldToViewportPoint(tailtarget.transform.position).x - MainCamera.WorldToViewportPoint(texttail.transform.position).x, tailtarget.transform.position.y - texttail.transform.position.y);
				float value = vector.x / vector.y * (0f - MainCamera.WorldToViewportPoint(tailtarget.transform.position).z) * 100f;
				texttail.transform.eulerAngles = new Vector3(texttail.transform.eulerAngles.x, globalcamdir.eulerAngles.y, Mathf.Clamp(value, -50f, 50f));
			}
			else
			{
				texttail.gameObject.SetActive(false);
			}
		}
		if (!started && player != null)
		{
			player.entity.DetectIgnoreSphere(true);
			started = true;
		}
		for (int i = 0; i < 2; i++)
		{
			if (!inbattle && inevent)
			{
				letterbox[i].color = Color.Lerp(letterbox[i].color, Color.black, TieFramerate(0.15f));
			}
			else
			{
				letterbox[i].color = Color.Lerp(letterbox[i].color, Color.clear, TieFramerate(0.15f));
			}
		}
		if (inputcooldown > 0f)
		{
			inputcooldown -= framestep;
		}
		if (switchicon != null)
		{
			switchicon.color = Color.Lerp(switchicon.color, Color.clear, TieFramerate(0.01f));
		}
		stickholdx = Mathf.Abs(InputIO.JoyStick(0)) < 0.5f && Mathf.Abs(InputIO.JoyStick(2)) < 0.5f;
		stickholdy = Mathf.Abs(InputIO.JoyStick(1)) < 0.5f && Mathf.Abs(InputIO.JoyStick(3)) < 0.5f;
		if (!started)
		{
			return;
		}
		if (flagvar != null && flagvar.Length >= 27)
		{
			if (flagvar[27] > 9999)
			{
				flagvar[27] = 9999;
			}
			if (flagvar[26] > 10000)
			{
				flagvar[26] = 10000;
			}
		}
		for (int j = 0; j < bountyquests.Length; j++)
		{
			if (boardquests[2].Contains(bountyquests[j]))
			{
				if (boardquests[0].Contains(bountyquests[j]))
				{
					boardquests[0].Remove(bountyquests[j]);
				}
				if (boardquests[1].Contains(bountyquests[j]))
				{
					boardquests[1].Remove(bountyquests[j]);
				}
			}
		}
		if (partylevel == 27)
		{
			partyexp = neededexp;
		}
	}

	public static int MixIngredients(int item1, int item2)
	{
		int result = 8;
		int length = recipedata.GetLength(0);
		if ((item1 > item2 && item2 > -1) || item2 == 156)
		{
			int num = item1;
			item1 = item2;
			item2 = num;
		}
		if (item1 == 156 || item2 == 156)
		{
			return BattleControl.GetChance((item2 == 156) ? new int[13]
			{
				34, 171, 171, 39, 124, 124, 8, 91, 91, 78,
				30, 172, 91
			} : new int[11]
			{
				34, 34, 8, 8, 171, 39, 8, 91, 124, 8,
				171
			});
		}
		for (int i = 0; i < length; i++)
		{
			if (recipedata[i, 0] == item1 && recipedata[i, 1] == item2)
			{
				result = recipedata[i, 2];
				break;
			}
		}
		return result;
	}

	public static bool HasRecipe(int id)
	{
		for (int i = 0; i < librarylimit[2]; i++)
		{
			if (libraryorder[2, i] == id)
			{
				return instance.librarystuff[2, i];
			}
		}
		return false;
	}

	public static int GetRecipeID(int id)
	{
		for (int i = 0; i < librarylimit[2]; i++)
		{
			if (libraryorder[2, i] == id)
			{
				return i;
			}
		}
		return -1;
	}

	public static void ResetCamera()
	{
		instance.camspeed = 0.1f;
		instance.camoffsetspeed = 0.1f;
		instance.changecamspeed = false;
		instance.camanglechange = false;
		instance.camoffset2 = Vector3.zero;
		instance.camanglespeed = 0.1f;
		instance.camtargetpos = null;
		instance.camoffset = defaultcamoffset;
		instance.camangleoffset = defaultcamangle;
		if (map != null)
		{
			if (map.camoffset.magnitude > 0.1f)
			{
				instance.camoffset = map.camoffset;
			}
			if (map.camangle.magnitude > 0.1f)
			{
				instance.camangleoffset = map.camangle;
			}
		}
		if (player != null)
		{
			instance.camtarget = player.transform;
		}
		else
		{
			instance.camtarget = null;
		}
	}

	public static void ResetCamera(bool instant)
	{
		ResetCamera();
		if (instant)
		{
			instance.camspeed = 1f;
			instance.Invoke("ResetCamSpeed", 0.1f);
		}
	}

	private void ResetCamSpeed()
	{
		instance.camspeed = 0.1f;
		instance.camanglespeed = 0.1f;
	}

	public static bool PartyIsNotMoving()
	{
		for (int i = 0; i < instance.playerdata.Length; i++)
		{
			if (instance.playerdata[i].entity.forcemove)
			{
				return false;
			}
		}
		return true;
	}

	public static bool GetKey(int id)
	{
		return GetKey(id, false);
	}

	public static Vector2 GetPressedDirection(bool hold)
	{
		Vector2 zero = Vector2.zero;
		if (GetKey(0, hold))
		{
			zero += Vector2.up;
		}
		else if (GetKey(1, hold))
		{
			zero += Vector2.down;
		}
		if (GetKey(2, hold))
		{
			zero += Vector2.left;
		}
		else if (GetKey(3, hold))
		{
			zero += Vector2.right;
		}
		return zero.normalized;
	}

	public static bool GetKey(int id, bool hold)
	{
		switch (id)
		{
		case -4:
		{
			for (int i = 0; i < 10; i++)
			{
				if (GetKey(i, hold))
				{
					return true;
				}
			}
			return false;
		}
		case -3:
			if (!GetKey(4, hold) && !GetKey(5, hold) && !GetKey(6, hold))
			{
				return GetKey(7, hold);
			}
			return true;
		case -2:
			if (!hold)
			{
				if (joystick && usejoystick > 0)
				{
					if ((InputIO.JoyStick(0) == 0f || !stickholdx) && (InputIO.JoyStick(1) == 0f || !stickholdy) && (InputIO.JoyStick(2) == 0f || !stickholdx))
					{
						if (InputIO.JoyStick(3) != 0f)
						{
							return stickholdy;
						}
						return false;
					}
					return true;
				}
				if (!InputIO.GetKeyDown(0, false) && !InputIO.GetKeyDown(1, false) && !InputIO.GetKeyDown(2, false))
				{
					return InputIO.GetKeyDown(3, false);
				}
				return true;
			}
			if (joystick && usejoystick > 0)
			{
				if (InputIO.JoyStick(0) == 0f && InputIO.JoyStick(1) == 0f && InputIO.JoyStick(2) == 0f)
				{
					return InputIO.JoyStick(3) != 0f;
				}
				return true;
			}
			if (!InputIO.GetKey(0, false) && !InputIO.GetKey(1, false) && !InputIO.GetKey(2, false))
			{
				return InputIO.GetKey(3, false);
			}
			return true;
		case -1:
			if (!hold)
			{
				return InputIO.anyKeyDown();
			}
			return InputIO.anyKey();
		default:
			if (!hold)
			{
				if (usejoystick > 0)
				{
					if (id == 0 && (InputIO.JoyStick(1) < -0.5f || InputIO.JoyStick(3) > 0.5f) && stickholdy)
					{
						return true;
					}
					if (id == 1 && (InputIO.JoyStick(1) > 0.5f || InputIO.JoyStick(3) < -0.5f) && stickholdy)
					{
						return true;
					}
					if (id == 2 && (InputIO.JoyStick(0) < -0.5f || InputIO.JoyStick(2) < -0.5f) && stickholdx)
					{
						return true;
					}
					if (id == 3 && (InputIO.JoyStick(0) > 0.5f || InputIO.JoyStick(2) > 0.5f) && stickholdx)
					{
						return true;
					}
					if (id == 4 && InputIO.GetKeyDown(0, true))
					{
						return true;
					}
					if (id == 5 && InputIO.GetKeyDown(1, true))
					{
						return true;
					}
					if (id == 6 && InputIO.GetKeyDown(2, true))
					{
						return true;
					}
					if (id == 7 && InputIO.GetKeyDown(3, true))
					{
						return true;
					}
					if (id == 8 && InputIO.GetKeyDown(4, true))
					{
						return true;
					}
					if (id == 9 && InputIO.GetKeyDown(5, true))
					{
						return true;
					}
				}
				return InputIO.GetKeyDown(id, false);
			}
			if (usejoystick > 0)
			{
				if (id == 0 && (InputIO.JoyStick(1) < 0f || InputIO.JoyStick(3) > 0f))
				{
					return true;
				}
				if (id == 1 && (InputIO.JoyStick(1) > 0f || InputIO.JoyStick(3) < 0f))
				{
					return true;
				}
				if (id == 2 && (InputIO.JoyStick(0) < 0f || InputIO.JoyStick(2) < 0f))
				{
					return true;
				}
				if (id == 3 && (InputIO.JoyStick(0) > 0f || InputIO.JoyStick(2) > 0f))
				{
					return true;
				}
				if (id == 4 && InputIO.GetKey(0, true))
				{
					return true;
				}
				if (id == 5 && InputIO.GetKey(1, true))
				{
					return true;
				}
				if (id == 6 && InputIO.GetKey(2, true))
				{
					return true;
				}
				if (id == 7 && InputIO.GetKey(3, true))
				{
					return true;
				}
				if (id == 8 && InputIO.GetKey(4, true))
				{
					return true;
				}
				if (id == 9 && InputIO.GetKey(5, true))
				{
					return true;
				}
			}
			return InputIO.GetKey(id, false);
		}
	}

	private void FixedUpdate()
	{
		if (basicload)
		{
			RefreshCamera();
			LoopMusic();
		}
	}

	public static float DimishingReturns(float input, float decay)
	{
		return (input - 1f) * decay;
	}

	public static float[][] LoopPoint()
	{
		string[] array = Resources.Load<TextAsset>("Data/LoopPoints").ToString().Split('\n');
		List<float[]> list = new List<float[]>();
		for (int i = 0; i < array.Length; i++)
		{
			string[] array2 = array[i].Split(';');
			list.Add(new float[2]
			{
				Convert.ToSingle(array2[0]),
				Convert.ToSingle(array2[1])
			});
		}
		return list.ToArray();
	}

	private void LoopMusic()
	{
		if (musicloop == null || musicloop.Length == 0)
		{
			musicloop = LoopPoint();
		}
		for (int i = 0; i < music.Length; i++)
		{
			if ((musicloop[lastmusic][0] != 0f || musicloop[lastmusic][0] != 0f) && music[i].time >= musicloop[lastmusic][0])
			{
				music[i].time = musicloop[lastmusic][1];
			}
		}
	}

	public static int GetRandomMedal()
	{
		return GetRandomMedal(false, false);
	}

	public static int GetRandomMedal(bool dontremove, bool random)
	{
		if (instance.flagstring[13].Length == 0)
		{
			instance.flagstring[13] = "0,1,2,3,4,5,6,7,8,9";
		}
		string[] array = instance.flagstring[13].Split(',');
		int[] array2 = new int[array.Length];
		for (int i = 0; i < array2.Length; i++)
		{
			array2[i] = Convert.ToInt32(array[i]);
		}
		int num = -1;
		int num2 = 0;
		if (random)
		{
			List<int> array3 = new List<int>(array2);
			RandomSort(ref array3);
			array2 = array3.ToArray();
			num2 = UnityEngine.Random.Range(0, array2.Length);
		}
		num = array2[num2];
		instance.flagstring[13] = "";
		for (int j = 0; j < array2.Length; j++)
		{
			if (j != num2 || dontremove)
			{
				instance.flagstring[13] += array2[j];
				if (j < array2.Length - 1)
				{
					instance.flagstring[13] += ",";
				}
			}
		}
		return num;
	}

	public static GameObject NewUIObject(string objname, Transform parent, Vector3 pos)
	{
		return NewUIObject(objname, parent, pos, Vector3.one, null, 0);
	}

	public static GameObject NewUIObject(string objname, Transform parent, Vector3 pos, Vector3 size, Sprite sprite)
	{
		return NewUIObject(objname, parent, pos, size, sprite, 0);
	}

	public static GameObject NewUIObject(string objname, Transform parent, Vector3 pos, Vector3 size, Sprite sprite, int sortorder)
	{
		GameObject gameObject = new GameObject(objname);
		if (parent == null)
		{
			gameObject.transform.parent = GUICamera.transform;
		}
		else
		{
			gameObject.transform.parent = parent;
		}
		gameObject.layer = 5;
		gameObject.transform.localEulerAngles = Vector3.zero;
		gameObject.transform.localPosition = pos;
		gameObject.transform.localScale = size;
		if (sprite != null)
		{
			SpriteRenderer spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
			spriteRenderer.sprite = sprite;
			spriteRenderer.sortingOrder = sortorder;
		}
		return gameObject;
	}

	public static bool CheckIfCanExist(int[] requires, int[] limit, int regionalflag)
	{
		if (limit != null && limit.Length != 0)
		{
			List<int> list = new List<int>();
			for (int i = 0; i < limit.Length; i++)
			{
				if (limit[i] < -1)
				{
					if (instance.flags[Mathf.Abs(limit[i])])
					{
						return true;
					}
				}
				else
				{
					list.Add(limit[i]);
				}
			}
			limit = list.ToArray();
		}
		if ((requires != null && requires.Length != 0 && requires[0] > -1 && !CheckAllBool(instance.flags, requires, true)) || (limit != null && limit.Length != 0 && limit[0] > -1 && CheckAllBool(instance.flags, limit, true)) || (regionalflag > -1 && map != null && instance.regionalflags[regionalflag]))
		{
			return true;
		}
		return false;
	}

	private static Vector3 CamLimiter(Vector3 pos)
	{
		return new Vector3(Mathf.Clamp(pos.x, map.camlimitneg.x, map.camlimitpos.x), Mathf.Clamp(pos.y, map.camlimitneg.y, map.camlimitpos.y), Mathf.Clamp(pos.z, map.camlimitneg.z, map.camlimitpos.z));
	}

	public static Vector3 LimitRadius(Vector3 pos, Vector3 origin, float radius)
	{
		Vector3 vector = Vector3.ClampMagnitude(pos - origin, radius);
		return origin + new Vector3(vector.x, pos.y, vector.z);
	}

	public static Vector3 LimitRadius(Vector3 pos, Vector3 origin, float radius, bool ignoreY)
	{
		Vector3 vector = Vector3.ClampMagnitude(pos - origin, radius);
		if (ignoreY)
		{
			Vector3 vector2 = new Vector3(origin.x, 0f, origin.z) + new Vector3(vector.x, 0f, vector.z);
			return new Vector3(vector2.x, pos.y, vector2.z);
		}
		return origin + new Vector3(vector.x, pos.y, vector.z);
	}

	private void RefreshCamera()
	{
		if (MainCamera.transform.parent != null)
		{
			if (camtarget != null)
			{
				if (map != null)
				{
					if (map.tetherdistance > 0f && insideid == -1)
					{
						MainCamera.transform.parent.position = Vector3.Lerp(MainCamera.transform.parent.position, LimitRadius(CamLimiter(camtarget.transform.position), map.actualcenter, map.tetherdistance), camspeed);
					}
					else
					{
						MainCamera.transform.parent.position = Vector3.Lerp(MainCamera.transform.parent.position, CamLimiter(camtarget.transform.position), camspeed);
					}
				}
				else
				{
					MainCamera.transform.parent.position = Vector3.Lerp(MainCamera.transform.parent.position, camtarget.transform.position, camspeed);
				}
			}
			else if (camtargetpos.HasValue)
			{
				Vector3 value = camtargetpos.Value;
				if (map != null)
				{
					if (map.tetherdistance > 0f && insideid == -1)
					{
						MainCamera.transform.parent.position = Vector3.Lerp(MainCamera.transform.parent.position, LimitRadius(CamLimiter(camtargetpos.Value), map.actualcenter, map.tetherdistance), camspeed);
					}
					else
					{
						MainCamera.transform.parent.position = Vector3.Lerp(MainCamera.transform.parent.position, CamLimiter(value), camspeed);
					}
				}
				else
				{
					MainCamera.transform.parent.position = Vector3.Lerp(MainCamera.transform.parent.position, value, camspeed);
				}
			}
		}
		Vector3 vector = Vector3.zero;
		if (screenshake != Vector3.zero)
		{
			vector = RandomVector(screenshake);
		}
		MainCamera.transform.localPosition = Vector3.Lerp(MainCamera.transform.localPosition, camoffset + camoffset2, camspeed) + vector;
		if (map != null && map.rotatecam && insideid == -1)
		{
			MainCamera.transform.LookAt(map.actualcenter);
			MainCamera.transform.parent.LookAt(map.actualcenter);
			if (map.tieYtoplayer)
			{
				MainCamera.transform.parent.transform.eulerAngles = new Vector3(0f, MainCamera.transform.parent.transform.eulerAngles.y, MainCamera.transform.parent.transform.eulerAngles.z);
			}
		}
		else
		{
			float t = (camanglechange ? camanglespeed : camspeed);
			MainCamera.transform.localEulerAngles = new Vector3(Mathf.LerpAngle(MainCamera.transform.localEulerAngles.x, 0f, t), Mathf.LerpAngle(MainCamera.transform.localEulerAngles.y, 0f, t), Mathf.LerpAngle(MainCamera.transform.localEulerAngles.z, 0f, t));
			MainCamera.transform.parent.localEulerAngles = new Vector3(Mathf.LerpAngle(MainCamera.transform.parent.localEulerAngles.x, camangleoffset.x, t), Mathf.LerpAngle(MainCamera.transform.parent.localEulerAngles.y, camangleoffset.y, t), Mathf.LerpAngle(MainCamera.transform.parent.localEulerAngles.z, camangleoffset.z, t));
		}
	}

	public static void SaveCameraPosition()
	{
		SaveCameraPosition(true);
	}

	public static void LoadCameraPosition()
	{
		SaveCameraPosition(false);
	}

	public static void SaveCameraPosition(bool set)
	{
		if (set)
		{
			tempcampos = instance.camtargetpos;
			tempcamoffset = instance.camoffset;
			tempcamspeed = instance.camspeed;
			tempcamangleoffset = instance.camangleoffset;
			tempcamtarget = instance.camtarget;
			tempmaplp = map.camlimitpos;
			tempmapln = map.camlimitneg;
		}
		else
		{
			instance.camtargetpos = tempcampos;
			instance.camspeed = tempcamspeed;
			instance.camoffset = tempcamoffset;
			instance.camangleoffset = tempcamangleoffset;
			instance.camtarget = tempcamtarget;
			map.camlimitpos = tempmaplp;
			map.camlimitneg = tempmapln;
		}
	}

	public static Vector3 LerpVectorAngle(Vector3 input, Vector3 target, float ammount)
	{
		return new Vector3(Mathf.LerpAngle(input.x, target.x, ammount), Mathf.LerpAngle(input.y, target.y, ammount), Mathf.LerpAngle(input.z, target.z, ammount));
	}

	public static Vector3 LerpVectorAngleSmooth(Vector3 input, Vector3 target, Vector3 current, float ammount)
	{
		float currentVelocity = current.x;
		float currentVelocity2 = current.y;
		float currentVelocity3 = current.z;
		Mathf.SmoothDampAngle(input.x, target.x, ref currentVelocity, ammount);
		Mathf.SmoothDampAngle(input.y, target.y, ref currentVelocity2, ammount);
		Mathf.SmoothDampAngle(input.z, target.z, ref currentVelocity3, ammount);
		return new Vector3(currentVelocity, currentVelocity2, currentVelocity3);
	}

	public static void RebuildHUD()
	{
		ApplyBadges();
		ApplyStatBonus();
		if (instance.hud != null && instance.hud.Length != 0)
		{
			UnityEngine.Object.Destroy(instance.hud[0].parent.gameObject);
		}
		instance.hud = null;
	}

	private static void CreateHUD()
	{
		ApplyBadges();
		ApplyStatBonus();
		instance.hud = new Transform[5];
		hudsprites = new SpriteRenderer[4];
		instance.hudfont = new DynamicFont[5];
		GameObject gameObject = new GameObject("HUD");
		gameObject.transform.parent = GUICamera.transform;
		gameObject.transform.localEulerAngles = Vector3.zero;
		gameObject.transform.localPosition = Vector3.zero;
		float num = -7f;
		for (int i = 0; i < 4; i++)
		{
			NewHUDElement(i, new Vector2(num, 8.5f), gameObject.transform);
			num += 3.5f;
			if (i == 2)
			{
				num += 3.5f;
			}
		}
		NewHUDElement(4, new Vector2(instance.hud[3].transform.position.x, 0f - instance.hud[3].transform.position.y), gameObject.transform);
		hudvalue = new float[6] { 6.5f, 7.5f, 8.5f, 9.5f, -6.5f, -7f };
	}

	private static void NewHUDElement(int hudid, Vector2 position, Transform parent)
	{
		GameObject gameObject = new GameObject("hud" + hudid);
		SpriteRenderer spriteRenderer = new GameObject("basesprite" + hudid).AddComponent<SpriteRenderer>();
		instance.hud[hudid] = gameObject.transform;
		instance.hud[hudid].parent = parent.transform;
		spriteRenderer.transform.parent = instance.hud[hudid];
		spriteRenderer.gameObject.layer = 5;
		spriteRenderer.sprite = guisprites[4];
		spriteRenderer.sortingOrder = 10;
		spriteRenderer.transform.localScale = new Vector3(0.55f, 0.65f, 1f);
		instance.hud[hudid].transform.localPosition = new Vector3(position.x, position.y, 10f);
		SpriteRenderer spriteRenderer2 = new GameObject("facesprite").AddComponent<SpriteRenderer>();
		spriteRenderer2.transform.localScale = Vector3.one * 0.8f;
		spriteRenderer2.sortingOrder = spriteRenderer.sortingOrder + 2;
		spriteRenderer2.gameObject.layer = 5;
		spriteRenderer2.transform.parent = spriteRenderer.transform;
		spriteRenderer2.transform.localPosition = new Vector2(-2.3f, 0f);
		if (hudid < 3 && hudid < instance.playerdata.Length)
		{
			spriteRenderer.color = instance.charcolor[instance.playerdata[hudid].animid];
			spriteRenderer2.sprite = guisprites[instance.playerdata[hudid].animid + 5];
			hudsprites[hudid] = spriteRenderer;
			NewUIObject("hpicon", spriteRenderer2.transform, new Vector3(0.45f, -0.6f), Vector3.one * 0.5f, guisprites[24], spriteRenderer2.sortingOrder + 1);
			if (hudid < instance.playerdata.Length)
			{
				instance.hudfont[hudid] = DynamicFont.SetUp(instance.playerdata[hudid].hpt.ToString().PadLeft(2, '0') + "/" + instance.playerdata[hudid].maxhp.ToString().PadLeft(2, '0'), false, true, 2f, 2, spriteRenderer.sortingOrder + 1, Vector2.one * 1.75f, spriteRenderer.transform, new Vector3(-0.9f, -0.6f), Color.white, new Vector2(3f, 0.85f));
				instance.hudfont[hudid].dropshadow = true;
			}
		}
		else
		{
			switch (hudid)
			{
			case 3:
				hudsprites[3] = spriteRenderer;
				spriteRenderer.color = instance.menucolors[3];
				spriteRenderer2.sprite = guisprites[28];
				instance.hudfont[hudid] = DynamicFont.SetUp(instance.tpt.ToString().PadLeft(2, '0') + "/" + instance.maxtp.ToString().PadLeft(2, '0'), false, true, 2f, 2, spriteRenderer.sortingOrder + 1, Vector2.one * 1.75f, spriteRenderer.transform, new Vector3(-0.9f, -0.6f), Color.white, new Vector2(3f, 0.85f));
				instance.hudfont[hudid].dropshadow = true;
				break;
			case 4:
				spriteRenderer.color = instance.menucolors[0];
				spriteRenderer2.sprite = guisprites[29];
				instance.hudfont[hudid] = DynamicFont.SetUp(instance.moneyt.ToString().PadLeft(3, '0'), false, true, 2f, 2, spriteRenderer.sortingOrder + 1, Vector2.one * 1.75f, spriteRenderer.transform, new Vector3(-0.75f, -0.6f), Color.white);
				instance.hudfont[hudid].dropshadow = true;
				break;
			}
		}
		instance.hud[hudid].transform.localEulerAngles = Vector3.zero;
		spriteRenderer.transform.localEulerAngles = Vector3.zero;
	}

	public static float QuadraticY(float x1, float x2, float ymax, float currentx)
	{
		return QuadraticY(new Vector2(x1, 0f), new Vector2(x2, 0f), ymax, currentx);
	}

	public static string GetItemString(bool emptyinv)
	{
		string text = "";
		for (int i = 0; i < instance.items[0].Count; i++)
		{
			text += instance.items[0][i];
			if (i < instance.items[0].Count - 1)
			{
				text += ",";
			}
		}
		if (emptyinv)
		{
			instance.items[0] = new List<int>();
		}
		return text;
	}

	public static float QuadraticY(Vector2 x1, Vector2 x2, float ymax, float currentx)
	{
		return BeizierCurve(x1, x2, ymax, GetPercentage(x1.x, x2.x, currentx)).y;
	}

	public static Vector2 BeizierCurve(Vector2 start, Vector2 end, float ymax, float t)
	{
		Vector2 vector = new Vector2((start.x + end.x) / 2f, ymax);
		Vector2 vector2 = (1f - t) * (1f - t) * start;
		Vector2 vector3 = 2f * t * (1f - t) * vector;
		Vector2 vector4 = t * t * end;
		return vector2 + vector3 + vector4;
	}

	public static bool EntitiesAreNotMoving(EntityControl[] entities)
	{
		for (int i = 0; i < entities.Length; i++)
		{
			if (entities[i].forcemove)
			{
				return false;
			}
		}
		return true;
	}

	public static string GetBleep(EntityControl entity)
	{
		return GetBleep(entity, 1f);
	}

	public static string GetBleep(EntityControl entity, float volume)
	{
		return "|bleep," + entity.dialoguebleepid + "," + entity.bleeppitch + "," + volume + "|";
	}

	public static Vector3 BeizierCurve3(Vector3 start, Vector3 end, float ymax, float t)
	{
		return BeizierCurve3(start, end, new Vector3((start.x + end.x) / 2f, (ymax > 0f) ? ((start.y + end.y) / 2f + ymax) : ymax, (start.z + end.z) / 2f), t);
	}

	public static bool AsianLang()
	{
		return languageid == 3;
	}

	public static Vector3 BeizierCurve3(Vector3 start, Vector3 end, Vector3 mid, float t)
	{
		t = Mathf.Clamp01(t);
		return (1f - t) * (1f - t) * start + 2f * t * (1f - t) * mid + t * t * end;
	}

	public static float BeizierFloat(float height, float t)
	{
		t = Mathf.Clamp01(t);
		return 2f * t * (1f - t) * height;
	}

	public static float QuadraticYSemiClamped(float currentx, float startx, float endx, float modifier)
	{
		return (0f - (modifier + GetDistance(startx, endx) / 10f) / GetDistance(startx, endx)) * (currentx - endx) * (currentx - startx);
	}

	public static float QuadraticYUnclamped(float currentx, float startx, float endx, float modifier)
	{
		return (0f - modifier) * (currentx - endx) * (currentx - startx);
	}

	public static Vector3 RandomVector(Vector3 input)
	{
		return new Vector3(UnityEngine.Random.Range(0f - input.x, input.x), UnityEngine.Random.Range(0f - input.y, input.y), UnityEngine.Random.Range(0f - input.z, input.z));
	}

	public static Vector3 RandomVector(float randomx, float randomy, float randomz)
	{
		return new Vector3(UnityEngine.Random.Range(0f - randomx, randomx), UnityEngine.Random.Range(0f - randomy, randomy), UnityEngine.Random.Range(0f - randomz, randomz));
	}

	public static Vector3 RandomVector(float input)
	{
		return new Vector3(UnityEngine.Random.Range(0f - input, input), UnityEngine.Random.Range(0f - input, input), UnityEngine.Random.Range(0f - input, input));
	}

	public static Vector3 RandomVector(Vector2 input)
	{
		return new Vector3(UnityEngine.Random.Range(0f - input.x, input.x), UnityEngine.Random.Range(0f - input.y, input.y));
	}

	public static Vector3 RandomVector(float randomx, float randomy)
	{
		return new Vector3(UnityEngine.Random.Range(0f - randomx, randomx), UnityEngine.Random.Range(0f - randomy, randomy));
	}

	public static float ColorDifference(Color a, Color b)
	{
		return new Vector3(a.r - b.r, a.g - b.g, a.b - b.b).magnitude;
	}

	public static void GlobalCommand(ref string text)
	{
		if (map != null && map.useglobalcommand && map.currentline > -1)
		{
			string[] array = map.commandlines[map.currentline].Split(';');
			Regex regex = new Regex(Regex.Escape("@"));
			for (int i = 0; i < array.Length; i++)
			{
				text = regex.Replace(text, array[i], 1);
			}
		}
	}

	private static void ForceHUD()
	{
		if (tphp != null)
		{
			for (int i = 0; i < tphp.Length; i++)
			{
				tphp[i] = -1;
			}
		}
		if (ptmhp != null)
		{
			for (int j = 0; j < ptmhp.Length; j++)
			{
				ptmhp[j] = -1;
			}
		}
		if (tmtp != null)
		{
			for (int k = 0; k < tmtp.Length; k++)
			{
				tmtp[k] = -1;
			}
		}
		tempmoneh = -1;
	}

	private static void RefreshHUD()
	{
		if (instance.hud == null || instance.hud.Length == 0)
		{
			CreateHUD();
			tphp = new int[instance.playerdata.Length];
			ptmhp = new int[instance.playerdata.Length];
			instance.hud[4].localPosition = new Vector3(instance.hud[3].localPosition.x, instance.hud[4].localPosition.y, instance.hud[4].localPosition.z);
		}
		else if (instance.hud.Length != 0 && instance.hudcooldown > 0f)
		{
			if (!instance.inbattle)
			{
				instance.hudcooldown -= framestep;
			}
			for (int i = 0; i < instance.playerdata.Length; i++)
			{
				if (instance.playerdata[i].hpt > instance.playerdata[i].hp)
				{
					instance.playerdata[i].hpt--;
				}
				else if (instance.playerdata[i].hpt < instance.playerdata[i].hp)
				{
					instance.playerdata[i].hpt++;
					if (instance.playerdata[i].hpt != instance.playerdata[i].hp && !sounds[5].isPlaying)
					{
						PlaySound("TP", 5, 1.15f + UnityEngine.Random.Range(-0.1f, 0.1f), 1f);
					}
				}
				if (tphp[i] != instance.playerdata[i].hpt || ptmhp[i] != instance.playerdata[i].maxhp)
				{
					instance.hudfont[i].text = string.Concat(instance.playerdata[i].hpt).PadLeft(2, '0') + "/" + string.Concat(instance.playerdata[i].maxhp).PadLeft(2, '0');
					tphp[i] = instance.playerdata[i].hpt;
					ptmhp[i] = instance.playerdata[i].maxhp;
				}
				if (instance.playerdata[i].hp > 0 && instance.playerdata[i].hp <= 4 && ColorDifference(hudsprites[i].color, instance.charcolor[instance.playerdata[i].trueid]) < 0.1f)
				{
					hudsprites[i].color = Color.red;
				}
				else if (instance.playerdata[i].hp == 0)
				{
					hudsprites[i].color = Color.gray;
				}
				else
				{
					hudsprites[i].color = Color.Lerp(hudsprites[i].color, instance.charcolor[instance.playerdata[i].trueid], framestep * 0.05f);
				}
			}
			if (hudsprites.Length > 3)
			{
				hudsprites[3].color = Color.Lerp(hudsprites[3].color, instance.menucolors[3], framestep * 0.05f);
			}
			if (instance.tpt > instance.tp)
			{
				instance.tpt--;
			}
			else if (instance.tpt < instance.tp)
			{
				instance.tpt++;
				if (instance.tpt != instance.tp && !sounds[5].isPlaying)
				{
					PlaySound("TP", 5, 1f + UnityEngine.Random.Range(-0.1f, 0.1f), 1f);
				}
			}
			if (instance.tpt != tmtp[0] || instance.maxtp != tmtp[1])
			{
				instance.hudfont[3].text = string.Concat(instance.tpt).PadLeft(2, '0') + "/" + string.Concat(instance.maxtp).PadLeft(2, '0');
				tmtp[0] = instance.tpt;
				tmtp[1] = instance.maxtp;
			}
			ShowHUD(true);
			if (instance.hudcooldown <= 0f)
			{
				instance.hudcooldown = -100f;
			}
		}
		else
		{
			ShowHUD(false);
		}
		if (instance.showmoney > 0f)
		{
			if (!instance.message)
			{
				instance.showmoney -= framestep;
			}
			if (instance.money != instance.moneyt && !sounds[4].isPlaying)
			{
				PlaySound("Money", 4, ((instance.money > instance.moneyt) ? 1f : 0.7f) + UnityEngine.Random.Range(-0.1f, 0.1f), 1f);
			}
		}
		if (instance.discoveryhud > 0f)
		{
			instance.discoveryhud -= framestep;
		}
		if (instance.moneyt > instance.money)
		{
			instance.moneyt--;
		}
		else if (instance.moneyt < instance.money)
		{
			instance.moneyt++;
		}
		if (tempmoneh != instance.moneyt)
		{
			instance.hudfont[4].text = string.Concat(instance.moneyt).PadLeft(3, '0');
			tempmoneh = instance.moneyt;
		}
	}

	public static void ShowHUD(bool show)
	{
		if (show && !hudvisible)
		{
			ForceHUD();
			hudvisible = true;
		}
		else if (!show)
		{
			hudvisible = false;
		}
		float num = 6.5f;
		float num2 = 4.25f;
		if (show)
		{
			num2 = 6.5f;
			num = 4.25f;
		}
		float num3 = 1f;
		for (int i = 0; i < instance.hud.Length; i++)
		{
			if (!(instance.hud[i] != null))
			{
				continue;
			}
			if (i < 3)
			{
				if (i < instance.playerdata.Length)
				{
					if (show)
					{
						hudvalue[i] = Mathf.Lerp(hudvalue[i], num, TieFramerate(0.15f));
					}
					else
					{
						hudvalue[i] = Mathf.Lerp(hudvalue[i], num + (float)i, TieFramerate(0.15f));
					}
				}
				else
				{
					hudvalue[i] = 10f;
				}
			}
			switch (i)
			{
			case 3:
				if (show)
				{
					hudvalue[i] = Mathf.Lerp(hudvalue[i], num, TieFramerate(0.15f));
				}
				else
				{
					hudvalue[i] = Mathf.Lerp(hudvalue[i], num + (float)i, TieFramerate(0.15f));
				}
				break;
			case 4:
				num3 = -1f;
				if (!show)
				{
					num2 = 6.5f;
					num = 4.25f;
				}
				if (instance.showmoney > 0f)
				{
					hudvalue[i] = Mathf.Lerp(hudvalue[i], num, TieFramerate(0.15f));
				}
				else
				{
					hudvalue[i] = Mathf.Lerp(hudvalue[i], num + (float)i, TieFramerate(0.15f));
				}
				break;
			}
			instance.hud[i].localPosition = Vector3.Lerp(instance.hud[i].localPosition, new Vector3(instance.hud[i].localPosition.x, num3 * (hudvalue[i] + (hudvalue[i] - num) * (hudvalue[i] - num2)), instance.hud[i].localPosition.z), TieFramerate(0.2f));
		}
	}

	public static Vector3 MiddlePoint(Vector3[] inputs)
	{
		Vector3 zero = Vector3.zero;
		for (int i = 0; i < inputs.Length; i++)
		{
			zero += inputs[i];
		}
		return zero / inputs.Length;
	}

	public static int CheckIfMore(int value, int[] values)
	{
		int num = 0;
		for (int i = 0; i < values.Length; i++)
		{
			if (value >= values[i])
			{
				num++;
			}
		}
		return num;
	}

	public static Vector3[] GetEntitiesPos(EntityControl[] e)
	{
		List<Vector3> list = new List<Vector3>();
		for (int i = 0; i < e.Length; i++)
		{
			list.Add(e[i].transform.position);
		}
		return list.ToArray();
	}

	public static void RefreshSkills()
	{
		bool flag = HasFollower(AnimIDs.AntQueen) || CurrentMap() == Maps.TestRoom || instance.flags[594] || BadgeIsEquipped(80);
		for (int i = 0; i < instance.playerdata.Length; i++)
		{
			instance.playerdata[i].skills = new List<int>();
			switch (instance.playerdata[i].trueid)
			{
			case 0:
				if (battle == null)
				{
					if (instance.flags[11])
					{
						instance.playerdata[i].skills.Add(34);
					}
					if (instance.flags[21])
					{
						instance.playerdata[i].skills.Add(35);
					}
					if (instance.flags[19])
					{
						instance.playerdata[i].skills.Add(36);
					}
				}
				instance.playerdata[i].skills.Add(2);
				if (instance.flags[21])
				{
					instance.playerdata[i].skills.Add(18);
				}
				if (BadgeIsEquipped(60))
				{
					instance.playerdata[i].skills.Add(44);
				}
				if (instance.partylevel >= 11)
				{
					instance.playerdata[i].skills.Add(16);
				}
				if (instance.partylevel >= 15)
				{
					instance.playerdata[i].skills.Add(24);
				}
				if (instance.flags[19])
				{
					instance.playerdata[i].skills.Add(5);
				}
				if (instance.flags[544])
				{
					instance.playerdata[i].skills.Add(26);
				}
				if (instance.flags[533])
				{
					instance.playerdata[i].skills.Add(31);
				}
				if (instance.partylevel >= 2)
				{
					instance.playerdata[i].skills.Add(11);
				}
				if (instance.flags[445])
				{
					instance.playerdata[i].skills.Add(45);
				}
				if (BadgeIsEquipped(51, instance.playerdata[i].trueid))
				{
					instance.playerdata[i].skills.Add(33);
				}
				if (BadgeIsEquipped(73, instance.playerdata[i].trueid))
				{
					instance.playerdata[i].skills.Add(48);
				}
				if (flag)
				{
					instance.playerdata[i].skills.Add(46);
				}
				break;
			case 1:
				if (battle == null)
				{
					if (instance.flags[17])
					{
						instance.playerdata[i].skills.Add(37);
					}
					if (instance.flags[699] && !instance.flags[39])
					{
						instance.playerdata[i].skills.Add(49);
					}
					if (instance.flags[39])
					{
						instance.playerdata[i].skills.Add(38);
					}
					if (instance.flags[18])
					{
						instance.playerdata[i].skills.Add(39);
					}
				}
				instance.playerdata[i].skills.Add(3);
				if (instance.partylevel >= 4)
				{
					instance.playerdata[i].skills.Add(32);
				}
				if (instance.flags[18])
				{
					instance.playerdata[i].skills.Add(6);
				}
				if (instance.flags[699])
				{
					instance.playerdata[i].skills.Add(10);
				}
				if (instance.flags[19])
				{
					instance.playerdata[i].skills.Add(5);
				}
				if (instance.partylevel >= 17)
				{
					instance.playerdata[i].skills.Add(27);
				}
				if (instance.flags[533])
				{
					instance.playerdata[i].skills.Add(31);
				}
				if (BadgeIsEquipped(13))
				{
					instance.playerdata[i].skills.Add(9);
				}
				if (BadgeIsEquipped(29))
				{
					instance.playerdata[i].skills.Add(19);
				}
				if (instance.flags[98])
				{
					instance.playerdata[i].skills.Add(43);
				}
				if (BadgeIsEquipped(51, instance.playerdata[i].trueid))
				{
					instance.playerdata[i].skills.Add(33);
				}
				if (BadgeIsEquipped(73, instance.playerdata[i].trueid))
				{
					instance.playerdata[i].skills.Add(48);
				}
				if (flag)
				{
					instance.playerdata[i].skills.Add(46);
				}
				break;
			case 2:
				if (battle == null)
				{
					instance.playerdata[i].skills.Add(40);
					if (instance.flags[20])
					{
						instance.playerdata[i].skills.Add(42);
					}
					if (instance.flags[171])
					{
						instance.playerdata[i].skills.Add(41);
					}
				}
				instance.playerdata[i].skills.Add(4);
				if (instance.partylevel >= 6)
				{
					instance.playerdata[i].skills.Add(21);
				}
				if (instance.flags[171])
				{
					instance.playerdata[i].skills.Add(25);
				}
				if (instance.partylevel >= 17)
				{
					instance.playerdata[i].skills.Add(27);
				}
				if (instance.flags[544])
				{
					instance.playerdata[i].skills.Add(26);
				}
				if (instance.flags[533])
				{
					instance.playerdata[i].skills.Add(31);
				}
				if (instance.flags[160])
				{
					instance.playerdata[i].skills.Add(17);
				}
				if (instance.flags[20])
				{
					instance.playerdata[i].skills.Add(7);
				}
				if (BadgeIsEquipped(52))
				{
					instance.playerdata[i].skills.Add(8);
				}
				if (BadgeIsEquipped(53))
				{
					instance.playerdata[i].skills.Add(22);
				}
				if (BadgeIsEquipped(10))
				{
					instance.playerdata[i].skills.Add(14);
				}
				if (BadgeIsEquipped(31))
				{
					instance.playerdata[i].skills.Add(23);
				}
				if (BadgeIsEquipped(39))
				{
					instance.playerdata[i].skills.Add(15);
				}
				if (BadgeIsEquipped(40))
				{
					instance.playerdata[i].skills.Add(30);
				}
				if (BadgeIsEquipped(37))
				{
					instance.playerdata[i].skills.Add(28);
				}
				if (BadgeIsEquipped(38))
				{
					instance.playerdata[i].skills.Add(29);
				}
				if (BadgeIsEquipped(15))
				{
					instance.playerdata[i].skills.Add(12);
				}
				if (BadgeIsEquipped(16))
				{
					instance.playerdata[i].skills.Add(13);
				}
				if (instance.partylevel >= 13)
				{
					instance.playerdata[i].skills.Add(47);
				}
				if (BadgeIsEquipped(51, instance.playerdata[i].trueid))
				{
					instance.playerdata[i].skills.Add(33);
				}
				if (BadgeIsEquipped(73, instance.playerdata[i].trueid))
				{
					instance.playerdata[i].skills.Add(48);
				}
				if (flag)
				{
					instance.playerdata[i].skills.Add(46);
				}
				break;
			}
		}
	}

	public static int[] PartyArray()
	{
		int[] array = new int[1] { instance.playerdata.Length };
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = instance.playerdata[i].trueid;
		}
		return array;
	}

	public static void InventoryFromString(string inp, int itemtype, bool addd)
	{
		if (inp == null || inp.Length <= 0)
		{
			return;
		}
		if (!addd)
		{
			instance.items[itemtype] = new List<int>();
		}
		string[] array = inp.Split(',');
		if (array.Length != 0)
		{
			for (int i = 0; i < array.Length; i++)
			{
				instance.items[itemtype].Add(Convert.ToInt32(array[i]));
			}
		}
	}

	public static IEnumerator DelayedObj(float delay, string path, Vector3 position, string sound, float destroy)
	{
		yield return new WaitForSeconds(delay);
		if (sound != null)
		{
			PlaySound(sound);
		}
		GameObject obj = UnityEngine.Object.Instantiate(Resources.Load(path), position, Quaternion.identity) as GameObject;
		if (destroy > 0f)
		{
			UnityEngine.Object.Destroy(obj, destroy);
		}
	}

	public static bool HasFollower(AnimIDs followerid)
	{
		if (map != null && map.tempfollowers != null && map.tempfollowers.Count > 0)
		{
			EntityControl[] array = map.tempfollowers.ToArray();
			for (int i = 0; i < map.tempfollowers.Count; i++)
			{
				if (array[i].originalid == (int)(followerid - 1))
				{
					return true;
				}
			}
		}
		return false;
	}

	public static bool ForceMoving(EntityControl[] e)
	{
		for (int i = 0; i < e.Length; i++)
		{
			if (e[i].forcemove)
			{
				return true;
			}
		}
		return false;
	}

	public static void PlayTransition(int id, int data, float speed, Color color)
	{
		if (color.r > 0.95f && color.g > 0.95f && color.b > 0.95f)
		{
			color = new Color(0.95f, 0.95f, 0.95f, color.a);
		}
		if (transition != null)
		{
			instance.StopCoroutine(transition);
		}
		if (instance.transitionobj != null && instance.transitionobj.Length != 0 && (id == 2 || id == 0 || id == 4 || id == 5))
		{
			for (int i = 0; i < instance.transitionobj.Length; i++)
			{
				if (instance.transitionobj[i] != null)
				{
					UnityEngine.Object.Destroy(instance.transitionobj[i].gameObject);
				}
			}
		}
		transition = instance.StartCoroutine(Transition(id, data, speed, color));
	}

	public static void DestroyAllChildren(Transform parent)
	{
		for (int num = parent.childCount - 1; num >= 0; num--)
		{
			UnityEngine.Object.Destroy(parent.GetChild(num).gameObject);
		}
	}

	public static void DestroyText(Transform parent)
	{
		for (int i = 0; i < parent.childCount; i++)
		{
			if (parent.GetChild(i).CompareTag("Text"))
			{
				DisableLetter(parent.GetChild(i));
			}
		}
	}

	public static void DestroyText(Transform parent, bool destroy)
	{
		for (int i = 0; i < parent.childCount; i++)
		{
			if (parent.GetChild(i).CompareTag("Text"))
			{
				DisableLetter(parent.GetChild(i), destroy);
			}
		}
	}

	private static void DisableLetter(Transform input)
	{
		DisableLetter(input, true);
	}

	private static void DisableLetter(Transform input, bool destroy)
	{
		for (int i = 0; i < input.childCount; i++)
		{
			if (input.GetChild(i).GetComponent<ButtonSprite>() != null)
			{
				UnityEngine.Object.Destroy(input.GetChild(i).gameObject);
			}
			else if (input.GetChild(i).GetComponent<TextMesh>() != null)
			{
				DisableLetter(input.GetChild(i).GetComponent<TextMesh>());
			}
		}
		if (destroy)
		{
			UnityEngine.Object.Destroy(input.gameObject);
		}
	}

	private static void DisableLetter(TextMesh input)
	{
		if (input.CompareTag("Letter"))
		{
			input.text = "";
			if (input.GetComponent<FontEffects>() != null)
			{
				UnityEngine.Object.Destroy(input.GetComponent<FontEffects>());
			}
			input.transform.parent = instance.transform;
			input.tag = "Untagged";
		}
		else
		{
			UnityEngine.Object.Destroy(input.gameObject);
		}
	}

	public static SpriteRenderer NewSolidColor(string name, Color color)
	{
		return NewSolidColor(name, color, 100f, Vector3.zero, new Vector2(0.5f, 0.5f));
	}

	public static SpriteRenderer NewSolidColor(string name, Color color, float pixelsperunit, Vector3 position, Vector2 pivot)
	{
		SpriteRenderer spriteRenderer = new GameObject(name).AddComponent<SpriteRenderer>();
		Texture2D texture2D = new Texture2D(1, 1);
		texture2D.SetPixel(0, 0, color);
		texture2D.Apply();
		spriteRenderer.sprite = Sprite.Create(texture2D, new Rect(0f, 0f, 1f, 1f), pivot, pixelsperunit);
		spriteRenderer.transform.position = position;
		return spriteRenderer;
	}

	public static void FadeOut()
	{
		PlayTransition(1, 0, 0.1f, Color.black);
	}

	public static void FadeOut(float speed)
	{
		PlayTransition(1, 0, speed, Color.black);
	}

	public static void FadeIn()
	{
		PlayTransition(0, 0, 0.1f, Color.black);
	}

	public static void FadeIn(float speed)
	{
		PlayTransition(0, 0, speed, Color.black);
	}

	public static void SetCameraInstant(Vector3 pos)
	{
		SetCamera(pos, 1f);
	}

	public static void SetCamera(Vector3 pos)
	{
		SetCamera(pos, 0.035f);
	}

	public static void FadeIn(float speed, Color color)
	{
		PlayTransition(0, 0, speed, color);
	}

	private static IEnumerator Transition(int id, int data, float speed, Color color)
	{
		instance.intransition = true;
		int? overridesort = null;
		Vector3[] hp = new Vector3[10]
		{
			new Vector3(-6.3f, 3.7f, 10f),
			new Vector3(0f, 3.7f, 10f),
			new Vector3(6.3f, 3.7f, 10f),
			new Vector3(-9.4f, -1.8f, 10f),
			new Vector3(-3.125f, -1.8f, 10f),
			new Vector3(3.125f, -1.8f, 10f),
			new Vector3(9.4f, -1.8f, 10f),
			new Vector3(-6.3f, -7.2f, 10f),
			new Vector3(0f, -7.2f, 10f),
			new Vector3(6.3f, -7.2f, 10f)
		};
		switch (id)
		{
		case 0:
		{
			instance.transitionobj = new Transform[1] { new GameObject("Dimmer").transform };
			SpriteRenderer r = instance.transitionobj[0].gameObject.AddComponent<SpriteRenderer>();
			Texture2D texture2D = new Texture2D(1, 1);
			texture2D.SetPixel(0, 0, color);
			texture2D.Apply();
			r.sprite = Sprite.Create(texture2D, new Rect(0f, 0f, 1f, 1f), new Vector2(0.5f, 0.5f));
			r.transform.localScale = new Vector3(3000f, 3000f, 1f);
			r.gameObject.layer = 5;
			if (!overridesort.HasValue)
			{
				r.sortingOrder = 9999;
			}
			else
			{
				r.sortingOrder = overridesort.Value;
			}
			r.transform.parent = GUICamera.transform;
			r.transform.localPosition = new Vector3(0f, 0f, 10f);
			r.transform.localEulerAngles = Vector3.zero;
			r.color = Color.clear;
			float failsafe = 600f;
			while (r.color.a < 1f && failsafe >= 0f)
			{
				r.color = Color.Lerp(r.color, color, framestep * speed);
				failsafe -= framestep;
				yield return null;
			}
			break;
		}
		case 1:
			if (instance.transitionobj != null && instance.transitionobj.Length != 0)
			{
				SpriteRenderer r = instance.transitionobj[0].GetComponent<SpriteRenderer>();
				float failsafe = 600f;
				while (r.color.a > 0f && failsafe >= 0f)
				{
					r.color = Color.Lerp(r.color, Color.clear, framestep * speed);
					failsafe -= framestep;
					yield return null;
				}
				UnityEngine.Object.Destroy(instance.transitionobj[0].gameObject);
			}
			break;
		case 2:
		{
			bool hexagon = data == 4;
			instance.transitionobj = new Transform[hexagon ? hp.Length : leafpos.Length];
			for (int k = 0; k < instance.transitionobj.Length; k++)
			{
				SpriteRenderer spriteRenderer = new GameObject("Leaf" + k).AddComponent<SpriteRenderer>();
				spriteRenderer.sprite = leafsprites[data];
				spriteRenderer.color = color;
				spriteRenderer.gameObject.layer = 5;
				spriteRenderer.sortingOrder = 200 + k;
				spriteRenderer.transform.parent = GUICamera.transform;
				if (!hexagon)
				{
					spriteRenderer.transform.localEulerAngles = new Vector3(0f, 0f, UnityEngine.Random.Range(0, 360));
					spriteRenderer.transform.localPosition = new Vector3(-20f, UnityEngine.Random.Range(-1, 1), 10f);
				}
				else
				{
					spriteRenderer.transform.localScale = Vector3.one * 0.75f;
					spriteRenderer.transform.localEulerAngles = Vector3.zero;
					spriteRenderer.transform.localPosition = hp[k] + new Vector3(0f, 10 + 10 * k);
				}
				instance.transitionobj[k] = spriteRenderer.transform;
			}
			while (instance.transitionobj[0].localPosition != new Vector3(leafpos[0].x, leafpos[0].y, 10f))
			{
				for (int l = 0; l < instance.transitionobj.Length; l++)
				{
					if (!hexagon)
					{
						instance.transitionobj[l].localPosition = Vector3.Lerp(instance.transitionobj[l].localPosition, new Vector3(leafpos[l].x, leafpos[l].y, 10f), TieFramerate(speed));
						instance.transitionobj[l].localEulerAngles = new Vector3(0f, 0f, instance.transitionobj[l].localEulerAngles.z + GetSqrDistance(instance.transitionobj[l].localPosition, new Vector3(leafpos[l].x, leafpos[l].y, 10f)) / 20f);
					}
					else
					{
						instance.transitionobj[l].localPosition = Vector3.Lerp(instance.transitionobj[l].localPosition, hp[l], TieFramerate(speed));
					}
				}
				yield return null;
			}
			break;
		}
		case 3:
		{
			bool hexagon = data == 4;
			if (instance.transitionobj == null || instance.transitionobj.Length == 0)
			{
				break;
			}
			while (GetDistance(instance.transitionobj[0].localPosition, new Vector3(20f, 10f)) > 0.1f)
			{
				for (int i = 0; i < instance.transitionobj.Length; i++)
				{
					if (hexagon)
					{
						instance.transitionobj[i].localPosition = Vector3.Lerp(instance.transitionobj[i].localPosition, hp[i] + new Vector3(0f, -20 - 10 * i), TieFramerate(speed));
						continue;
					}
					instance.transitionobj[i].localPosition = Vector3.Lerp(instance.transitionobj[i].localPosition, new Vector3(20f, 10f), TieFramerate(speed));
					instance.transitionobj[i].localEulerAngles = new Vector3(0f, 0f, instance.transitionobj[i].localEulerAngles.z + GetSqrDistance(instance.transitionobj[i].localPosition, new Vector3(20f, 10f)) / 50f);
				}
				yield return null;
			}
			for (int j = 0; j < instance.transitionobj.Length; j++)
			{
				if (instance.transitionobj[j] != null)
				{
					UnityEngine.Object.Destroy(instance.transitionobj[j].gameObject);
				}
			}
			break;
		}
		case 4:
		case 5:
		{
			SpriteRenderer t = (UnityEngine.Object.Instantiate(Resources.Load("Prefabs/Objects/RoundTransition"), GUICamera.transform) as GameObject).GetComponent<SpriteRenderer>();
			if (id == 5)
			{
				t.material.color = color;
				speed /= 2f;
			}
			t.sortingOrder = data;
			overridesort = data;
			t.transform.localPosition = new Vector3(0f, 0f, 10f);
			t.transform.localEulerAngles = Vector3.zero;
			instance.transitionobj = new Transform[1] { t.transform };
			while ((id == 4 && t.material.color.a < 0.985f) || (id == 5 && t.material.color.a > 0.05f))
			{
				if (id == 4)
				{
					t.material.color = Color.Lerp(t.material.color, color, TieFramerate(speed));
				}
				else
				{
					t.material.color = Color.Lerp(t.material.color, Color.clear, TieFramerate(speed));
				}
				t.material.SetFloat("_Cutoff", 1f - t.material.color.a);
				yield return null;
			}
			UnityEngine.Object.Destroy(t.gameObject, 0.2f);
			speed = 1f;
			if (id != 4)
			{
				break;
			}
			goto case 0;
		}
		}
		instance.intransition = false;
		yield return null;
		transition = null;
	}

	public static void ForceAnim(EntityControl entity)
	{
		if (entity != null && entity.anim != null)
		{
			string text = "";
			string text2 = entity.animstate.ToString();
			if (entity.animstate < 100)
			{
				Animations animstate = (Animations)entity.animstate;
				text2 = animstate.ToString();
			}
			if (entity.height > 0.1f)
			{
				text += "f";
			}
			entity.anim.Play(text2 + text);
		}
	}

	public static void RefreshEntities(bool onlyplayer)
	{
		RefreshEntities(false, false, onlyplayer);
	}

	public static void RefreshEntities()
	{
		RefreshEntities(false, false, false);
	}

	public static void RefreshEntities(bool forceanim, bool refreshmap)
	{
		RefreshEntities(forceanim, refreshmap, false);
	}

	public static void SetMonitor()
	{
	}

	public static void RefreshEntities(bool forceanim, bool refreshmap, bool onlyplayer)
	{
		for (int i = 0; i < instance.playerdata.Length; i++)
		{
			if (instance.playerdata[i].entity != null)
			{
				instance.playerdata[i].entity.hitwall = false;
				instance.playerdata[i].entity.animid = instance.playerdata[i].animid;
				if (forceanim)
				{
					ForceAnim(instance.playerdata[i].entity);
				}
				instance.playerdata[i].entity.UpdateSpriteMat();
			}
		}
		if (onlyplayer)
		{
			return;
		}
		EntityControl[] array = UnityEngine.Object.FindObjectsOfType<EntityControl>();
		for (int j = 0; j < array.Length; j++)
		{
			array[j].oldid = -1;
			array[j].oldstate = -1;
			array[j].emoticoncooldown = 0f;
			array[j].hitwall = false;
			if (array[j].height > 0.1f)
			{
				array[j].oldfly = !array[j].flyinganim;
			}
			if ((array[j].item || instance.inevent) && array[j].item)
			{
				array[j].UpdateItem();
			}
			if (forceanim)
			{
				ForceAnim(array[j]);
			}
			if (!refreshmap || !(array[j].npcdata != null))
			{
				continue;
			}
			array[j].npcdata.disguisecooldown = -1;
			if (array[j].npcdata.disguiseobj != null)
			{
				array[j].npcdata.disguiseobj.gameObject.SetActive(false);
				array[j].sprite.enabled = true;
			}
			if (array[j].npcdata.entitytype != NPCControl.NPCType.Object)
			{
				continue;
			}
			if (((array[j].npcdata.objecttype == NPCControl.ObjectTypes.Dropplet && (array[j].npcdata.data[2] == 0 || !array[j].npcdata.hit)) || array[j].npcdata.objecttype == NPCControl.ObjectTypes.PushRock) && array[j].rigid != null)
			{
				array[j].rigid.useGravity = true;
				array[j].rigid.isKinematic = false;
				if (array[j].npcdata.objecttype == NPCControl.ObjectTypes.Dropplet)
				{
					array[j].onground = false;
					array[j].rigid.velocity = Vector3.zero;
					if (array[j].originalmap == map.transform)
					{
						array[j].transform.parent = map.transform;
					}
				}
			}
			else if (array[j].npcdata.objecttype == NPCControl.ObjectTypes.Geizer && array[j].npcdata.hit && array[j].npcdata.internaltransform != null && array[j].npcdata.internaltransform.Length != 0)
			{
				array[j].npcdata.internaltransform[3].GetComponentInChildren<ParticleSystem>().Play();
			}
		}
		if (refreshmap && map != null)
		{
			TrailRenderer[] array2 = UnityEngine.Object.FindObjectsOfType<TrailRenderer>();
			for (int k = 0; k < array2.Length; k++)
			{
				array2[k].Clear();
			}
		}
	}

	public static void SetEntityLastPos(bool setit)
	{
		EntityControl[] array = UnityEngine.Object.FindObjectsOfType<EntityControl>();
		for (int i = 0; i < array.Length; i++)
		{
			if (setit)
			{
				array[i].lastpos = array[i].transform.position;
				continue;
			}
			array[i].transform.position = array[i].lastpos;
			if (array[i].animid == -1)
			{
				array[i].rigid.useGravity = false;
				array[i].rigid.isKinematic = true;
			}
		}
	}

	public static float GetSqrDistance(Vector3 a, Vector3 b)
	{
		return (a - b).sqrMagnitude * 1f;
	}

	public static float GetSqrDistance(Vector3 a, Vector3 b, bool ignorey)
	{
		if (!ignorey)
		{
			return GetSqrDistance(a, b);
		}
		return GetSqrDistance(new Vector3(a.x, 0f, a.z), new Vector3(b.x, 0f, b.z));
	}

	public static float GetDistance(Vector3 a, Vector3 b)
	{
		return (a - b).magnitude * 1f;
	}

	public static float GetDistance(Vector2 a, Vector2 b)
	{
		return (a - b).magnitude * 1f;
	}

	public static float GetDistance(Vector3 a, Vector3 b, bool ignoreY)
	{
		if (!ignoreY)
		{
			return GetDistance(a, b);
		}
		return GetDistance(new Vector3(a.x, 0f, a.z), new Vector3(b.x, 0f, b.z));
	}

	public static float GetDistance(float a, float b)
	{
		if (b > a)
		{
			float num = a;
			a = b;
			b = num;
		}
		return Mathf.Abs(a - b);
	}

	public static float LoopValue(float value, float min, float max)
	{
		return LoopValue(value, min, min, false);
	}

	public static float LoopValue(float value, float min, float max, bool floor)
	{
		while (value < min)
		{
			value += max;
		}
		if (value > max)
		{
			value %= max;
		}
		if (floor)
		{
			value = Mathf.FloorToInt(value);
		}
		return value;
	}

	public static GameObject PlayParticle(string name, string sound, Vector3 position)
	{
		return PlayParticle(name, sound, position, new Vector3(-90f, 0f), 5f);
	}

	public static GameObject PlayParticle(string name, Vector3 position, float time)
	{
		return PlayParticle(name, null, position, new Vector3(-90f, 0f), time);
	}

	public static GameObject PlayParticle(string name, Vector3 position)
	{
		return PlayParticle(name, null, position, new Vector3(-90f, 0f), 5f);
	}

	public static GameObject PlayParticle(string name, string sound, Vector3 position, Vector3 angle, float alivetime)
	{
		return PlayParticle(name, sound, position, angle, alivetime, 3000);
	}

	public static GameObject PlayParticle(string name, string sound, Vector3 position, Vector3 angle, float alivetime, int rendersort)
	{
		if (sound != null)
		{
			PlaySound(sound);
		}
		GameObject gameObject = UnityEngine.Object.Instantiate(Resources.Load("Prefabs/Particles/" + name), position, Quaternion.Euler(angle)) as GameObject;
		Renderer component = gameObject.GetComponent<Renderer>();
		if (component != null)
		{
			component.material.renderQueue = rendersort;
		}
		if (alivetime > 0f)
		{
			UnityEngine.Object.Destroy(gameObject, alivetime);
		}
		return gameObject;
	}

	public static Color RainbowColor(int variant)
	{
		return RainbowColor(variant, 5.9f, 0f, 1f, 1f);
	}

	public static Color RainbowColor(int variant, float colorspeed)
	{
		return RainbowColor(variant, colorspeed, 0f, 1f, 1f);
	}

	public static Color RainbowColor(int variant, float colorspeed, float min, float limit, float alpha)
	{
		return new Color(Mathf.Clamp(Mathf.Sin((Time.time + (float)variant + 20f) * colorspeed) * 2f, min, limit), Mathf.Clamp(Mathf.Sin((Time.time + (float)variant) * colorspeed) * 2f, min, limit), Mathf.Clamp(Mathf.Sin((Time.time + (float)variant + 60f) * colorspeed) * 2f, min, limit), alpha);
	}

	public static float ColorMagnitude(Color color)
	{
		return new Vector3(color.r, color.g, color.b).magnitude;
	}

	public static float Snap([In] ref float value, [In] ref float step)
	{
		if (step != 0f)
		{
			return Mathf.Floor(value / step + 0.5f) * step;
		}
		return value;
	}

	public static Vector3 Snap([In] ref Vector3 value, [In] ref Vector3 step)
	{
		return new Vector3(Snap(ref value.x, ref step.x), Snap(ref value.y, ref step.y), Snap(ref value.z, ref step.z));
	}

	public static IEnumerator LevelUpMessage()
	{
		string[] ld = Resources.Load<TextAsset>("Data/LevelData").ToString().Split('\n');
		bool canmessage = true;
		for (int i = 0; i < ld.Length; i++)
		{
			string[] t = ld[i].Split(',');
			if (Convert.ToInt32(t[0]) != instance.partylevel)
			{
				continue;
			}
			yield return new WaitForSeconds(0.5f);
			int num = Convert.ToInt32(t[1]);
			int num2 = 141 + num;
			switch (num)
			{
			case 0:
			{
				int num5 = Convert.ToInt32(t[2]);
				if (HasPlayer(num5))
				{
					instance.flagstring[0] = menutext[num5 + 46];
					instance.flagstring[1] = skilldata[Convert.ToInt32(t[3]), 0];
				}
				else
				{
					canmessage = false;
				}
				break;
			}
			case 1:
			{
				int num4 = Convert.ToInt32(t[2]);
				instance.flagstring[0] = menutext[num4 + 46];
				instance.flagstring[1] = t[4] + " ";
				switch (Convert.ToInt32(t[3]))
				{
				case 0:
					instance.flagstring[1] += menutext[16];
					AddStatBonus(StatBonus.Attack, Convert.ToInt32(t[4]), Convert.ToInt32(t[2]));
					break;
				case 1:
					instance.flagstring[1] += menutext[17];
					AddStatBonus(StatBonus.Defense, Convert.ToInt32(t[4]), Convert.ToInt32(t[2]));
					break;
				case 2:
					instance.flagstring[1] += menutext[14];
					AddStatBonus(StatBonus.HP, Convert.ToInt32(t[4]), Convert.ToInt32(t[2]));
					break;
				}
				canmessage = HasPlayer(num4);
				break;
			}
			case 2:
			{
				int num3 = Convert.ToInt32(t[3]);
				if (Convert.ToInt32(t[2]) == 0)
				{
					instance.flagstring[0] = num3 + " " + menutext[15];
					instance.tp += num3;
					instance.maxtp += num3;
					if (num3 == 3)
					{
						AddStatBonus(StatBonus.TP, num3, -1);
						break;
					}
					for (int j = 0; j < num3; j++)
					{
						AddStatBonus(StatBonus.TP, 1, -1);
					}
				}
				else
				{
					instance.flagstring[0] = num3 + " " + menutext[19];
					instance.bp += num3;
					instance.maxbp += num3;
					AddStatBonus(StatBonus.MP, num3, -1);
				}
				break;
			}
			case 3:
				instance.flagstring[0] = t[2];
				instance.maxitems += Convert.ToInt32(t[2]);
				instance.flagstring[1] = instance.maxitems.ToString();
				break;
			}
			if (canmessage)
			{
				instance.StartCoroutine(SetText("|boxstyle,4||spd,0||halfline||center|" + menutext[num2], true, Vector3.zero, null, null));
				while (instance.message)
				{
					yield return null;
				}
				yield return null;
			}
		}
		ApplyStatBonus();
		yield return null;
		instance.flagvar[2] = 1;
	}

	public static void SetPlayers(Vector3[] newentitypos)
	{
		if (newentitypos != null)
		{
			for (int i = 0; i < instance.playerdata.Length; i++)
			{
				if (instance.playerdata[i].entity != null)
				{
					UnityEngine.Object.Destroy(instance.playerdata[i].entity.gameObject);
				}
			}
		}
		SetPlayers();
		if (newentitypos == null)
		{
			return;
		}
		for (int j = 0; j < instance.playerdata.Length; j++)
		{
			if (instance.playerdata[j].entity != null)
			{
				instance.playerdata[j].entity.transform.position = newentitypos[j];
			}
		}
	}

	public static void SetPlayers()
	{
		EntityControl entityControl = null;
		for (int i = 0; i < instance.playerdata.Length; i++)
		{
			instance.playerdata[i].entity = EntityControl.CreateNewEntity("Player " + i);
			instance.playerdata[i].entity.animid = instance.playerdata[i].animid;
			instance.playerdata[i].entity.alwaysactive = true;
			instance.playerdata[i].condition = new List<int[]>();
			if (entityControl == null)
			{
				instance.playerdata[i].entity.gameObject.AddComponent<PlayerControl>();
				entityControl = instance.playerdata[i].entity;
				instance.camtarget = instance.playerdata[i].entity.transform;
				instance.playerdata[i].entity.tag = "Player";
			}
			else if (entityControl != null)
			{
				EntityControl following = entityControl;
				instance.playerdata[i].entity.following = following;
				instance.playerdata[i].entity.followoffset = (float)i * 0.1f;
				instance.playerdata[i].entity.gameObject.layer = 9;
				instance.playerdata[i].entity.transform.position = new Vector3(instance.playerdata[i].entity.transform.position.x, instance.playerdata[i].entity.transform.position.y, instance.playerdata[i].entity.transform.position.z + instance.playerdata[i].entity.followoffset);
				instance.playerdata[i].entity.tag = "PFollower";
				instance.playerdata[i].entity.mainparty = true;
				entityControl = instance.playerdata[i].entity;
				instance.playerdata[i - 1].entity.followedby = instance.playerdata[i].entity.transform;
			}
		}
	}

	public static EntityControl[] GetPartyEntities()
	{
		List<EntityControl> list = new List<EntityControl>();
		for (int i = 0; i < instance.playerdata.Length; i++)
		{
			list.Add(instance.playerdata[i].entity);
		}
		return list.ToArray();
	}

	public static EntityControl[] GetPartyEntities(bool idorder)
	{
		if (!idorder)
		{
			return GetPartyEntities();
		}
		List<EntityControl> list = new List<EntityControl>();
		EntityControl[] array = new EntityControl[3]
		{
			GetEntity(-4),
			GetEntity(-5),
			GetEntity(-6)
		};
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i] != null)
			{
				list.Add(array[i]);
			}
		}
		return list.ToArray();
	}

	private static IEnumerator LowerVolume(int musicid, float volume, float frametime, bool percent)
	{
		float vol = music[musicid].volume;
		float target = (percent ? (vol * volume) : volume);
		float a = 0f;
		do
		{
			music[musicid].volume = Mathf.Lerp(vol, target, a / frametime);
			a += framestep;
			yield return null;
		}
		while (a < frametime + 1f);
	}

	public static IEnumerator ChapterName(int chapterid)
	{
		SpriteRenderer back = NewUIObject("back", GUICamera.transform, new Vector3(0f, 0.5f, 10f), Vector3.one * 1.2f, Resources.LoadAll<Sprite>("Sprites/GUI/textbox")[12], -1).GetComponent<SpriteRenderer>();
		back.color = Color.clear;
		float vol = music[0].volume;
		instance.StartCoroutine(LowerVolume(0, 0.01f, 100f, true));
		if (chapterid < 5)
		{
			PlayTransition(0, -5, 0.01f, new Color(1f, 1f, 1f, 0.5f));
			yield return null;
			SpriteRenderer dm = GetTransitionSprite();
			while (dm.color.a < 0.35f)
			{
				yield return null;
			}
		}
		AudioSource audioSource = PlaySound("ch" + (chapterid + 2));
		if (audioSource != null)
		{
			audioSource.volume = musicvolume;
		}
		Coroutine c = instance.StartCoroutine(GradualColor(back, (chapterid == 5) ? Color.black : Color.white, 200f, true));
		string text = menutext[173] + " " + (chapterid + 2);
		instance.StartCoroutine(SetText("|setbreak,9999,true||boxstyle,-1||size,1.3||spd,0||font,2||rainbow|" + text + "|rainbow||line||halfline||font,0||size,1||dropshadow,0.075,-0.075|" + ((chapterid < 5) ? "|color,4|" : "|color,0|") + "|fadeletter|              " + menutext[176 + chapterid] + "|fwait," + ((audioSource == null) ? 5f : (audioSource.clip.length * 0.95f)) + "|", true, new Vector3(-5f, -2.75f, 10f), null, null));
		yield return null;
		instance.blinker.transform.localPosition = new Vector3(0f, -6f, 0.5f);
		while (instance.message)
		{
			yield return null;
		}
		instance.StopCoroutine(c);
		instance.StartCoroutine(LowerVolume(0, vol, 30f, false));
		instance.StartCoroutine(GradualColor(back, Color.clear, 15f, true));
		UnityEngine.Object.Destroy(back.gameObject, 1f);
		if (chapterid < 5)
		{
			PlayTransition(1, 0, 0.025f, Color.black);
			yield return null;
			SpriteRenderer dm = GetTransitionSprite();
			while (dm.color.a > 0.05f)
			{
				yield return null;
			}
		}
		chaptername = null;
	}

	public static float TieFramerate(float value)
	{
		return value * Time.smoothDeltaTime * 60f;
	}

	public static IEnumerator ShakeObject(Transform obj, Vector3 shake, float frametime, bool returntostart)
	{
		Vector3 p = obj.position;
		float a = 0f;
		do
		{
			obj.position = p + RandomVector(shake);
			a += TieFramerate(1f);
			yield return null;
		}
		while (a < frametime + 1f);
		if (returntostart)
		{
			obj.position = p;
		}
	}

	public static void LoadMap(int id, bool recreateplayers)
	{
		if (recreateplayers)
		{
			for (int i = 0; i < instance.playerdata.Length; i++)
			{
				if (instance.playerdata[i].entity != null)
				{
					UnityEngine.Object.Destroy(instance.playerdata[i].entity.gameObject);
				}
			}
			player = null;
		}
		LoadMap(id);
	}

	public static void LoadMap()
	{
		LoadMap(Convert.ToInt32(map.name));
	}

	public static void LoadMap(int id)
	{
		instance.changecamspeed = false;
		instance.camanglespeed = 0.1f;
		instance.camanglechange = false;
		instance.flags[400] = false;
		if (map != null)
		{
			for (int i = 0; i < instance.playerdata.Length; i++)
			{
				if (instance.playerdata[i].entity != null)
				{
					instance.playerdata[i].entity.transform.parent = null;
					if (i == instance.playerdata.Length - 1)
					{
						instance.playerdata[i].entity.followedby = null;
					}
				}
			}
			EntityControl[] array = map.tempfollowers.ToArray();
			if (array != null && array.Length != 0)
			{
				for (int j = 0; j < array.Length; j++)
				{
					if (array[j] != null)
					{
						UnityEngine.Object.Destroy(array[j].gameObject);
					}
				}
			}
			UnityEngine.Object.Destroy(map.gameObject);
			if (!instance.inevent)
			{
				GC.Collect();
				Resources.UnloadUnusedAssets();
			}
			GameObject[] array2 = GameObject.FindGameObjectsWithTag("MapObj");
			for (int k = 0; k < array2.Length; k++)
			{
				UnityEngine.Object.Destroy(array2[k].gameObject);
			}
		}
		GameObject obj = UnityEngine.Object.Instantiate(Resources.Load("Prefabs/Maps/" + (Maps)id)) as GameObject;
		map = obj.GetComponent<MapControl>();
		obj.name = id.ToString();
		if (player == null)
		{
			SetPlayers();
		}
		instance.ForceLoadSprites();
		if (player != null)
		{
			player.entity.sound.loop = false;
			player.entity.sound.Stop();
			player.pausecooldown = 120f;
		}
	}

	public static void FixSamira()
	{
		if (instance.samiramusics == null || instance.samiramusics.Count <= 0)
		{
			return;
		}
		int[][] array = instance.samiramusics.ToArray();
		for (int num = array.Length - 1; num >= 0; num--)
		{
			switch ((Musics)array[num][0])
			{
			case Musics.Title:
			case Musics.Wind:
			case Musics.Water:
			case Musics.MachineHum:
			case Musics.Breathing:
				instance.samiramusics.RemoveAt(num);
				break;
			}
		}
	}

	public static Transform Create9Box(Vector3 position, Vector2 size, int type, int sortorder, Color color, bool grow)
	{
		Transform transform = new GameObject("9Box").transform;
		SpriteRenderer spriteRenderer = transform.gameObject.AddComponent<SpriteRenderer>();
		spriteRenderer.sprite = Resources.Load<Sprite>("Sprites/GUI/9Box/box" + type);
		spriteRenderer.gameObject.layer = 5;
		spriteRenderer.color = color;
		spriteRenderer.sortingOrder = sortorder;
		spriteRenderer.size = size;
		spriteRenderer.drawMode = SpriteDrawMode.Tiled;
		spriteRenderer.tileMode = SpriteTileMode.Adaptive;
		if (grow)
		{
			transform.transform.localScale = Vector3.zero;
			transform.gameObject.AddComponent<DialogueAnim>();
		}
		else
		{
			transform.transform.localScale = Vector3.one;
		}
		transform.transform.parent = GUICamera.transform;
		transform.transform.localEulerAngles = Vector3.zero;
		transform.transform.localPosition = position;
		return transform;
	}

	public static IEnumerator LateAngle(Transform obj, Vector3 angle, bool local, WaitForSeconds time)
	{
		yield return time;
		if (local)
		{
			obj.localEulerAngles = angle;
		}
		else
		{
			obj.eulerAngles = angle;
		}
	}

	public static bool CheckAllBool(bool[] array, int[] values, bool state)
	{
		for (int i = 0; i < values.Length; i++)
		{
			if (array[values[i]] != state)
			{
				return false;
			}
		}
		return true;
	}

	private static float GetLetterOffset(TextMesh letter, float size)
	{
		if (letter == null)
		{
			return 0.3f * size;
		}
		return (letter.GetComponent<MeshRenderer>().bounds.extents.x * 2f + -2f) * size;
	}

	public static int FontID(int id)
	{
		if (id < 0)
		{
			return Mathf.Abs(id + 1);
		}
		if (id == 2)
		{
			return 1;
		}
		return id;
	}

	public static GameObject WaterSplash(Vector3 pos, Vector3 size)
	{
		GameObject obj = UnityEngine.Object.Instantiate(Resources.Load("Prefabs/Objects/WaterSplash"), pos, Quaternion.identity) as GameObject;
		obj.transform.localScale = size;
		UnityEngine.Object.Destroy(obj, 0.75f);
		return obj;
	}

	public static void SystemText(string text, Transform parent, Vector3 pos)
	{
		instance.StartCoroutine(SetText(text, false, pos, parent, null));
	}

	public static float GetLetterOffset(char letter, int fontid, float size)
	{
		if (letter == ' ')
		{
			return 0.3f * size;
		}
		fontid = FontID(fontid);
		CharacterInfo info = default(CharacterInfo);
		fonts[fontid].RequestCharactersInTexture(letter.ToString() ?? "");
		if (fonts[fontid].GetCharacterInfo(letter, out info))
		{
			return ((float)info.advance * 0.75f + -2f) * (size / (float)fonts[fontid].fontSize);
		}
		return 0.3f * size;
	}

	public static Vector3 VectorFromString(string[] inputs)
	{
		return new Vector3(Convert.ToSingle(inputs[0]), Convert.ToSingle(inputs[1]), Convert.ToSingle(inputs[2]));
	}

	public static IEnumerator LightingBolt(Vector3 a, Vector3 b, int segments, float variant, Color color, float frametime, float lineduration)
	{
		GameObject t = UnityEngine.Object.Instantiate(Resources.Load("Prefabs/Particles/LightingBolt"), a, Quaternion.identity) as GameObject;
		TrailRenderer component = t.GetComponent<TrailRenderer>();
		component.material.color = color;
		Gradient gradient = new Gradient();
		gradient.colorKeys = new GradientColorKey[2]
		{
			new GradientColorKey(color, 0f),
			new GradientColorKey(color, 1f)
		};
		component.colorGradient = gradient;
		component.time = lineduration;
		int amt = Mathf.Clamp(segments, 2, segments);
		for (int i = 0; i < amt; i++)
		{
			Vector3 tp = t.transform.position;
			Vector3 pp = Vector3.Lerp(a, b, (float)i / (float)(amt - 1));
			if (i > 0 && i < amt - 1)
			{
				pp += RandomVector(variant);
			}
			float c = frametime / (float)amt;
			float aa = 0f;
			do
			{
				t.transform.position = Vector3.Lerp(tp, pp, c);
				aa += TieFramerate(1f);
				yield return null;
			}
			while (aa < c + 1f);
		}
		UnityEngine.Object.Destroy(t, 1f);
		yield return null;
	}

	private static bool ContainsLine(string command)
	{
		if (command.Contains("line"))
		{
			switch ((Commands)Enum.Parse(typeof(Commands), command, true))
			{
			case Commands.Shopline:
				if (!instance.inlist)
				{
					if (player != null && player.npc != null && player.npc.Count > 0)
					{
						return player.npc[0].interacttype == NPCControl.Interaction.Shop;
					}
					return false;
				}
				return true;
			case Commands.Unpauseline:
				return pausemenu == null;
			default:
				return true;
			}
		}
		return false;
	}

	private static string ReplaceFunctions(string text)
	{
		string text2 = "";
		if (text != null)
		{
			for (int i = 0; i < text.Length; i++)
			{
				if (text[i] == '|')
				{
					string text3 = "|";
					for (int j = i + 1; j < text.Length && text[j] != '|'; j++)
					{
						text3 += text[j];
					}
					if (text3.Contains("sstring"))
					{
						text2 += instance.flagstring[Convert.ToInt32(text3.Replace("|sstring,", ""))];
					}
					else if (text3.Contains("|menu"))
					{
						string[] array = text3.Replace("|", "").Split(',');
						if (array.Length == 2)
						{
							text2 += menutext[Convert.ToInt32(array[1])];
						}
						else
						{
							array[1] = menutext[Convert.ToInt32(array[1])];
							switch (array[2])
							{
							case "1":
								text2 = text2 + array[1][0] + "-" + array[1];
								break;
							case "2":
								text2 += array[1].ToUpper();
								break;
							case "3":
								text2 = text2 + array[1][0] + "-" + array[1].ToUpper();
								break;
							}
						}
					}
					else
					{
						text2 = text2 + text3 + "|";
					}
					i += text3.Length;
				}
				else
				{
					text2 += text[i];
				}
			}
		}
		return text2;
	}

	private static string OrganizeLines(string text, float maxoffset, float size, int fontid)
	{
		if (languageid == 3)
		{
			return text;
		}
		string text2 = "";
		string text3 = "";
		bool flag = false;
		float num = 0f;
		float num2 = 0f;
		text = ReplaceFunctions(text);
		for (int i = 0; i < text.Length; i++)
		{
			if (text[i] == '\n')
			{
				continue;
			}
			if (text[i] == '|')
			{
				string text4 = "";
				for (int j = i + 1; j < text.Length && text[j] != '|'; j++)
				{
					text4 += text[j];
				}
				text3 = text3 + "|" + text4 + "|";
				bool flag2 = false;
				string[] array = text4.Split(',');
				if (array[0].Contains("next") || array[0].Contains("blank") || ContainsLine(array[0]))
				{
					if (num + num2 > maxoffset)
					{
						text2 += "\n";
						num = 0f;
					}
					text2 += text3;
					num = 0f;
					text3 = "";
					num2 = 0f;
				}
				else if (array[0].Contains("button"))
				{
					num += 0.7f;
					string text5 = InputIO.KeyboardString(Convert.ToInt32(text4.Replace("button,", "")));
					if (text5.Length > 1 && !text5.Contains("Arrow"))
					{
						num += 0.7f;
					}
					num2 = 0f;
				}
				else if (array[0].Contains("size") && array[0] != "battlesize")
				{
					if (array[1] != "multi")
					{
						size = Convert.ToSingle(array[1]);
					}
				}
				else if (array[0].Contains("singlebreak"))
				{
					flag = true;
					maxoffset = Convert.ToSingle(array[1]);
					if (languageid == 6)
					{
						maxoffset *= 1.15f;
					}
				}
				if (!flag2)
				{
					i += text4.Length + 1;
				}
			}
			else if (text[i] != ' ')
			{
				text3 += text[i];
				num2 += (float)Mathf.FloorToInt(GetLetterOffset(text[i], fontid, size) * 25f) / 25f * (flag ? 0.975f : 1f);
			}
			else
			{
				num += 0.3f * size;
				if (num + num2 > maxoffset)
				{
					text2 = ((!flag) ? (text2 + "\n") : (text2 + "|line|"));
					num = (((languageid > 0 && !AsianLang()) || (languageid == 0 && map != null && map.englishbreakfix && instance.message)) ? num2 : 0f);
				}
				else
				{
					num += num2;
				}
				text2 = text2 + text3 + " ";
				text3 = "";
				num2 = 0f;
			}
		}
		if (num2 + num > maxoffset)
		{
			text2 += "\n";
		}
		text2 += text3;
		if (flag)
		{
			text2 = text2.Replace("\n", "|line|").Replace("|line||line|", "|line|");
		}
		return text2;
	}

	public static float GetDistance01(float start, float end, float multiplier)
	{
		return Mathf.Clamp01(1f - GetDistance(start, end) * multiplier);
	}

	public static float GetPercentage(float start, float end, float currentvalue)
	{
		float value = end - start;
		float num = end - currentvalue;
		return 1f - num / Mathf.Clamp(value, 0.001f, float.PositiveInfinity);
	}

	public static void ScreamWaves(Vector3 pos)
	{
		instance.StartCoroutine(Waves(pos, 5, 20f, EventControl.quartersec, false, false, null));
	}

	public static IEnumerator Waves(Vector3 pos, int ammount, float frametime, WaitForSeconds delay, bool invert, bool tridimentional, Color? color)
	{
		GameObject sphere = Resources.Load<GameObject>("Prefabs/Objects/SphereGlowEffect");
		Vector3 max = new Vector3(50f, 50f, (!tridimentional) ? 1 : 50);
		for (int i = 0; i < ammount; i++)
		{
			Transform transform = UnityEngine.Object.Instantiate(sphere, pos, Quaternion.identity).transform;
			transform.localScale = (invert ? max : Vector3.zero);
			if (color.HasValue)
			{
				transform.GetComponent<Renderer>().material.color = color.Value;
			}
			instance.StartCoroutine(GradualScale(transform, invert ? Vector3.zero : max, frametime, true));
			if (delay != null)
			{
				yield return delay;
			}
		}
	}

	public static IEnumerator GradualScale(Transform obj, Vector3 target, float frametime, bool destroy)
	{
		float a = 0f;
		Vector3 ss = obj.localScale;
		do
		{
			obj.localScale = Vector3.Lerp(ss, target, a / frametime);
			a += TieFramerate(1f);
			yield return null;
		}
		while (a < frametime + 1f);
		if (destroy)
		{
			UnityEngine.Object.Destroy(obj.gameObject);
		}
	}

	public static IEnumerator SetText(string text, Vector3 position, Transform parent)
	{
		instance.StartCoroutine(SetText(text, 0, null, false, false, position, Vector3.zero, Vector2.one, parent, null));
		yield return null;
	}

	public static IEnumerator SetText(string text, Transform parent, NPCControl caller)
	{
		instance.StartCoroutine(SetText(text, 0, messagebreak, true, false, Vector3.zero, Vector3.zero, Vector2.one, parent, caller));
		yield return null;
	}

	public static IEnumerator SetText(string text, bool dialogue, Vector3 position, Transform parent, NPCControl caller)
	{
		float? linebreak = messagebreak;
		if (!dialogue)
		{
			linebreak = null;
		}
		instance.StartCoroutine(SetText(text, 0, linebreak, dialogue, false, position, Vector3.zero, Vector2.one, parent, caller));
		yield return null;
	}

	public static IEnumerator InnSleep(NPCControl caller, Vector3? position, bool changemusic, bool nofadeout)
	{
		instance.StartCoroutine(InnSleep(caller, position, changemusic, nofadeout, null, null));
		yield return null;
	}

	public static IEnumerator InnSleep(NPCControl caller, Vector3? position, bool changemusic, bool nofadeout, Vector3? camn, Vector3? camp)
	{
		instance.showmoney = -1f;
		string tm = ((music[0].clip != null) ? music[0].clip.name : null);
		PlaySound("Inn", 5);
		FadeMusic(0.1f);
		if (caller != null)
		{
			caller.entity.talking = false;
		}
		PlayTransition(4, 9999, 0.01f, Color.black);
		yield return new WaitForSeconds(0.1f);
		float failsafe = 700f;
		while (transition != null && failsafe > 0f)
		{
			failsafe -= framestep;
			yield return null;
		}
		if (position.HasValue)
		{
			float ts = instance.camspeed;
			instance.camspeed = 1f;
			Vector3 value = position.Value;
			player.transform.position = value;
			if (camn.HasValue)
			{
				map.camlimitneg = camn.Value;
			}
			if (camp.HasValue)
			{
				map.camlimitpos = camp.Value;
			}
			yield return new WaitForSeconds(0.1f);
			TeleportFollowers(true);
			instance.camspeed = ts;
		}
		failsafe = 60f;
		while (sounds[5].isPlaying || failsafe > 0f)
		{
			failsafe -= framestep;
			yield return null;
		}
		if (!nofadeout)
		{
			PlayTransition(1, 0, 0.15f, Color.clear);
			yield return new WaitForSeconds(0.5f);
		}
		Heal();
		instance.hudcooldown = 150f;
		if (changemusic)
		{
			ChangeMusic(tm);
		}
		player.npc = new List<NPCControl>();
		if (caller != null)
		{
			caller.entity.emoticoncooldown = 0f;
		}
		chaptername = null;
	}

	public static bool FrameDifference(int frames)
	{
		return Time.frameCount % Mathf.CeilToInt((float)((vsync == 0) ? Application.targetFrameRate : Screen.currentResolution.refreshRate) / 60f) == 0;
	}

	public static string GetDialogueText(int id)
	{
		if (id >= 0)
		{
			if (id > map.dialogues.Length - 1)
			{
				if (map.useglobalcommand)
				{
					map.currentline = -1;
				}
				return null;
			}
			if (map.useglobalcommand)
			{
				map.currentline = id;
			}
			return map.dialogues[id];
		}
		return commondialogue[Mathf.Abs(id) - 1];
	}

	public static SpriteRenderer NewSpriteObject(Vector3 position, Transform parent, Sprite sprite)
	{
		return NewSpriteObject("tempsprite", position, Vector3.zero, parent, sprite, spritemat);
	}

	public static SpriteRenderer NewSpriteObject(string name, Vector3 position, Vector3 rotation, Transform parent, Sprite sprite, Material mat)
	{
		SpriteRenderer spriteRenderer = new GameObject(name).AddComponent<SpriteRenderer>();
		spriteRenderer.transform.parent = parent;
		spriteRenderer.transform.localPosition = position;
		spriteRenderer.transform.localEulerAngles = rotation;
		spriteRenderer.sprite = sprite;
		spriteRenderer.gameObject.layer = 14;
		spriteRenderer.material = mat;
		return spriteRenderer;
	}

	public static Sprite GetItemSprite(bool badge, int id)
	{
		return itemsprites[Convert.ToInt32(badge), id];
	}

	public static int CrystalBerryAmmount()
	{
		int num = 0;
		for (int i = 0; i < instance.crystalbflags.Length; i++)
		{
			if (instance.crystalbflags[i])
			{
				num++;
			}
		}
		return num;
	}

	public static IEnumerator FadeSound(AudioSource sound, float speed)
	{
		while (sound.volume > 0f && sound.isPlaying)
		{
			sound.volume -= TieFramerate(speed);
			yield return null;
		}
		sound.Stop();
	}

	public static void TeleportFollowers()
	{
		TeleportFollowers(false);
	}

	public static void TeleportFollowers(bool all)
	{
		float num = 1f;
		for (int i = 1; i < instance.playerdata.Length; i++)
		{
			if (instance.playerdata[i].entity != null)
			{
				instance.playerdata[i].entity.transform.position = player.transform.position + instance.globalcamdir.forward.normalized * (num * 0.025f);
				instance.playerdata[i].entity.rigid.velocity = Vector3.zero;
			}
		}
		if (all)
		{
			EntityControl[] array = map.tempfollowers.ToArray();
			for (int j = 0; j < array.Length; j++)
			{
				if (array != null)
				{
					array[j].transform.position = player.transform.position;
				}
			}
		}
		if (map.chompy != null)
		{
			map.chompy.DelayedPosition(player.transform.position + instance.globalcamdir.forward.normalized * 0.3f);
		}
	}

	public static GameObject Create6Wheel(Sprite[] sprites, Vector3 spritescale, bool gui)
	{
		return Create6Wheel(new Color[6]
		{
			Color.white,
			Color.white,
			Color.white,
			Color.white,
			Color.white,
			Color.white
		}, sprites, spritescale, gui);
	}

	public static GameObject Create6Wheel(Sprite[] sprites, Vector3 spritescale)
	{
		return Create6Wheel(new Color[6]
		{
			Color.white,
			Color.white,
			Color.white,
			Color.white,
			Color.white,
			Color.white
		}, sprites, spritescale, false);
	}

	public static GameObject Create6Wheel(Color[] colors, Sprite[] sprites, Vector3 spritescale, bool gui)
	{
		GameObject gameObject = new GameObject("Wheel");
		SpriteRenderer spriteRenderer = NewSpriteObject(new Vector3(0f, 0f, -0.3f), gameObject.transform, guisprites[11]);
		spriteRenderer.transform.localScale = new Vector3(0.5f, 1f, 1f);
		if (gui)
		{
			spriteRenderer.gameObject.layer = 5;
		}
		spriteRenderer = NewSpriteObject(Vector3.zero, gameObject.transform, guisprites[121]);
		if (gui)
		{
			spriteRenderer.gameObject.layer = 5;
		}
		for (int i = 0; i < 6; i++)
		{
			spriteRenderer = NewSpriteObject(new Vector3(0f, 0f, -0.1f), gameObject.transform, guisprites[122]);
			spriteRenderer.material.color = colors[i];
			if (gui)
			{
				spriteRenderer.gameObject.layer = 5;
			}
			spriteRenderer = NewSpriteObject(new Vector3(-0.05f, 0.8f, -0.1f), spriteRenderer.transform, sprites[i]);
			spriteRenderer.transform.localScale = spritescale;
			spriteRenderer.transform.parent.localEulerAngles = new Vector3(0f, 0f, 60 * i);
			spriteRenderer.transform.parent.localScale = new Vector3(0.9f, 0.9f, 1f);
			if (gui)
			{
				spriteRenderer.gameObject.layer = 5;
			}
		}
		if (gui)
		{
			SetParenting(gameObject.transform, GUICamera.transform);
		}
		return gameObject;
	}

	public static void SetParenting(Transform t, Transform parent)
	{
		t.parent = parent;
		t.transform.localPosition = Vector3.zero;
		t.transform.localScale = Vector3.one;
		t.transform.localEulerAngles = Vector3.zero;
	}

	public static void TeleportFollowers(float distance, TPDir dir, Transform caller)
	{
		TeleportFollowers(distance, 1f, dir, caller.position);
	}

	public static void TeleportFollowers(float distance, TPDir dir, Vector3 caller)
	{
		TeleportFollowers(distance, 1f, dir, caller);
	}

	public static void TeleportFollowers(float distance, float offset, TPDir dir, Vector3 caller, bool alsoTPextras)
	{
		for (int i = 1; i < instance.playerdata.Length; i++)
		{
			if (instance.playerdata[i].entity != null && instance.playerdata[i].entity.following != null && GetDistance(instance.playerdata[i].entity.transform.position, instance.playerdata[i].entity.following.transform.position, false) > distance)
			{
				Vector3 vector = Vector3.zero;
				switch (dir)
				{
				case TPDir.Right:
					vector = instance.globalcamdir.right.normalized;
					break;
				case TPDir.Left:
					vector = -instance.globalcamdir.right.normalized;
					break;
				case TPDir.Up:
					vector = instance.globalcamdir.forward.normalized;
					break;
				case TPDir.Down:
					vector = -instance.globalcamdir.forward.normalized;
					break;
				case TPDir.Away:
					vector = -(caller - instance.playerdata[i].entity.following.transform.position).normalized + MainCamera.transform.forward.normalized * 0.1f;
					break;
				case TPDir.Center:
					vector = instance.globalcamdir.forward.normalized * 0.1f * (i + 1);
					break;
				}
				instance.playerdata[i].entity.transform.position = instance.playerdata[i].entity.following.transform.position + vector * offset;
				instance.playerdata[i].entity.rigid.velocity = Vector3.zero;
			}
		}
		if (map != null && alsoTPextras)
		{
			if (map.tempfollowers == null || map.tempfollowers.Count <= 0)
			{
				return;
			}
			EntityControl[] array = map.tempfollowers.ToArray();
			for (int j = 0; j < array.Length; j++)
			{
				if (array[j] != null && GetDistance(array[j].transform.position, caller) > distance)
				{
					Vector3 vector2 = Vector3.zero;
					switch (dir)
					{
					case TPDir.Right:
						vector2 = instance.globalcamdir.right.normalized;
						break;
					case TPDir.Left:
						vector2 = -instance.globalcamdir.right.normalized;
						break;
					case TPDir.Up:
						vector2 = instance.globalcamdir.forward.normalized;
						break;
					case TPDir.Down:
						vector2 = -instance.globalcamdir.forward.normalized;
						break;
					case TPDir.Away:
						vector2 = -caller.normalized + MainCamera.transform.forward.normalized * 0.1f;
						break;
					case TPDir.Center:
						vector2 = instance.globalcamdir.forward.normalized * 0.1f * (j + 1);
						break;
					}
					array[j].transform.position = caller + vector2 * offset;
					array[j].rigid.velocity = Vector3.zero;
				}
			}
		}
		else if (map != null && map.chompy != null && map.chompy.following != null && GetDistance(map.chompy.transform.position, map.chompy.following.transform.position) > distance)
		{
			map.chompy.transform.position = map.chompy.following.transform.position + instance.globalcamdir.forward.normalized * 0.1f;
		}
	}

	public static void TeleportFollowers(float distance, float offset, TPDir dir, Vector3 caller)
	{
		TeleportFollowers(distance, offset, dir, caller, false);
	}

	public static void Reset()
	{
		SceneManager.LoadScene(0);
	}

	private static void BreakLine(ref float x, ref float y, float max, Vector2 size)
	{
		if (x >= max)
		{
			x = 0f;
			y -= 0.7f * size.y;
		}
	}

	private static EntityControl GetLastFollowing()
	{
		EntityControl entityControl = null;
		if (player.entity.followedby == null)
		{
			return player.entity;
		}
		if (instance.playerdata[instance.playerdata.Length - 1].entity.followedby == null)
		{
			return instance.playerdata[instance.playerdata.Length - 1].entity;
		}
		return map.tempfollowers[map.tempfollowers.Count - 1];
	}

	public static void StopEntitiesMove()
	{
		EntityControl[] array = UnityEngine.Object.FindObjectsOfType<EntityControl>();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].StopForceMove();
		}
	}

	public static bool CheckAllBool(bool[] array, bool state)
	{
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i] != state)
			{
				return false;
			}
		}
		return true;
	}

	public static EntityControl AddFollower(EntityControl caller, int id)
	{
		Vector3 position = player.transform.position + instance.globalcamdir.forward.normalized * instance.playerdata.Length * 0.05f;
		if (caller != null)
		{
			caller.Unfix();
			id = caller.animid;
			position = caller.transform.position;
			instance.extrafollowers.Add(id);
			UnityEngine.Object.Destroy(caller.gameObject);
		}
		if (id > -1)
		{
			EntityControl lastFollowing = GetLastFollowing();
			EntityControl entityControl = EntityControl.CreateNewEntity("Follower " + id, id, position, lastFollowing);
			if (map != null)
			{
				entityControl.transform.parent = map.transform;
			}
			entityControl.tempfollower = true;
			entityControl.tag = "PFollower";
			player.npc = new List<NPCControl>();
			map.tempfollowers.Add(entityControl);
			lastFollowing.followedby = entityControl.transform;
			return lastFollowing;
		}
		return null;
	}

	public static EntityControl GetExtraFollower(int id)
	{
		EntityControl result = null;
		EntityControl[] array = map.tempfollowers.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i].animid == id)
			{
				return array[i];
			}
		}
		return result;
	}

	public static IEnumerator LateParent(Transform a, Transform b, float time)
	{
		yield return new WaitForSeconds(time);
		a.parent = b;
	}

	private static void ResetDiag()
	{
		tempdiag = "|size," + fontdsize + "|";
	}

	public static void StopDig(bool delayed)
	{
		if (delayed)
		{
			instance.StartCoroutine(DigStop());
		}
		else
		{
			StopDig();
		}
	}

	private static IEnumerator DigStop()
	{
		yield return new WaitForSeconds(0.1f);
		StopDig();
	}

	public static void TeleportFollowers(TPDir direction, Vector3 from)
	{
		TeleportFollowers(1.25f, direction, from);
	}

	public static void StopDig()
	{
		EntityControl[] partyEntities = GetPartyEntities();
		for (int i = 1; i < partyEntities.Length; i++)
		{
			partyEntities[i].LockRigid(false);
			partyEntities[i].spin = Vector3.zero;
			partyEntities[i].startscale = Vector3.one;
			partyEntities[i].sprite.transform.localScale = Vector3.one;
			partyEntities[i].overrideanim = false;
			partyEntities[i].ccol.enabled = true;
		}
	}

	private static void ResetDiag(bool soft)
	{
		ResetDiag();
		if (!soft)
		{
			diagstring = new List<string>();
			currentdialogue = 0;
		}
	}

	public static int GetValueFromString(string input)
	{
		if (input == "money")
		{
			return instance.money;
		}
		if (!input.Contains("var") && !input.Contains("v"))
		{
			return Convert.ToInt32(input);
		}
		return instance.flagvar[Convert.ToInt32(input.Replace("var", "").Replace("v", ""))];
	}

	public static IEnumerator Shrink(Transform obj, float frametime, bool deleteonend)
	{
		Vector3 s = obj.transform.localScale;
		float a = 0f;
		do
		{
			obj.transform.localScale = Vector3.Lerp(s, Vector3.zero, a / frametime);
			a += TieFramerate(1f);
			yield return null;
		}
		while (a < frametime + 1f);
		if (deleteonend)
		{
			UnityEngine.Object.Destroy(obj.gameObject);
		}
	}

	public static void FaceTowardsY(Transform t, Vector3 pos)
	{
		Vector3 eulerAngles = t.eulerAngles;
		t.LookAt(pos);
		t.eulerAngles = new Vector3(eulerAngles.x, t.eulerAngles.y, eulerAngles.z);
	}

	public static void Insert(int value, ref int[] array)
	{
		List<int> list = new List<int>();
		list.AddRange(array);
		list.Add(value);
		array = list.ToArray();
	}

	public static IEnumerator SetText(string text, int fonttype, float? linebreak, bool dialogue, bool tridimensional, Vector3 position, Vector3 cameraoffset, Vector2 size, Transform parent, NPCControl caller)
	{
		float speed = 0f;
		float currentoffset = 0f;
		float currentline = 0f;
		float maxlenght = 0f;
		float bleeppitch = 1f;
		float bleepvolume = size.magnitude;
		float langOffset = 1f;
		int colorindex = 0;
		int sort = 0;
		int transferi = 0;
		int eventcall = -1;
		int writen = 0;
		int ignorenext = 0;
		int layer = 5;
		bool centralize = false;
		bool wavy = false;
		bool shaky = false;
		bool rainbow = false;
		bool glitchy = false;
		bool skipi = false;
		bool end = false;
		bool promptmenu = false;
		bool minibubble = false;
		bool tempoverf = instance.overridefollower;
		bool questboardpromp = false;
		bool tempevent = false;
		bool fadeletter = false;
		bool fontlock = false;
		bool initialflip = false;
		bool ui3d = false;
		bool single = false;
		bool testing = false;
		bool asian = dialogue && AsianLang();
		bool locksize = false;
		bool superglitch = false;
		if (languageid == 6)
		{
			langOffset = 0.9f;
		}
		notextbacktrack = false;
		Time.timeScale = 1f;
		Vector2? dropshadow = null;
		List<EntityControl> returnentitycol = new List<EntityControl>();
		Vector3 camtoffset = instance.camoffset;
		instance.camoffset2 = Vector3.zero;
		TextMesh ndd = null;
		TextMesh ds = null;
		Vector3? transfer = null;
		SpriteRenderer backbox = null;
		GameObject textholder = new GameObject("Text: " + text);
		GameObject textbox = null;
		Animator windowstyle = null;
		List<MiniBubble> bubbles = new List<MiniBubble>();
		List<GameObject> buts = new List<GameObject>();
		Transform tokenbox = null;
		AudioClip bleep = null;
		RigidbodyConstraints tcons = RigidbodyConstraints.FreezePosition;
		if (text != null)
		{
			text = text.Replace("\r\n", "\n");
		}
		textholder.transform.parent = parent;
		textholder.transform.localPosition = position;
		if (!tridimensional)
		{
			textholder.transform.localEulerAngles = Vector3.zero;
		}
		textholder.transform.localScale = Vector3.one;
		textholder.tag = "Text";
		if (fonttype == 0)
		{
			if (languageid == 3)
			{
				fonttype = 3;
			}
			else if (languageid == 6)
			{
				fonttype = 4;
			}
			else if (languageid == 5)
			{
				fonttype = 5;
			}
		}
		if (linebreak.HasValue)
		{
			text = OrganizeLines(text, linebreak.Value, size.x, fonttype);
		}
		if (dialogue)
		{
			instance.letterprompt = -1;
			define = new List<string[]>();
			tempdiag = "|size," + size.x + "," + size.y + "|";
			if (asian)
			{
				text = text.Insert(0, asiansize);
				speed = 0.03f;
			}
			else
			{
				speed = 0.02f;
			}
			currentdialogue = 0;
			instance.skiptext = false;
			instance.isholdingskip = false;
			instance.waitinput = false;
			backtracking = false;
			diagstring = new List<string>();
			instance.discoveryhud = -1f;
			noskip = false;
			instance.inputcooldown = 10f;
			instance.overridefollower = true;
			instance.hudcooldown = 0f;
			if (caller != null)
			{
				if ((caller.interacttype == NPCControl.Interaction.Shop || caller.interacttype == NPCControl.Interaction.CaravanBadge) && (int)caller.shopkeeper.dialogues[1].y != 1)
				{
					instance.showmoney = 10f;
				}
				else
				{
					instance.showmoney = 0f;
				}
			}
			instance.promptpick = -1;
			instance.message = true;
			instance.minipause = true;
			if (player != null && !instance.inevent)
			{
				if (caller != null && (caller.interacttype != NPCControl.Interaction.SavePoint || !instance.inevent))
				{
					if (caller.interacttype == NPCControl.Interaction.Shop || caller.interacttype == NPCControl.Interaction.CaravanBadge)
					{
						player.entity.FaceTowards(caller.shopkeeper.transform.position);
					}
					else
					{
						player.entity.FaceTowards(caller.transform.position);
					}
				}
				initialflip = player.entity.flip;
				player.entity.StopMoving(0);
				tcons = player.entity.rigid.constraints;
				if (caller != null)
				{
					TeleportFollowers(2.55f, 1.5f, TPDir.Away, caller.transform.position);
					yield return null;
				}
			}
			if (cameraoffset.magnitude > 0.1f)
			{
				instance.camoffset += cameraoffset;
			}
			textbox = (maintextbox = UnityEngine.Object.Instantiate(Resources.Load("Prefabs/Textbox")) as GameObject);
			instance.texttail = textbox.transform.GetChild(0);
			instance.tailtarget = parent;
			if (parent != null && parent.GetComponent<EntityControl>() != null)
			{
				bleep = Resources.Load<AudioClip>("Audio/Sounds/Dialogue/Dialogue" + parent.GetComponent<EntityControl>().dialoguebleepid);
				bleeppitch = parent.GetComponent<EntityControl>().bleeppitch;
			}
			if (instance.playerdata != null)
			{
				for (int i = 1; i < instance.playerdata.Length; i++)
				{
					if (instance.playerdata[i].entity != null && instance.playerdata[i].entity.gameObject.activeInHierarchy)
					{
						instance.playerdata[i].entity.StopForceMove(-1, false);
						instance.playerdata[i].entity.sprite.enabled = true;
					}
				}
			}
			if (map != null)
			{
				EntityControl[] array = map.tempfollowers.ToArray();
				for (int j = 0; j < array.Length; j++)
				{
					if (array[j] != null)
					{
						array[j].StopForceMove(-1, false);
					}
				}
			}
			GlobalCommand(ref text);
			textholder.transform.parent = textbox.transform;
			textbox.transform.parent = GUICamera.transform;
			instance.textbox = textholder.transform;
			textbox.transform.localEulerAngles = Vector3.zero;
			textbox.transform.localPosition = new Vector3(0f, 3.25f, 10f);
			if (position.magnitude > 0.1f)
			{
				textholder.transform.localPosition = position;
			}
			else
			{
				textholder.transform.localPosition = new Vector3(-5.5f, 0.9f);
			}
			textbox.transform.localScale = Vector3.zero;
			textbox.AddComponent<DialogueAnim>();
			instance.blinker = textbox.transform.GetChild(1).GetComponent<SpriteRenderer>();
			windowstyle = textbox.GetComponent<Animator>();
			textholder.transform.localEulerAngles = Vector3.zero;
			if (map != null)
			{
				map.StopMovingEntities(null, -1);
			}
			if (player != null)
			{
				while (player.switchcooldown > 0f)
				{
					yield return null;
				}
			}
		}
		else
		{
			text = text.Insert(0, "|spd,0|");
		}
		for (int k = 0; k < text.Length; k++)
		{
			if (dialogue)
			{
				fontdsize = size.x;
				fontdtype = fonttype;
				linebr = linebreak.Value;
				if (k < 10 && player != null && !instance.inevent)
				{
					player.entity.flip = initialflip;
				}
			}
			if (text[k] == '\n')
			{
				currentoffset = 0f;
				currentline -= 0.7f * size.y * (asian ? 1.25f : 1f);
			}
			else if (text[k] == '|')
			{
				SetTalk(dialogue, false);
				string command = "";
				for (int l = k + 1; l < text.Length && text[l] != '|'; l++)
				{
					command += text[l];
				}
				string[] temp = command.Split(',');
				Commands com = (Commands)Enum.Parse(typeof(Commands), temp[0].Replace(".", ""), true);
				if (dialogue && (com == Commands.Icon || com == Commands.Button || com == Commands.Size || com == Commands.Shaky || com == Commands.Wavy || com == Commands.Rainbow || com == Commands.Glitchy || com == Commands.Halfline || com == Commands.Quarterline))
				{
					tempdiag = tempdiag + "|" + command + "|";
				}
				if (ignorenext <= 0 && (!testing || (dialogue && (com == Commands.Next || com == Commands.Blank || com == Commands.Speed || com == Commands.Icon || com == Commands.Menu || com == Commands.Color || com == Commands.Shaky || com == Commands.Faketail || com == Commands.Rainbow || com == Commands.Glitchy || com == Commands.Wavy || com == Commands.Tail || com == Commands.Tailextra || com == Commands.Goto || com == Commands.Call || com == Commands.Minibubble || com == Commands.Breakend || temp[0].Contains("line")))))
				{
					string text7;
					string newValue2;
					switch (com)
					{
					case Commands.Testdiag:
						testing = true;
						break;
					case Commands.Single:
						single = ((temp.Length <= 1) ? (!single) : Convert.ToBoolean(temp[1]));
						break;
					case Commands.Singlebreak:
						if (single)
						{
							text = OrganizeLines(text, 99999f, size.x, fonttype);
						}
						break;
					case Commands.Define:
						define.Add(new string[2]
						{
							temp[2],
							temp[1]
						});
						break;
					case Commands.Showtokens:
						if (temp.Length == 1)
						{
							if (tokenbox != null)
							{
								UnityEngine.Object.Destroy(tokenbox.gameObject);
							}
							SpriteRenderer component3 = NewUIObject("TokenBar", GUICamera.transform, new Vector3(15f, (instance.showmoney > 0f) ? (-3f) : (-4.25f), 10f), new Vector3(0.55f, 0.6f, 1f), guisprites[4], 0).GetComponent<SpriteRenderer>();
							component3.color = new Color(0.9f, 0.5f, 0f);
							tokenbox = component3.transform;
							instance.StartCoroutine(SetText("|sort,10||color,4||dropshadow,0.1,-0.1|" + instance.flagvar[27].ToString().PadLeft(4, '0'), 2, null, false, false, new Vector3(-0.75f, -0.4f), Vector3.zero, Vector2.one * 1.75f, component3.transform, null));
							NewUIObject("Icon", tokenbox, new Vector3(-2.25f, 0.1f), Vector3.one * 2f, itemsprites[0, 110], 1).GetComponent<SpriteRenderer>();
							DialogueAnim dialogueAnim = tokenbox.gameObject.AddComponent<DialogueAnim>();
							dialogueAnim.targetpos = new Vector3(7f, dialogueAnim.transform.localPosition.y, 10f);
							dialogueAnim.targetscale = new Vector3(0.55f, 0.6f, 1f);
							dialogueAnim.speed = 0.4f;
						}
						else if (tokenbox != null && temp[1] == "null")
						{
							UnityEngine.Object.Destroy(tokenbox.gameObject);
						}
						break;
					case Commands.Breakend:
						text = text.Replace("|" + command + "|", "|break||destroyminibubble||end|");
						k--;
						skipi = true;
						break;
					case Commands.Fixchompy:
						if (map.chompy != null)
						{
							map.chompy.following = instance.playerdata[instance.playerdata.Length - 1].entity;
						}
						break;
					case Commands.Switch:
					{
						EntityControl entity8 = GetEntity(Convert.ToInt32(temp[1]));
						if (temp.Length == 2)
						{
							entity8.npcdata.hit = !entity8.npcdata.hit;
						}
						else
						{
							entity8.npcdata.hit = Convert.ToBoolean(temp[2]);
						}
						break;
					}
					case Commands.Triui:
						ui3d = !ui3d;
						break;
					case Commands.Igcolmove:
					{
						EntityControl entity5 = GetEntity(temp[1], caller.entity);
						if (!(entity5 != null) || !(player != null))
						{
							break;
						}
						returnentitycol.Add(entity5);
						Physics.IgnoreCollision(entity5.ccol, player.entity.ccol, true);
						if (entity5.npcdata != null)
						{
							if (entity5.npcdata.pusher != null)
							{
								Physics.IgnoreCollision(entity5.npcdata.pusher, player.entity.ccol, true);
							}
							if (entity5.npcdata.scol != null)
							{
								Physics.IgnoreCollision(entity5.npcdata.scol, player.entity.ccol, true);
							}
							if (entity5.npcdata.boxcol != null)
							{
								Physics.IgnoreCollision(entity5.npcdata.boxcol, player.entity.ccol, true);
							}
						}
						player.entity.hitwall = false;
						break;
					}
					case Commands.Battle:
					{
						int[] array8;
						if (temp[1] == "all")
						{
							array8 = GradualFill(Enum.GetNames(typeof(Enemies)).Length);
						}
						else
						{
							string[] array9 = temp[1].Split('.');
							array8 = new int[array9.Length];
							for (int num43 = 0; num43 < array8.Length; num43++)
							{
								array8[num43] = Convert.ToInt32(array9[num43]);
							}
						}
						SaveCameraPosition();
						instance.StartCoroutine(BattleControl.StartBattle(array8, (temp.Length > 3) ? Convert.ToInt32(temp[3]) : (-1), -1, (temp.Length > 2) ? temp[2] : null, null, false));
						bool tmes = instance.message;
						instance.message = false;
						textbox.GetComponent<DialogueAnim>().shrink = true;
						yield return new WaitForSeconds(0.5f);
						while (battle != null)
						{
							yield return null;
						}
						yield return new WaitForSeconds(0.5f);
						instance.message = tmes;
						textbox.GetComponent<DialogueAnim>().shrink = false;
						break;
					}
					case Commands.Lockbacktrack:
						notextbacktrack = true;
						break;
					case Commands.Removestat:
						MonoBehaviour.print(GetValueFromString(temp[1]));
						instance.statbonus.RemoveAt(GetValueFromString(temp[1]));
						if (temp.Length > 2 && temp[2] == "true")
						{
							ApplyStatBonus();
						}
						break;
					case Commands.Addstat:
						AddStatBonus((StatBonus)GetValueFromString(temp[1]), GetValueFromString(temp[2]), GetValueFromString(temp[3]));
						if (temp.Length > 4 && temp[4] == "true")
						{
							ApplyStatBonus();
						}
						break;
					case Commands.Area:
						text = text.Replace("|area|", areanames[instance.areaid]);
						k--;
						skipi = true;
						break;
					case Commands.Librarybook:
						instance.flagvar[15]++;
						UnityEngine.Object.FindObjectOfType<LibraryShelf>().Refresh();
						break;
					case Commands.Setbreak:
						if (temp.Length > 1)
						{
							linebreak = ((!(temp[1] == "null")) ? new float?(Convert.ToSingle(temp[1])) : ((float?)null));
							if (temp.Length > 2 && temp[2] == "true")
							{
								text = OrganizeLines(text, linebreak.Value, size.x, fonttype);
							}
						}
						break;
					case Commands.Music:
					{
						float fadespeed = 0.1f;
						if (temp.Length > 2)
						{
							fadespeed = Convert.ToSingle(temp[2]);
						}
						if (temp.Length == 1 || temp[1] == "null")
						{
							ChangeMusic((AudioClip)null, fadespeed);
						}
						else
						{
							ChangeMusic(temp[1], fadespeed);
						}
						break;
					}
					case Commands.Sound:
					{
						float pitch = 1f;
						float volume = 1f;
						int id2 = -1;
						bool loop = false;
						if (temp.Length > 2)
						{
							id2 = Convert.ToInt32(temp[2]);
						}
						if (temp.Length > 3)
						{
							pitch = Convert.ToSingle(temp[3]);
						}
						if (temp.Length > 4)
						{
							volume = Convert.ToSingle(temp[4]);
						}
						if (temp.Length > 5)
						{
							loop = Convert.ToBoolean(temp[5]);
						}
						PlaySound(temp[1], id2, pitch, volume, loop);
						break;
					}
					case Commands.Shakecamera:
						ShakeScreen(Vector3.one * Convert.ToSingle(temp[1]), Convert.ToSingle(temp[2]), temp.Length > 3 && temp[3] == "true");
						break;
					case Commands.Setprize:
						if (temp[1] == "find")
						{
							int num2 = IntFromString(temp[2]);
							for (int m = 0; m < instance.prizeids.Length; m++)
							{
								if (instance.prizeids[m] == num2 && instance.flagvar[instance.prizeflags[m]] == 2)
								{
									instance.flagvar[instance.prizeflags[m]] = 3;
									List<int> list = new List<int>(caravanorder);
									list.Remove(instance.prizeids[m]);
									caravanorder = list.ToArray();
									break;
								}
							}
						}
						else if (temp[1] == "this" && caller != null)
						{
							for (int n = 0; n < instance.prizeids.Length; n++)
							{
								if (instance.prizeids[n] == caller.entity.animstate && instance.flagvar[instance.prizeflags[n]] == 2)
								{
									instance.flagvar[instance.prizeflags[n]] = 3;
									break;
								}
							}
						}
						else
						{
							instance.flagvar[instance.prizeflags[(temp[1] == "var") ? Convert.ToInt32(temp[2]) : Convert.ToInt32(temp[1])]] = 3;
						}
						break;
					case Commands.Fadeletter:
						fadeletter = !fadeletter;
						break;
					case Commands.Kinematicplayer:
						if (temp[1] == "temp")
						{
							player.entity.rigid.isKinematic = true;
							text += "|kinematicplayer,false|";
						}
						else
						{
							player.entity.rigid.isKinematic = Convert.ToBoolean(temp[1]);
						}
						break;
					case Commands.Removefollower:
						if (temp.Length == 1)
						{
							instance.extrafollowers = new List<int>();
						}
						else
						{
							instance.extrafollowers.Remove(Convert.ToInt32(temp[1]));
						}
						break;
					case Commands.Follow:
						temp = new string[3] { "", "this", "true" };
						goto case Commands.Addfollower;
					case Commands.Addfollower:
						if (temp[1] == "this")
						{
							if (caller != null)
							{
								AddFollower(caller.entity, -1);
							}
							if (temp.Length > 2 && temp[2] == "true")
							{
								textbox.GetComponent<DialogueAnim>().shrink = true;
								EventControl.temproutine = instance.StartCoroutine(EventControl.PartyMover(false));
								while (EventControl.temproutine != null)
								{
									yield return null;
								}
								textbox.GetComponent<DialogueAnim>().shrink = false;
							}
						}
						else
						{
							instance.extrafollowers.Add((temp[1] == "var") ? instance.flagvar[Convert.ToInt32(temp[2])] : Convert.ToInt32(temp[1]));
						}
						break;
					case Commands.Dropshadow:
						dropshadow = ((temp.Length != 1) ? ((temp.Length != 2) ? new Vector2?(new Vector2(Convert.ToSingle(temp[1]), Convert.ToSingle(temp[2]))) : new Vector2?(Vector2.one * Convert.ToSingle(temp[1]))) : ((Vector2?)null));
						break;
					case Commands.Stars:
					{
						int num10 = Convert.ToInt32(temp[1]);
						for (int num11 = 0; num11 < num10; num11++)
						{
							SpriteRenderer spriteRenderer = new GameObject().AddComponent<SpriteRenderer>();
							spriteRenderer.sprite = guisprites[100];
							spriteRenderer.transform.parent = textholder.transform;
							spriteRenderer.transform.localScale = ((battle != null) ? new Vector3(0.5f, 0.25f, 1f) : (Vector3.one / 2f));
							spriteRenderer.transform.localEulerAngles = Vector3.zero;
							spriteRenderer.color = Color.white;
							spriteRenderer.gameObject.layer = 5;
							spriteRenderer.sortingOrder = sort + num11;
							spriteRenderer.transform.localPosition = new Vector3(currentoffset, currentline);
							currentoffset += spriteRenderer.sprite.bounds.extents.x - 0.15f;
						}
						break;
					}
					case Commands.Mothfly:
					{
						int num21 = ((temp.Length <= 1) ? 1 : Convert.ToInt32(temp[1]));
						string text4 = "";
						float num22 = ((temp.Length > 2) ? Convert.ToSingle(temp[2]) : 0.35f);
						for (int num23 = 0; num23 < num21; num23++)
						{
							text4 = text4 + "|icon,146," + num22 + "|";
							if (writen < 2)
							{
								if (num23 == 0)
								{
									text4 += "  ";
								}
								if (num23 == 1)
								{
									text4 += " ";
								}
							}
						}
						text = text.Replace("|" + command + "|", text4);
						k--;
						skipi = true;
						break;
					}
					case Commands.Icon:
					{
						int num19 = Convert.ToInt32(temp[1]);
						float num20 = 1f;
						if (temp.Length > 2)
						{
							num20 = Convert.ToSingle(temp[2]);
						}
						SpriteRenderer component2 = NewUIObject("icon" + num19, textholder.transform, new Vector3(currentoffset, currentline), new Vector3(size.x, size.y, 1f) * num20, guisprites[num19], (temp.Length > 3) ? Convert.ToInt32(temp[3]) : sort).GetComponent<SpriteRenderer>();
						component2.transform.localPosition = new Vector3(currentoffset, currentline) + (Vector3)component2.sprite.pivot / (component2.sprite.pixelsPerUnit * 2f);
						if (num19 == 24)
						{
							component2.transform.localPosition += new Vector3(-0.1f, -0.25f);
						}
						component2.gameObject.tag = "Letter";
						buts.Add(component2.gameObject);
						currentoffset += component2.bounds.size.x;
						if (linebreak.HasValue)
						{
							BreakLine(ref currentoffset, ref currentline, linebreak.Value, size);
						}
						FontEffects fontEffects = component2.gameObject.AddComponent<FontEffects>();
						fontEffects.SetEffects(shaky, wavy, rainbow, false, false, 0, k);
						if (num19 == 146)
						{
							fontEffects.rotate = true;
						}
						if (single)
						{
							text = text.Replace("|" + command + "|", "\t");
							k--;
							skipi = true;
						}
						if (dialogue)
						{
							writen++;
							SetTalk(true, true);
							if (speed > 0f && !instance.skiptext)
							{
								PlayBleep(bleep, bleeppitch, bleepvolume, k);
								yield return new WaitForSeconds(speed);
							}
						}
						break;
					}
					case Commands.Checksum:
						if (GetValueFromString(temp[1]) + GetValueFromString(temp[2]) <= GetValueFromString(temp[3]))
						{
							break;
						}
						temp = new string[2]
						{
							"",
							temp[4]
						};
						goto case Commands.Goto;
					case Commands.Emoticon:
						GetEntity(caller, temp[1]).Emoticon(Convert.ToInt32(temp[2]), Convert.ToInt32(temp[3]));
						break;
					case Commands.Tab:
						if (ndd != null)
						{
							ndd.tabSize = Convert.ToSingle(temp[1]);
						}
						break;
					case Commands.Exp:
					case Commands.Level:
					{
						bool flag2 = false;
						int num16;
						if (temp[1] == "add")
						{
							num16 = ((!temp[2].Contains("var")) ? Convert.ToInt32(temp[2]) : instance.flagvar[Convert.ToInt32(temp[2].Replace("var", ""))]);
						}
						else if (temp[1] == "sub")
						{
							num16 = ((!temp[2].Contains("var")) ? (-Convert.ToInt32(temp[2])) : (-instance.flagvar[Convert.ToInt32(temp[2].Replace("var", ""))]));
						}
						else
						{
							flag2 = true;
							num16 = ((!temp[1].Contains("var")) ? Convert.ToInt32(temp[1]) : instance.flagvar[Convert.ToInt32(temp[1].Replace("var", ""))]);
						}
						if (flag2)
						{
							if (com == Commands.Exp)
							{
								instance.partyexp = Mathf.Clamp(num16, 0, instance.neededexp - 1);
							}
							else
							{
								instance.partylevel = Mathf.Clamp(num16, 1, 99);
							}
						}
						else if (com == Commands.Exp)
						{
							instance.partyexp = Mathf.Clamp(instance.partyexp + num16, 0, instance.neededexp - 1);
						}
						else
						{
							instance.partylevel = Mathf.Clamp(instance.partylevel + num16, 1, 99);
						}
						break;
					}
					case Commands.Font:
						fonttype = Convert.ToInt32(temp[1]);
						if (temp.Length > 2)
						{
							fontlock = temp[2] == "lock";
						}
						break;
					case Commands.Breakflag:
						if (!instance.flags[Convert.ToInt32(temp[1])])
						{
							end = true;
						}
						break;
					case Commands.Openpause:
						if (pausemenu != null)
						{
							pausemenu.ChangeWindow(Convert.ToInt32(temp[1]));
						}
						end = true;
						break;
					case Commands.Exitgame:
						if (pausemenu != null && pausemenu.calledfrommain)
						{
							Application.Quit();
						}
						else
						{
							FadeMusic(0.15f);
							PlayTransition(0, 0, 0.1f, Color.black);
							yield return new WaitForSeconds(1f);
							SceneManager.LoadScene(0);
						}
						end = true;
						break;
					case Commands.Bleep:
						if (temp.Length == 2)
						{
							if (temp[1][0] == '@')
							{
								EntityControl entity7 = GetEntity(temp[1].Replace("@", ""));
								temp[1] = entity7.dialoguebleepid.ToString();
								bleeppitch = entity7.bleeppitch;
							}
							else
							{
								int num65 = Convert.ToInt32(temp[1]) + 1;
								temp[1] = endata[num65].bleepid.ToString();
								bleeppitch = endata[num65].bleeppitch;
							}
						}
						else
						{
							bleeppitch = Convert.ToSingle(temp[2]);
							if (temp.Length > 3)
							{
								bleepvolume = Convert.ToSingle(temp[3]);
							}
						}
						bleep = Resources.Load<AudioClip>("Audio/Sounds/Dialogue/Dialogue" + temp[1]);
						break;
					case Commands.Discovery:
						if (temp[1] == "all")
						{
							for (int num50 = 0; num50 < instance.librarystuff.GetLength(0); num50++)
							{
								for (int num51 = 0; num51 < instance.librarystuff.GetLength(1); num51++)
								{
									instance.librarystuff[num50, num51] = true;
								}
							}
						}
						else
						{
							int num52 = Convert.ToInt32(temp[1]);
							if (!instance.librarystuff[0, num52])
							{
								UpdateJounal(Library.Discovery, num52);
							}
						}
						break;
					case Commands.Hidespeed:
						textbox.GetComponent<DialogueAnim>().shrinkspeed = Convert.ToSingle(temp[1]);
						break;
					case Commands.Jump:
					{
						EntityControl entityControl2 = ((!(caller != null)) ? GetEntity(Convert.ToInt32(temp[1])) : GetEntity(temp[1], caller.entity));
						if (temp.Length == 2)
						{
							entityControl2.Jump();
						}
						else
						{
							entityControl2.Jump(Convert.ToSingle(temp[2]));
						}
						break;
					}
					case Commands.Setvar:
						if (temp[1] == "add" || temp[1] == "sub")
						{
							instance.flagvar[Convert.ToInt32(temp[2])] += ((temp[3] == "var") ? instance.flagvar[Convert.ToInt32(temp[4])] : Convert.ToInt32(temp[3])) * ((!(temp[1] == "sub")) ? 1 : (-1));
						}
						else
						{
							instance.flagvar[Convert.ToInt32(temp[1])] = Convert.ToInt32(temp[2]);
						}
						break;
					case Commands.Teleportparty:
						if (temp.Length == 1)
						{
							TeleportFollowers();
						}
						else if (temp.Length == 2)
						{
							TeleportFollowers(Convert.ToBoolean(temp[1]));
						}
						else
						{
							TeleportFollowers(Convert.ToSingle(temp[1]), (temp.Length > 2) ? ((TPDir)Enum.Parse(typeof(TPDir), temp[2], true)) : TPDir.Center, caller.transform);
						}
						break;
					case Commands.Resetregion:
					{
						for (int num79 = 0; num79 < instance.regionalflags.Length; num79++)
						{
							instance.regionalflags[num79] = false;
						}
						break;
					}
					case Commands.Destroydescbox:
						if ((caller != null && caller.interacttype != NPCControl.Interaction.CaravanBadge && caller.interacttype != NPCControl.Interaction.Shop) || (temp.Length > 1 && temp[1] == "1"))
						{
							caller.DestroyDescWindow();
						}
						break;
					case Commands.Giveitem:
					{
						if (temp[1] == "all")
						{
							for (int num67 = 0; num67 < Enum.GetNames(typeof(Items)).Length; num67++)
							{
								instance.items[0].Add(num67);
							}
							break;
						}
						EntityControl temptail = ((instance.tailtarget != null) ? instance.tailtarget.GetComponent<EntityControl>() : null);
						int itemtype = Convert.ToInt32(temp[1]);
						int id3 = -1;
						int num68;
						int redirect;
						if (temp[2] == "var")
						{
							num68 = instance.flagvar[Convert.ToInt32(temp[3])];
							redirect = Convert.ToInt32(temp[4]);
							if (temp.Length > 5)
							{
								id3 = Convert.ToInt32(temp[5]);
							}
						}
						else
						{
							num68 = Convert.ToInt32(temp[2]);
							redirect = Convert.ToInt32(temp[3]);
							if (temp.Length > 4)
							{
								id3 = Convert.ToInt32(temp[4]);
							}
						}
						instance.flagstring[1] = menutext[125];
						if (itemtype == 2 && instance.flags[681] && num68 != 11)
						{
							num68 = GetRandomMedal();
						}
						if (itemtype > -1 && itemtype < 3 && caller != null && caller.interacttype != NPCControl.Interaction.CaravanBadge && caller.interacttype != NPCControl.Interaction.Shop && (itemtype != 1 || instance.insideid == -1 || num68 != 110))
						{
							caller.CreateDescWindow((itemtype == 2) ? 2 : 0, num68);
						}
						if ((itemtype != 0 || instance.items[0].Count + 1 > instance.maxitems) && itemtype == 0)
						{
							break;
						}
						DialogueAnim a = textbox.GetComponent<DialogueAnim>();
						a.shrink = true;
						SpriteRenderer[] t = new SpriteRenderer[3];
						Sprite sprite = ((itemtype == 3) ? guisprites[83] : ((itemtype > -1) ? GetItemSprite(itemtype == 2, num68) : ((num68 >= 20) ? itemsprites[0, 186] : ((num68 < 5) ? itemsprites[0, 6] : itemsprites[0, 7]))));
						EntityControl ent = GetEntity(id3);
						ent.rigid.velocity = Vector3.zero;
						t[0] = NewSpriteObject("tempitem", ent.transform.position + new Vector3(0f, 2.75f, -0.1f), instance.globalcamdir.eulerAngles, null, sprite, spritemat);
						t[1] = NewUIObject("fauxmessage", GUICamera.transform, new Vector3(0f, 3f, 10f), Vector3.one, Resources.LoadAll<Sprite>("sprites/gui/textbox")[3]).GetComponent<SpriteRenderer>();
						t[0].gameObject.layer = 14;
						SpriteRenderer spriteRenderer2 = NewSpriteObject("back", new Vector3(0f, 0f, 0.2f), Vector3.zero, t[0].transform, guisprites[85], t[0].material);
						spriteRenderer2.transform.localScale = Vector3.zero;
						spriteRenderer2.gameObject.AddComponent<DialogueAnim>();
						spriteRenderer2.gameObject.layer = 14;
						switch (itemtype)
						{
						case 0:
							spriteRenderer2.material.color = new Color(0f, 0.7f, 0.7f);
							break;
						case 1:
							spriteRenderer2.material.color = new Color(1f, 0.3f, 0.4f);
							break;
						default:
							spriteRenderer2.material.color = new Color(1f, 0.5f, 0f);
							break;
						}
						int num69 = 106;
						if (itemtype > -1)
						{
							instance.showmoney = 0f;
							instance.moneyt = instance.money;
						}
						if (itemtype == -1)
						{
							instance.showmoney = 1f;
							instance.money = Mathf.Clamp(instance.money + num68, 0, 999);
							instance.flagvar[0] = num68;
							num69 = 110;
							itemtype = 0;
						}
else if (itemtype < 2)
{
    if (itemtype >= 0 && itemtype < instance.items.Length)
    {
        if (num68 >= 0 && num68 < itemdata.GetLength(1))
        {
            instance.flagstring[0] = itemdata[0, num68, 0];
            instance.flagstring[1] = itemdata[0, num68, 3];
        }

        if (!instance.items[itemtype].Contains(num68))
        {
			Debug.LogError("PERMIT GIVEITEM REACHED: type=" + itemtype + " id=" + num68);
            instance.items[itemtype].Add(num68);
			
        }
    }
						}
						else
						{
							switch (itemtype)
							{
							case 2:
								instance.flagstring[0] = GetBadgeName(num68);
								instance.flagstring[1] = badgedata[num68, 6];
								instance.badges.Add(new int[2] { num68, -2 });
								break;
							case 3:
								instance.flagvar[14]++;
								instance.flagstring[0] = menutext[112];
								instance.crystalbflags[num68] = true;
								break;
							}
						}
						instance.StartCoroutine(SetText("|sort,1||center||halfline|" + menutext[num69], Vector3.zero, t[1].transform));
						ent.overrideanim = true;
						ent.animstate = 4;
						PlaySound("ItemGet" + itemtype);
						yield return new WaitForSeconds(0.45f);
						t[2] = NewUIObject("fauxcursor", t[1].transform, new Vector3(6f, -2f), Vector3.one, cursorsprite[0], 20).GetComponent<SpriteRenderer>();
						t[2].gameObject.AddComponent<SpriteBounce>().SetUp(0.1f, 7f);
						t[2].transform.localEulerAngles = new Vector3(0f, 0f, -90f);
						instance.waitinput = true;
						yield return null;
						while (instance.waitinput)
						{
							yield return null;
						}
						for (int num70 = 0; num70 < t.Length; num70++)
						{
							UnityEngine.Object.Destroy(t[num70].gameObject);
						}
						a.shrink = false;
						instance.waitinput = false;
						instance.skiptext = false;
						ent.overrideanim = false;
						ent.animstate = 0;
						string text10 = "";
						if (!instance.flags[31] && itemtype == 2)
						{
							text10 = "|tail,null||destroydescbox||blank||boxstyle,4|" + commondialogue[31] + "|flag,31,true||break||gettail," + temptail.originalid + "||boxstyle,0|";
						}
						text = OrganizeLines(text10 + "|destroydescbox||blank|" + GetDialogueText(redirect), linebreak.Value, size.x, fonttype);
						skipi = true;
						k = -1;
						break;
					}
					case Commands.Lockmovement:
						player.entity.rigid.constraints = (RigidbodyConstraints)122;
						break;
					case Commands.Backbox:
						if (backbox == null)
						{
							backbox = NewUIObject("backbox", textholder.transform, Vector3.zero, Vector3.one, guisprites[0], sort - 2).GetComponent<SpriteRenderer>();
							Color color = Color.white;
							if (temp.Length > 1)
							{
								color = instance.textcolors[Convert.ToInt32(temp[1])];
							}
							backbox.color = new Color(color.r, color.g, color.b, 0.4f);
						}
						break;
					case Commands.Lock:
						instance.message = true;
						instance.minipause = true;
						break;
					case Commands.Removemaplimits:
						if (temp.Length == 1)
						{
							map.RemoveLimit(false);
						}
						else
						{
							map.RemoveLimit(Convert.ToBoolean(temp[1]));
						}
						break;
					case Commands.Resetmaplimits:
						if (temp.Length == 1)
						{
							map.RestoreLimit(false);
						}
						else
						{
							map.RestoreLimit(Convert.ToBoolean(temp[1]));
						}
						break;
					case Commands.Resetcamera:
						ResetCamera();
						break;
					case Commands.Shoppool:
						if (temp[1] == "reset")
						{
							if (temp.Length == 2)
							{
								for (int num31 = 0; num31 < instance.badgeshops.Length; num31++)
								{
									instance.badgeshops[num31] = new List<int>();
								}
							}
							else
							{
								instance.badgeshops[temp[2].Contains("var") ? instance.flagvar[Convert.ToInt32(temp[2].Replace("var", ""))] : Convert.ToInt32(temp[2])] = new List<int>();
							}
						}
						else
						{
							instance.badgeshops[temp[1].Contains("var") ? instance.flagvar[Convert.ToInt32(temp[1].Replace("var", ""))] : Convert.ToInt32(temp[1])].Add(temp[2].Contains("var") ? instance.flagvar[Convert.ToInt32(temp[2].Replace("var", ""))] : Convert.ToInt32(temp[2]));
						}
						break;
					case Commands.Optiontovar:
						instance.flagvar[(temp.Length != 1) ? Convert.ToInt32(temp[1]) : 0] = instance.option;
						break;
					case Commands.Next:
						instance.waitinput = true;
						instance.skiptext = false;
						noskip = false;
						if (instance.tailtarget != null)
						{
							instance.tailtarget.GetComponent<EntityControl>().talking = false;
						}
						while (instance.waitinput)
						{
							yield return null;
						}
						yield return null;
						if (currentdialogue == diagstring.Count && !backtracking)
						{
							currentdialogue++;
							diagstring.Add(OrganizeLines(tempdiag, linebreak.Value, size.x, fonttype));
							tempdiag = "|size," + size.x + "," + size.y + "|";
						}
						while (currentdialogue < diagstring.Count)
						{
							if (buts != null && buts.Count > 0)
							{
								for (int num80 = 0; num80 < buts.Count; num80++)
								{
									if (buts[num80] != null)
									{
										UnityEngine.Object.Destroy(buts[num80].gameObject);
									}
								}
							}
							yield return null;
						}
						yield return null;
						backtracking = false;
						goto case Commands.Blank;
					case Commands.Chapterintro:
					{
						bool tmes = instance.message;
						bool a2 = instance.minipause;
						chaptername = instance.StartCoroutine(ChapterName(Convert.ToInt32(temp[1])));
						while (chaptername != null)
						{
							yield return null;
						}
						instance.message = tmes;
						instance.minipause = a2;
						break;
					}
					case Commands.Break:
						instance.waitinput = true;
						instance.skiptext = false;
						noskip = false;
						if (instance.tailtarget != null)
						{
							instance.tailtarget.GetComponent<EntityControl>().talking = false;
						}
						while (instance.waitinput)
						{
							yield return null;
						}
						break;
					case Commands.Cancelaction:
						if (player != null)
						{
							player.CancelAction();
						}
						break;
					case Commands.Backline:
						currentoffset = 0f;
						currentline += 0.7f * size.y * ((temp.Length > 2) ? Convert.ToSingle(temp[2]) : 1f);
						if (dialogue && !backtracking)
						{
							tempdiag = tempdiag + "|" + command + "|";
						}
						break;
					case Commands.Line:
					{
						float num66 = 1f;
						if (temp.Length > 1)
						{
							num66 = Convert.ToSingle(temp[1]);
						}
						currentoffset = 0f;
						currentline -= 0.7f * size.y * num66;
						if (!single)
						{
							if (dialogue && !backtracking)
							{
								tempdiag = tempdiag + "|" + command + "|";
							}
						}
						else
						{
							ndd = null;
							ds = null;
						}
						break;
					}
					case Commands.Pauseline:
						if (!(pausemenu != null) || pausemenu.windowid >= 3)
						{
							break;
						}
						goto case Commands.Line;
					case Commands.Shopline:
						if (!FreePlayer() && (!(instance.itemlist != null) || !(pausemenu == null)))
						{
							break;
						}
						goto case Commands.Line;
					case Commands.Unpauseline:
						if (!(pausemenu == null))
						{
							break;
						}
						goto case Commands.Line;
					case Commands.Mapsize:
						if (!(pausemenu != null) || pausemenu.windowid != 6)
						{
							break;
						}
						goto case Commands.Size;
					case Commands.Librarysize:
						if (!(pausemenu != null) || pausemenu.windowid != 3)
						{
							break;
						}
						goto case Commands.Size;
					case Commands.Battlesize:
						if (!(battle != null))
						{
							break;
						}
						if (temp[1] == "multi")
						{
							temp = new string[3]
							{
								"",
								temp[2],
								temp[3]
							};
							goto case Commands.Sizemulti;
						}
						goto case Commands.Size;
					case Commands.Unpausesize:
						if (!(pausemenu == null) || !(instance.questboardobj == null))
						{
							break;
						}
						goto case Commands.Size;
					case Commands.Pausesize:
						if (!(pausemenu != null) || !(instance.questboardobj == null))
						{
							break;
						}
						goto case Commands.Size;
					case Commands.Listsize:
						if (!instance.inlist || !(pausemenu == null) || !(battle == null))
						{
							break;
						}
						if (temp[1] == "multi")
						{
							temp = new string[3]
							{
								"",
								temp[2],
								temp[3]
							};
							goto case Commands.Sizemulti;
						}
						goto case Commands.Size;
					case Commands.Questsize:
						if (!(instance.questboardobj != null))
						{
							break;
						}
						goto case Commands.Size;
					case Commands.Questbreak:
						if (instance.questboardobj != null == (temp.Length == 1 && Convert.ToBoolean(temp[1])))
						{
							k = text.Length;
						}
						break;
					case Commands.Libraryline:
						if (!(pausemenu != null) || pausemenu.windowid != 3)
						{
							break;
						}
						goto case Commands.Line;
					case Commands.Halfline:
						temp = new string[2] { "", "0.5" };
						goto case Commands.Line;
					case Commands.Quarterline:
						temp = new string[2] { "", "0.25" };
						goto case Commands.Line;
					case Commands.Blank:
					{
						colorindex = 0;
						TextMesh[] componentsInChildren = textholder.GetComponentsInChildren<TextMesh>();
						for (int num17 = 0; num17 < componentsInChildren.Length; num17++)
						{
							DisableLetter(componentsInChildren[num17]);
						}
						currentline = 0f;
						currentoffset = 0f;
						writen = 0;
						maxlenght = 0f;
						ResetDiag();
						GameObject[] array4 = buts.ToArray();
						for (int num18 = 0; num18 < array4.Length; num18++)
						{
							UnityEngine.Object.Destroy(array4[num18]);
						}
						buts = new List<GameObject>();
						yield return null;
						if (temp.Length <= 1)
						{
							break;
						}
						goto case Commands.Tail;
					}
					case Commands.Center:
						centralize = !centralize;
						if (temp.Length > 1)
						{
							currentline = 0f;
							if (position.magnitude > 0.1f)
							{
								textholder.transform.localPosition = position;
							}
							else
							{
								textholder.transform.localPosition = new Vector3(-5.5f, 0.9f);
							}
						}
						break;
					case Commands.Stopskip:
						instance.skiptext = false;
						instance.isholdingskip = fadeletter;
						break;
					case Commands.Noskip:
						instance.skiptext = false;
						instance.isholdingskip = fadeletter;
						noskip = true;
						break;
					case Commands.End:
						end = true;
						if (caller != null && caller.objecttype == NPCControl.ObjectTypes.Item)
						{
							caller.StartCoroutine(caller.entity.Death());
						}
						break;
					case Commands.Hide:
						if (textbox != null && textbox.GetComponent<DialogueAnim>() != null)
						{
							textbox.GetComponent<DialogueAnim>().shrink = !textbox.GetComponent<DialogueAnim>().shrink;
						}
						break;
					case Commands.Faketail:
						if (textbox != null && textbox.GetComponent<DialogueAnim>() != null)
						{
							DialogueAnim a = textbox.GetComponent<DialogueAnim>();
							a.shrink = true;
							yield return new WaitForSeconds(0.15f);
							a.shrink = false;
							yield return new WaitForSeconds(0.15f);
						}
						break;
					case Commands.Rainbow:
						rainbow = !rainbow;
						if (ndd != null)
						{
							FontEffects fontEffects2 = ndd.GetComponent<FontEffects>();
							if (fontEffects2 == null)
							{
								fontEffects2 = ndd.gameObject.AddComponent<FontEffects>();
							}
							fontEffects2.SetEffects(false, false, rainbow, false, false, 0, 0);
						}
						break;
					case Commands.Shaky:
						shaky = !shaky;
						break;
					case Commands.Wavy:
						wavy = !wavy;
						break;
					case Commands.Glitchy:
						glitchy = !glitchy;
						superglitch = temp.Length > 1 && temp[1] == "1";
						break;
					case Commands.Overfollower:
					{
						if (temp.Length > 1)
						{
							instance.overridefollower = Convert.ToBoolean(temp[1]);
							break;
						}
						for (int num7 = 1; num7 < instance.playerdata.Length; num7++)
						{
							instance.playerdata[num7].entity.StopForceMove(-1, false);
						}
						instance.overridefollower = !instance.overridefollower;
						break;
					}
					case Commands.Choicewave:
						textholder.AddComponent<PromptAnim>().SetUp(Convert.ToInt32(temp[1]), temp.Length > 2);
						break;
					case Commands.Spd:
					case Commands.Speed:
					{
						float num3 = Convert.ToSingle(temp[1]);
						speed = ((num3 != -1f) ? num3 : 0.02f);
						break;
					}
					case Commands.Color:
						colorindex = Convert.ToInt32(temp[1]);
						if (ndd != null)
						{
							ndd.GetComponent<MeshRenderer>().material.color = instance.textcolors[colorindex];
						}
						break;
					case Commands.Anim:
					{
						if (!(pausemenu == null))
						{
							break;
						}
						int animstate = ((!char.IsNumber(temp[2][0])) ? ((int)Enum.Parse(typeof(Animations), temp[2])) : Convert.ToInt32(temp[2]));
						EntityControl[] array17 = null;
						if (temp[1] == "this")
						{
							array17 = new EntityControl[1] { instance.tailtarget.GetComponent<EntityControl>() };
						}
						else if (!(temp[1] == "parent"))
						{
							array17 = ((temp[1] == "party") ? GetPartyEntities() : ((!(caller != null)) ? new EntityControl[1] { GetEntity(temp[1]) } : new EntityControl[1] { GetEntity(temp[1], caller.entity) }));
						}
						else
						{
							parent.GetComponent<EntityControl>();
						}
						if (array17 != null)
						{
							for (int num75 = 0; num75 < array17.Length; num75++)
							{
								array17[num75].changedstate = true;
								array17[num75].animstate = animstate;
							}
						}
						yield return null;
						break;
					}
					case Commands.Questprompt:
						questboardpromp = true;
						break;
					case Commands.Copyvar:
						instance.flagvar[Convert.ToInt32(temp[2])] = Convert.ToInt32(temp[1]);
						break;
					case Commands.Addvar:
					{
						int num78 = GetValueFromString(temp[2]);
						bool flag6 = false;
						if (temp.Length > 3)
						{
							switch (temp[3])
							{
							case "-":
								num78 *= -1;
								break;
							case "*":
								flag6 = true;
								instance.flagvar[Convert.ToInt32(temp[1])] *= num78;
								break;
							case "/":
								flag6 = true;
								instance.flagvar[Convert.ToInt32(temp[1])] /= num78;
								break;
							}
						}
						if (!flag6)
						{
							instance.flagvar[Convert.ToInt32(temp[1])] += num78;
						}
						break;
					}
					case Commands.Scorecheck:
					{
						FadeIn();
						yield return EventControl.sec;
						map.RemoveLimit(true);
						SetCameraInstant(new Vector3(0f, 70f));
						bool a2 = MainCamera.GetComponent<FXAA>().enabled;
						MainCamera.GetComponent<FXAA>().enabled = true;
						SetRenderTexture(2);
						SpriteRenderer t2 = NewSolidColor("score", Color.Lerp(Color.white, Color.black, 0.75f), 0.01f, new Vector3(0f, 70f, 3f), new Vector2(0.5f, 0.5f));
						instance.StartCoroutine(SetText(FlappyBee.args + menutext[237], 1, null, false, true, new Vector3(0f, 4.8f), Vector3.one, Vector3.one * 2f, t2.transform, null));
						Transform tt = new GameObject().transform;
						tt.parent = t2.transform;
						FadeOut();
						yield return EventControl.sec;
						float y2 = 2f;
						for (int redirect = 0; redirect < 2; redirect++)
						{
							instance.StartCoroutine(SetText(FlappyBee.args + "|center|" + menutext[210 + redirect], 1, null, false, true, new Vector3(-7f, y2, -3f), Vector3.one, new Vector2(1.5f, 2f), t2.transform, null));
							yield return EventControl.halfsec;
							instance.StartCoroutine(SetText(FlappyBee.args + "|center|" + instance.flagvar[28 + redirect].ToString().PadLeft(5, '0'), 1, null, false, true, new Vector3(4f, y2, -3f), Vector3.one, Vector3.one * 2f, t2.transform, null));
							yield return EventControl.halfsec;
							y2 -= 1.65f;
						}
						instance.StartCoroutine(SetText(FlappyBee.args + menutext[238], 1, null, false, true, new Vector3(0f, 0f, -3f), Vector3.one, new Vector2(1.5f, 2f), tt.transform, null));
						yield return EventControl.halfsec;
						while (!GetKey(4) && !GetKey(5))
						{
							tt.transform.localPosition = new Vector3(0f, (Mathf.Sin(Time.time * 10f) > 0f) ? (-2) : 999);
							yield return null;
						}
						tt.transform.position = new Vector3(0f, 999f);
						FadeIn();
						yield return EventControl.sec;
						SetRenderTexture(0);
						MainCamera.GetComponent<FXAA>().enabled = a2;
						UnityEngine.Object.Destroy(t2.gameObject);
						map.RestoreLimit(true);
						ResetCamera(true);
						yield return null;
						FadeOut();
						yield return EventControl.sec;
						break;
					}
					case Commands.DungeonGame:
					case Commands.BeeGame:
					case Commands.PartyGame:
					{
						textbox.GetComponent<DialogueAnim>().shrink = true;
						Color c = RenderSettings.fogColor;
						FadeIn();
						string music = MainManager.music[0].clip.name;
						yield return new WaitForSeconds(1f);
						Vector3[] ml = new Vector3[2] { map.camlimitneg, map.camlimitpos };
						map.camlimitneg = Vector3.one * -999f;
						map.camlimitpos = Vector3.one * 999f;
						Transform tt = null;
						switch (com)
						{
						case Commands.DungeonGame:
							tt = new GameObject().AddComponent<MazeGame>().transform;
							instance.flagvar[6] = 1;
							break;
						case Commands.BeeGame:
							tt = new GameObject().AddComponent<FlappyBee>().transform;
							instance.flagvar[6] = 0;
							break;
						}
						tt.position = new Vector3(0f, 70f);
						instance.camspeed = 1f;
						yield return null;
						instance.camspeed = 0.1f;
						yield return null;
						while (tt != null)
						{
							yield return null;
						}
						map.camlimitneg = ml[0];
						map.camlimitpos = ml[1];
						ResetCamera(true);
						RenderSettings.fogEndDistance = map.fogend;
						RenderSettings.fogColor = c;
						ChangeMusic(music);
						FadeOut();
						yield return EventControl.sec;
						instance.skiptext = false;
						textbox.GetComponent<DialogueAnim>().shrink = false;
						if (instance.blinker == null)
						{
							instance.blinker = textbox.transform.GetChild(1).GetComponent<SpriteRenderer>();
						}
						break;
					}
					case Commands.Cardbattle:
					{
						CardGame cardGame = MainCamera.gameObject.AddComponent<CardGame>();
						Transform tt = instance.texttail;
						int opponentanimid = ((temp.Length == 1 || temp[1] == "this") ? (caller.entity.originalid + 1) : Convert.ToInt32(temp[1]));
						int[] opponentdeck = null;
						if (temp.Length > 3)
						{
							if (temp[3][0] == '@')
							{
								opponentdeck = CardGame.pdecks[Convert.ToInt32(temp[3].Replace("@", ""))];
							}
							else
							{
								List<int> list4 = new List<int>();
								string[] array12 = temp[3].Split('@');
								for (int num61 = 0; num61 < array12.Length; num61++)
								{
									list4.Add(Convert.ToInt32(array12[num61]));
								}
								opponentdeck = list4.ToArray();
							}
						}
						int mapid = ((temp.Length > 2) ? Convert.ToInt32(temp[2]) : (-1));
						cardGame.StartCoroutine(cardGame.StartCard(opponentanimid, mapid, opponentdeck));
						textbox.GetComponent<DialogueAnim>().shrink = true;
						instance.message = false;
						yield return new WaitForSeconds(1f);
						while (instance.cardgame != null)
						{
							yield return null;
						}
						instance.message = true;
						instance.texttail = tt;
						textbox.GetComponent<DialogueAnim>().shrink = false;
						if (instance.blinker == null)
						{
							instance.blinker = textbox.transform.GetChild(1).GetComponent<SpriteRenderer>();
						}
						break;
					}
					case Commands.Prompt:
					{
						if (temp[1] == "yesno")
						{
							command = "prompt,map,0.5,2," + temp[2] + "," + temp[3] + ",@" + menutext[5] + ",@" + menutext[6];
							if (temp.Length > 4)
							{
								command = command + "," + temp[4];
							}
							temp = command.Split(',');
						}
						float num34 = 0f;
						float num35 = 0f;
						string[] array6 = temp[2].Split(';');
						for (int num36 = 0; num36 < array6.Length; num36++)
						{
							switch (array6[num36][0])
							{
							case '$':
								num34 += Convert.ToSingle(array6[num36].Replace("$", ""));
								break;
							case 'x':
								num35 += Convert.ToSingle(array6[num36].Replace("x", ""));
								break;
							}
						}
						listcanceled = false;
						instance.prompt = true;
						instance.numberprompt = false;
						instance.option = 0;
						instance.maxoptions = Convert.ToInt32(temp[3]);
						float num37 = 0f;
						string[] array7 = new string[instance.maxoptions];
						bool flag3 = temp[1] == "card";
						promptmenu = temp[1] == "main" || temp[1] == "menu";
						for (int num38 = 0; num38 < instance.maxoptions; num38++)
						{
							if (temp[4 + instance.maxoptions + num38][0] == '@')
							{
								array7[num38] = temp[4 + instance.maxoptions + num38].Remove(0, 1).Replace("}", ",").Replace("{", "|");
							}
							else if (flag3)
							{
								array7[num38] = instance.cardgame.carddiag[Convert.ToInt32(temp[4 + instance.maxoptions + num38])];
							}
							else
							{
								array7[num38] = GetText(promptmenu, Convert.ToInt32(temp[4 + instance.maxoptions + num38]));
							}
							float textLenght = GetTextLenght(array7[num38], fonttype);
							if (textLenght > num37)
							{
								num37 = textLenght;
							}
						}
						if (temp[temp.Length - 1] == "none" || temp[temp.Length - 1] == "-1")
						{
							listcancel = -1;
						}
						else if (temp[temp.Length - 1][0] == '$')
						{
							listcancel = Convert.ToInt32(temp[temp.Length - 1].Replace("$", ""));
						}
						else
						{
							listcancel = instance.maxoptions - 1;
						}
						num37 /= 2.5f;
						num37 += 0.1f;
						if (num35 > num37)
						{
							num37 = num35;
						}
						instance.promptbox = Create9Box(new Vector3(0f, -0.6f * (float)instance.maxoptions + ((instance.maxoptions == 5) ? 0.65f : ((float)((instance.maxoptions == 6) ? 1 : 0))) + num34, 10f), new Vector2(Mathf.Clamp(num37 * 2.1f + 0.75f, 2f, float.PositiveInfinity), 0.5f + (float)instance.maxoptions), 0, -5, (textbox == null) ? Color.white : textbox.GetComponent<SpriteRenderer>().color, true);
						float num39 = 0.25f + 0.5f * (float)(instance.maxoptions - 2);
						if (instance.maxoptions > 3 && battle == null)
						{
							instance.promptbox.localPosition += new Vector3(instance.promptbox.localPosition.x, 0.1f * (float)instance.maxoptions);
						}
						instance.promptpointers = new int[instance.maxoptions];
						CreateCursor(instance.promptbox);
						for (int num40 = 0; num40 < instance.maxoptions; num40++)
						{
							instance.StartCoroutine(SetText(array7[num40] + "|choicewave," + num40 + "|", 0, null, false, false, new Vector3(0f - num37 - 0.1f, num39, 0f), Vector3.zero, new Vector3(0.85f, 0.85f, 1f), instance.promptbox, null));
							instance.promptpointers[num40] = Convert.ToInt32(Convert.ToInt32(temp[4 + num40]));
							num39 -= 1f;
						}
						instance.cursor.transform.localPosition = new Vector3(instance.promptbox.GetChild(1).localPosition.x - 0.25f, instance.promptbox.GetChild(1).localPosition.y + 0.25f, 10f);
						instance.inputcooldown = 5f;
						break;
					}
					case Commands.LetterPrompt:
						instance.inputcooldown = 5f;
						instance.flagstring[2] = command;
						instance.flagvar[5] = -1;
						textbox.GetComponent<DialogueAnim>().shrink = true;
						CreateLetterPrompt(-1, textbox.GetComponent<SpriteRenderer>().color);
						break;
					case Commands.NumberPrompt:
					{
						instance.letterprompt = -1;
						listcanceled = false;
						instance.inputcooldown = 5f;
						if (instance.flagvar[0] == -555)
						{
							instance.flagvar[0] = 0;
						}
						instance.prompt = true;
						instance.flagvar[5] = -1;
						instance.numberprompt = true;
						instance.flagstring[0] = "";
						instance.option = 0;
						instance.maxoptions = 11;
						instance.promptbox = Create9Box(new Vector3(0f, -1.2f, 10f), new Vector2(7f, 3f), 0, -3, (textbox == null) ? Color.white : textbox.GetComponent<SpriteRenderer>().color, true);
						listtype = Convert.ToInt32(temp[1]);
						instance.flagvar[10] = Convert.ToInt32(temp[2]);
						listredirect = Convert.ToInt32(temp[3]);
						listcancel = Convert.ToInt32(temp[4]);
						instance.npromptholder = new GameObject("number holder").transform;
						instance.npromptholder.transform.parent = instance.promptbox.transform;
						instance.npromptholder.transform.localPosition = new Vector3(0f, 0.5f, 0.05f);
						instance.npromptholder.transform.localEulerAngles = Vector3.zero;
						instance.npromptholder.transform.localScale = Vector3.one;
						float num24 = -2.25f;
						float num25 = -0.25f;
						for (int num26 = 0; num26 < instance.maxoptions + 1; num26++)
						{
							string text5 = num26 + "|choicewave," + num26 + ",true|";
							switch (num26)
							{
							case 10:
							{
								string text6 = "";
								if (languageid == 6)
								{
									text6 = "|sizemulti,0.8,1|";
								}
								text5 = text6 + menutext[42] + "|choicewave," + num26 + "|";
								break;
							}
							case 11:
								text5 = menutext[74] + "|choicewave," + num26 + "|";
								break;
							}
							instance.StartCoroutine(SetText("|center|" + text5, 0, null, false, false, new Vector3(num24, num25, 0f), Vector3.zero, new Vector3(0.85f, 0.85f, 1f), instance.promptbox, null));
							num24 += 0.5f;
							if (num26 == 9)
							{
								num25 -= 0.75f;
								num24 = -1.5f;
							}
							else if (num26 > 9)
							{
								num24 = 1.5f;
							}
						}
						instance.RefreshNumberPrompt();
						break;
					}
					case Commands.Sort:
						sort = Convert.ToInt32(temp[1]);
						if (ndd != null)
						{
							ndd.GetComponent<MeshRenderer>().sortingOrder = sort;
						}
						break;
					case Commands.Save:
						roomtransition = true;
						yield return null;
						if (Save(caller.vectordata[0]))
						{
							PlaySound("AtkSuccess", -1, 1f, 0.6f);
							text = OrganizeLines("|blank|" + menutext[8] + "|stopskip|", linebreak.Value, size.x, fonttype);
						}
						else
						{
							text = OrganizeLines("|blank|" + menutext[9] + "|stopskip|", linebreak.Value, size.x, fonttype);
						}
						yield return null;
						roomtransition = false;
						k = -1;
						skipi = true;
						break;
					case Commands.Parent:
						caller = GetEntity(Convert.ToInt32(temp[1])).npcdata;
						break;
					case Commands.Tail:
					case Commands.Tailextra:
					case Commands.Gettail:
					{
						if (testing)
						{
							break;
						}
						ResetDiag(false);
						Transform transform = instance.tailtarget;
						if (instance.tailtarget != null)
						{
							instance.tailtarget.GetComponent<EntityControl>().talking = false;
						}
						bool a2 = temp.Length > 2 && temp[2] == "instant";
						EntityControl temptail = null;
						if (temp[1] == "caller" && caller != null)
						{
							temptail = caller.entity;
						}
						else if (temp[1] == "parent" && parent != null)
						{
							temptail = parent.GetComponent<EntityControl>();
						}
						else if (temp[1] != "null")
						{
							if (com != Commands.Gettail)
							{
								temptail = ((com == Commands.Tailextra && map != null) ? map.tempfollowers[Convert.ToInt32(temp[1])] : ((!(caller != null)) ? GetEntity(temp[1]) : GetEntity(temp[1], caller.entity)));
							}
							else
							{
								int num28 = Convert.ToInt32(temp[1]);
								if (num28 <= 2 && HasPlayer(num28))
								{
									temptail = GetEntity(-(4 + num28));
								}
								else
								{
									EntityControl[] array5 = UnityEngine.Object.FindObjectsOfType<EntityControl>();
									for (int num29 = 0; num29 < array5.Length; num29++)
									{
										if (array5[num29].originalid == num28)
										{
											temptail = array5[num29];
											break;
										}
									}
								}
							}
						}
						else
						{
							instance.tailtarget = null;
						}
						if (!a2 && (temptail == null || temptail.transform != transform))
						{
							textbox.GetComponent<DialogueAnim>().shrink = true;
							yield return new WaitForSeconds(0.035f);
						}
						if (temptail != null)
						{
							instance.tailtarget = temptail.transform;
						}
						yield return null;
						if (temptail != null)
						{
							bleep = Resources.Load<AudioClip>("Audio/Sounds/Dialogue/Dialogue" + temptail.dialoguebleepid);
							bleeppitch = temptail.bleeppitch;
						}
						else
						{
							bleep = null;
						}
						yield return null;
						if (!a2)
						{
							textbox.GetComponent<DialogueAnim>().shrink = false;
							if (temp.Length != 3)
							{
								break;
							}
							goto case Commands.Anim;
						}
						break;
					}
					case Commands.Flag:
						if (temp.Length > 3)
						{
							instance.flags[instance.flagvar[Convert.ToInt32(temp[2])]] = Convert.ToBoolean(temp[3]);
						}
						else
						{
							instance.flags[Convert.ToInt32(temp[1])] = Convert.ToBoolean(temp[2]);
						}
						break;
					case Commands.Mapflag:
						if (temp.Length > 3)
						{
							map.mapflags[instance.flagvar[Convert.ToInt32(temp[2])]] = Convert.ToBoolean(temp[3]);
						}
						else
						{
							map.mapflags[Convert.ToInt32(temp[1])] = Convert.ToBoolean(temp[2]);
						}
						break;
					case Commands.Regionalflag:
						instance.regionalflags[Convert.ToInt32(temp[1])] = Convert.ToBoolean(temp[2]);
						break;
					case Commands.Checkmoney:
					{
						int num8 = 2;
						int num9;
						if (temp[1].Contains("var"))
						{
							num9 = instance.flagvar[Convert.ToInt32(temp[2])];
							num8 = 3;
						}
						else
						{
							num9 = Convert.ToInt32(temp[1]);
						}
						if (instance.money < num9)
						{
							text = "|blank|" + OrganizeLines(GetDialogueText(Convert.ToInt32(temp[num8])), linebreak.Value, size.x, fonttype);
							k = -1;
							skipi = true;
						}
						break;
					}
					case Commands.Checkinvqtd:
						if (temp[2] == "full")
						{
							int num81 = Convert.ToInt32(temp[1]);
							if ((num81 == 0 && instance.items[num81].Count >= instance.maxitems) || (num81 == 2 && instance.items[num81].Count >= instance.maxstorage))
							{
								text = OrganizeLines("|blank|" + GetDialogueText(Convert.ToInt32(temp[3])), linebreak.Value, size.x, fonttype);
								k = -1;
								skipi = true;
							}
						}
						else if (temp[2] != "full" && instance.items[Convert.ToInt32(temp[1])].Count <= Convert.ToInt32(temp[2]))
						{
							text = OrganizeLines("|blank|" + GetDialogueText(Convert.ToInt32(temp[3])), linebreak.Value, size.x, fonttype);
							k = -1;
							skipi = true;
						}
						break;
					case Commands.Additemtoss:
					{
						int num72 = Convert.ToInt32(temp[1]);
						int num73 = ((!temp[2].Contains("var")) ? Convert.ToInt32(temp[2]) : instance.flagvar[Convert.ToInt32(temp[3])]);
						if ((num72 == 0 && instance.items[0].Count + 1 <= instance.maxitems) || num72 != 0)
						{
							if (num72 != 2 && num72 != 3)
							{
								instance.items[num72].Add(num73);
							}
							else if (num72 == 2)
							{
								instance.badges.Add(new int[2] { num73, -2 });
							}
							if (caller != null)
							{
								caller.DestroyDescWindow();
							}
							goto case Commands.End;
						}
						if (caller != null)
						{
							caller.hit = false;
							caller.DestroyDescWindow();
							if (caller.data.Length > 1 && caller.data[1] > -1)
							{
								eventtoss = caller.data[1];
							}
						}
						text = "|blank||boxstyle,4||spd,0|" + menutext[3] + "|pickitem,0,1,true,false,-2,-3,5|";
						instance.blinker.enabled = false;
						k = -1;
						skipi = true;
						break;
					}
					case Commands.Additem:
						if (MultiItem())
						{
							int[] array13 = new int[2]
							{
								(listtype == 0) ? 2 : 0,
								(listtype != 0) ? 2 : 0
							};
							int[] array14 = instance.multiselect.OrderBy((int result) => result).ToArray();
							for (int num62 = array14.Length - 1; num62 >= 0; num62--)
							{
								instance.items[array13[0]].Add(instance.items[array13[1]][array14[num62]]);
								instance.items[array13[1]].RemoveAt(array14[num62]);
							}
							instance.multiselect = new List<int>();
						}
						else
						{
							int item = ((!temp[2].Contains("var")) ? Convert.ToInt32(temp[2]) : instance.flagvar[Convert.ToInt32(temp[3])]);
							instance.items[Convert.ToInt32(temp[1])].Add(item);
						}
						break;
					case Commands.Money:
						if (temp[1] == "var")
						{
							if (temp[2][0] == '-')
							{
								instance.money -= instance.flagvar[Convert.ToInt32(temp[3])];
							}
							else
							{
								instance.money += instance.flagvar[Convert.ToInt32(temp[2])];
							}
						}
						else
						{
							instance.money += Convert.ToInt32(temp[1]);
						}
						instance.money = Mathf.Clamp(instance.money, 0, 999);
						break;
					case Commands.Heal:
						if (temp.Length > 1 && temp[1] == "tp")
						{
							Heal(new Healing[1] { Healing.TPOnly }, instance.partyorder, true, true);
							break;
						}
						Heal();
						if (temp.Length > 1)
						{
							instance.hudcooldown = 0f;
						}
						break;
					case Commands.Termacadecheck:
						if (instance.flagvar[28] > 9500 && instance.flagvar[29] > 4500)
						{
							temp = new string[2]
							{
								"",
								temp[2]
							};
						}
						else
						{
							if (instance.flagvar[28] <= 9500 && instance.flagvar[29] <= 4500)
							{
								break;
							}
							temp = new string[2]
							{
								"",
								temp[1]
							};
						}
						goto case Commands.Goto;
					case Commands.Caravanmedal:
						if (PrizeBadges(true).Length <= 1)
						{
							break;
						}
						temp = new string[3]
						{
							"",
							temp[1],
							"keep"
						};
						goto case Commands.Goto;
					case Commands.GetFromMap:
						text = OrganizeLines("|blank|" + GetDialogueFromMap((Maps)Convert.ToInt32(temp[1]), Convert.ToInt32(temp[2]), -1), linebreak.Value, size.x, fonttype);
						GlobalCommand(ref text);
						k = -1;
						skipi = true;
						break;
					case Commands.Goto:
						if (temp[1] == "random")
						{
							text = OrganizeLines("|blank|" + GetDialogueText(Convert.ToInt32(temp[UnityEngine.Random.Range(2, temp.Length)])), linebreak.Value, size.x, fonttype);
							if (currentdialogue == diagstring.Count && !backtracking)
							{
								currentdialogue++;
								diagstring.Add(OrganizeLines(tempdiag, linebreak.Value, size.x, fonttype));
								tempdiag = "|size," + size.x + "," + size.y + "|";
							}
						}
						else
						{
							string text11 = "";
							if (temp.Length > 2)
							{
								for (int num71 = 2; num71 < temp.Length; num71++)
								{
									text11 = text11 + "|" + temp[num71] + "|";
								}
							}
							string text12 = ((text11 == "|keep|") ? "" : "|blank|");
							text = text12 + OrganizeLines(GetDialogueText(Convert.ToInt32(temp[1])) + text11, linebreak.Value, size.x, fonttype);
							if (currentdialogue == diagstring.Count && !backtracking)
							{
								currentdialogue++;
								diagstring.Add(OrganizeLines(tempdiag, linebreak.Value, size.x, fonttype));
								tempdiag = "|size," + size.x + "," + size.y + "|";
							}
						}
						GlobalCommand(ref text);
						k = -1;
						skipi = true;
						break;
					case Commands.Plural:
						text = ((GetValueFromString(temp[1]) != 1) ? text.Replace("|" + command + "|", temp[3]) : text.Replace("|" + command + "|", temp[2]));
						k--;
						skipi = true;
						break;
					case Commands.String:
					case Commands.Sstring:
					case Commands.Menu:
					case Commands.Call:
					{
						string text8;
						switch (com)
						{
						case Commands.Call:
							text8 = GetDialogueText(Convert.ToInt32(temp[1]));
							break;
						case Commands.Menu:
							text8 = menutext[Convert.ToInt32(temp[1])];
							break;
						default:
							text8 = instance.flagstring[Convert.ToInt32(temp[1])];
							break;
						}
						text8 = text8.Replace("\r\n", "\n");
						if (temp.Length > 2 && temp[2] == "clamp" && GetTextLenght(text8, fonttype) > Convert.ToSingle(temp[3]))
						{
							text8 = "|sizemulti," + ((temp.Length > 4) ? string.Concat(Convert.ToSingle(temp[4])) : "0.5") + ",1|" + text8 + "|size," + size.x + "," + size.y + "|";
						}
						text = text.Replace("|" + command + "|", text8);
						if (temp.Length > 2 && temp[2] == "true")
						{
							text = OrganizeLines(text, linebreak.Value, size.x, fonttype);
						}
						k--;
						skipi = true;
						break;
					}
					case Commands.Anstring:
					{
						int num41 = (temp[1].Contains("var") ? instance.flagvar[Convert.ToInt32(temp[1].Replace("var", ""))] : Convert.ToInt32(temp[1]));
						int num42 = ((temp.Length > 2) ? Convert.ToInt32(temp[2]) : 0);
						text = text.Replace("|" + command + "|", (num42 == 0) ? itemdata[0, num41, 3] : badgedata[num41, 6]);
						k--;
						skipi = true;
						break;
					}
					case Commands.Var:
						if (temp.Length == 2)
						{
							string newValue3 = instance.flagvar[Convert.ToInt32(temp[1])].ToString();
							text = text.Replace("|" + command + "|", newValue3);
							k--;
							skipi = true;
						}
						else if (temp[2] == "pad")
						{
							string newValue4 = instance.flagvar[Convert.ToInt32(temp[1])].ToString().PadLeft(Convert.ToInt32(temp[3]), (temp.Length > 4) ? temp[4][0] : '0');
							text = text.Replace("|" + command + "|", newValue4);
							k--;
							skipi = true;
						}
						else
						{
							instance.flagvar[Convert.ToInt32(temp[1])] = GetValueFromString(temp[2]);
						}
						break;
					case Commands.Maxmedals:
						text = text.Replace("|" + command + "|", string.Concat(120));
						k--;
						skipi = true;
						break;
					case Commands.Medaltotal:
						text7 = instance.badges.Count.ToString();
						goto IL_6b47;
					case Commands.Cberrytotal:
						text7 = CrystalBerryAmmount().ToString();
						goto IL_6b47;
					case Commands.Flagvalue:
					{
						string newValue = ((temp.Length <= 2) ? instance.flags[Convert.ToInt32(temp[1])].ToString() : instance.flags[instance.flagvar[Convert.ToInt32(temp[2])]].ToString());
						text = text.Replace("|" + command + "|", newValue);
						k--;
						skipi = true;
						break;
					}
					case Commands.Currency:
					{
						string text2 = ((!(temp[1] == "var")) ? temp[1] : instance.flagvar[Convert.ToInt32(temp[2])].ToString());
						if (languageid == 6)
						{
							string text3 = text2;
							if (text2.Length > 2)
							{
								text3 = text2[text2.Length - 2].ToString() + text2[text2.Length - 1];
							}
							switch (text3)
							{
							case "11":
							case "12":
							case "13":
							case "14":
								text2 = text2 + " " + menutext[275];
								break;
							default:
								switch (text3[text3.Length - 1])
								{
								case '1':
									text2 = text2 + " " + menutext[277];
									break;
								case '2':
								case '3':
								case '4':
									text2 = text2 + " " + menutext[278];
									break;
								default:
									text2 = text2 + " " + menutext[275];
									break;
								}
								break;
							}
						}
						else
						{
							text2 = text2 + " " + menutext[Convert.ToInt32(Convert.ToInt32(text2) != 1)];
						}
						text = text.Replace("|" + command + "|", text2);
						k--;
						skipi = true;
						break;
					}
					case Commands.Kill:
					{
						EntityControl entity = GetEntity(temp[1], caller.entity);
						if (entity.npcdata != null && (entity.npcdata.interacttype == NPCControl.Interaction.Shop || entity.npcdata.interacttype == NPCControl.Interaction.CaravanBadge) && entity.animid == 2)
						{
							if (entity.npcdata.interacttype == NPCControl.Interaction.CaravanBadge)
							{
								entity.npcdata.SetBadgeShop(true);
							}
							else
							{
								entity.npcdata.shopkeeper.SetBadgeShop(true);
							}
						}
						if (entity.npcdata != null && entity.npcdata.interacttype != NPCControl.Interaction.CaravanBadge)
						{
							entity.StartCoroutine(entity.Death(true));
						}
						if (player != null)
						{
							player.npc = new List<NPCControl>();
						}
						break;
					}
					case Commands.Rerollshops:
						if (caller != null)
						{
							caller.SetBadgeShop(true);
						}
						break;
					case Commands.Position:
						textholder.transform.localPosition = new Vector3(textholder.transform.localPosition.x, Convert.ToSingle(temp[1]), textholder.transform.localPosition.z);
						break;
					case Commands.Boxstyle:
					{
						SpriteRenderer component = textbox.GetComponent<SpriteRenderer>();
						if (temp[1] == "-1")
						{
							component.enabled = false;
							instance.tailtarget = null;
						}
						else if (windowstyle != null)
						{
							windowstyle.Play(temp[1]);
						}
						if (temp.Length > 2)
						{
							component.sortingOrder = Convert.ToInt32(temp[2]);
						}
						break;
					}
					case Commands.Boxspeed:
						textbox.GetComponent<DialogueAnim>().shrinkspeed = Convert.ToSingle(temp[1]);
						break;
					case Commands.Pickitem:
						instance.inputcooldown = 5f;
						instance.multiselect = new List<int>();
						if (battle == null)
						{
							instance.inputcooldown = 5f;
						}
						if (temp.Length == 4)
						{
							storeid = 0;
							listtype = Convert.ToInt32(temp[1]);
							listammount = 5;
							listcancel = Convert.ToInt32(temp[3]);
							listredirect = Convert.ToInt32(temp[2]);
							listdesc = true;
							listsell = false;
						}
						else
						{
							storeid = Convert.ToInt32(temp[2]);
							listcancel = Convert.ToInt32(temp[6]);
							listredirect = Convert.ToInt32(temp[5]);
							if (temp.Length < 8)
							{
								listammount = 5;
							}
							else
							{
								listammount = Convert.ToInt32(temp[7]);
							}
							listtype = Convert.ToInt32(temp[1]);
							listdesc = Convert.ToBoolean(temp[3]);
							listsell = Convert.ToBoolean(temp[4]);
						}
						ShowItemList(listtype, defaultlistpos, listdesc, listsell);
						break;
					case Commands.Clonestring:
						instance.flagstring[Convert.ToInt32(temp[2])] = instance.flagstring[Convert.ToInt32(temp[1])];
						break;
					case Commands.Checkmapflag:
						if (temp.Length > 3)
						{
							Convert.ToBoolean(temp[3]);
						}
						if (map.mapflags[Convert.ToInt32(temp[1])])
						{
							text = OrganizeLines(GetDialogueText(Convert.ToInt32(temp[2])), linebreak.Value, size.x, fonttype);
							instance.skiptext = false;
							skipi = true;
							k = -1;
						}
						break;
					case Commands.Checkpos:
						if (player != null)
						{
							bool flag7 = false;
							switch (temp[1])
							{
							case "x":
								flag7 = player.transform.position.x >= Convert.ToSingle(temp[2]);
								break;
							case "y":
								flag7 = player.transform.position.y >= Convert.ToSingle(temp[2]);
								break;
							case "z":
								flag7 = player.transform.position.z >= Convert.ToSingle(temp[2]);
								break;
							}
							if (!flag7)
							{
								text = OrganizeLines(GetDialogueText(Convert.ToInt32(temp[3])), linebreak.Value, size.x, fonttype);
								instance.skiptext = false;
								skipi = true;
								k = -1;
							}
						}
						break;
					case Commands.Checkminibubble:
						if (UnityEngine.Object.FindObjectsOfType<MiniBubble>().Length != 0)
						{
							text = OrganizeLines(GetDialogueText(Convert.ToInt32(temp[1])), linebreak.Value, size.x, fonttype);
							instance.skiptext = false;
							skipi = true;
							k = -1;
						}
						break;
					case Commands.Checkanim:
						if (caller != null)
						{
							EntityControl entity9 = GetEntity(temp[1], caller.entity);
							bool num76 = temp[2][0] == '!';
							int num77 = Convert.ToInt32(temp[2].Replace("!", ""));
							if ((num76 && entity9.animstate != num77) || entity9.animstate == num77)
							{
								text = OrganizeLines(GetDialogueText(Convert.ToInt32(temp[3])), linebreak.Value, size.x, fonttype);
								instance.skiptext = false;
								skipi = true;
								k = -1;
							}
						}
						break;
					case Commands.Checktrue:
					case Commands.Checkflag:
					case Commands.Battlewon:
					{
						if (temp[1] == "var")
						{
							if (instance.flagvar[Convert.ToInt32(temp[2])] != Convert.ToInt32(temp[3]))
							{
								text = OrganizeLines(GetDialogueText(Convert.ToInt32(temp[4])), linebreak.Value, size.x, fonttype);
								instance.skiptext = false;
								skipi = true;
								k = -1;
							}
							break;
						}
						bool flag5 = false;
						string[] array15 = temp[1].Split('@');
						if (array15.Length > 1)
						{
							bool[] array16 = new bool[array15.Length];
							for (int num63 = 0; num63 < array15.Length; num63++)
							{
								int num64 = Convert.ToInt32(array15[num63]);
								array16[num63] = instance.flags[Mathf.Abs(num64)] == num64 > 0;
							}
							flag5 = CheckAllBool(array16, com == Commands.Checktrue);
						}
						if (flag5 || (array15.Length == 1 && ((com == Commands.Battlewon && !battleresult) || (com != Commands.Battlewon && (instance.flags[Convert.ToInt32(temp[1])] ? 1 : 0) == ((com != Commands.Checkflag) ? 1 : 0)))))
						{
							text = OrganizeLines(GetDialogueText(Convert.ToInt32(temp[(com == Commands.Battlewon) ? 1 : 2])), linebreak.Value, size.x, fonttype);
							instance.skiptext = false;
							skipi = true;
							k = -1;
						}
						break;
					}
					case Commands.Entityalive:
					{
						EntityControl entity6 = GetEntity(Convert.ToInt32(temp[1]));
						if (entity6 != null && entity6.gameObject.activeInHierarchy)
						{
							text = OrganizeLines(GetDialogueText(Convert.ToInt32(temp[2])), linebreak.Value, size.x, fonttype);
							instance.skiptext = false;
							skipi = true;
							k = -1;
						}
						break;
					}
					case Commands.Removeitemat:
						if (!MultiItem())
						{
							instance.items[IntFromString(temp[1])].RemoveAt((temp.Length == 2 || temp[2] == "opt") ? listoption : IntFromString(temp[2]));
						}
						break;
					case Commands.Removeitem:
						if (!MultiItem())
						{
							if (temp[1] == "3")
							{
								int[][] array10 = instance.badges.ToArray();
								for (int num53 = 0; num53 < instance.badges.Count; num53++)
								{
									if (array10[num53][0] == ((temp[2] == "var") ? instance.flagvar[Convert.ToInt32(temp[3])] : Convert.ToInt32(temp[2])))
									{
										instance.badges.RemoveAt(num53);
										break;
									}
								}
								break;
							}
							int[] array11 = new int[temp.Length - 1];
							for (int num54 = 1; num54 < temp.Length; num54++)
							{
								if (temp[num54].Contains("var"))
								{
									array11[num54 - 1] = instance.flagvar[Convert.ToInt32(temp[num54 + 1])];
								}
								else
								{
									array11[num54 - 1] = Convert.ToInt32(temp[num54]);
								}
							}
							instance.items[array11[0]].Remove(array11[1]);
						}
						else if (listsell)
						{
							instance.multiselect = instance.multiselect.OrderBy((int result) => result).ToList();
							for (int num55 = instance.multiselect.Count - 1; num55 >= 0; num55--)
							{
								instance.items[0].RemoveAt(instance.multiselect[num55]);
							}
							instance.multiselect = new List<int>();
						}
						break;
					case Commands.Getstorage:
						text = text.Replace("|" + command + "|", (instance.maxstorage - instance.items[2].Count).ToString());
						k--;
						skipi = true;
						break;
					case Commands.Button:
					{
						ButtonSprite buttonSprite = new GameObject(command).AddComponent<ButtonSprite>();
						int num47 = -1;
						if (temp.Length >= 3)
						{
							num47 = Convert.ToInt32(temp[2]);
						}
						string description = null;
						if (temp.Length >= 4)
						{
							description = temp[3];
						}
						int num48 = Convert.ToInt32(temp[1]);
						float num49 = 0.25f;
						bool flag4 = false;
						if (num47 != 1 && !InputIO.IsConsole && InputIO.LongButton(num48))
						{
							num49 += 0.3f;
							flag4 = true;
						}
						buttonSprite.tridimentional = tridimensional;
						buttonSprite.SetUp(num48, num47, description, new Vector2(currentoffset + num49, currentline + 0.225f * size.y), new Vector3(size.x / 2.2f, size.y / 2.2f, 1f), sort, null);
						if (!tridimensional)
						{
							buttonSprite.gameObject.layer = layer;
							buttonSprite.layer = layer;
						}
						buttonSprite.transform.parent = textholder.transform;
						buttonSprite.transform.localEulerAngles = Vector3.zero;
						buts.Add(buttonSprite.gameObject);
						currentoffset += 0.7f;
						if (flag4)
						{
							currentoffset += 0.7f;
						}
						if ((speed > 0f && !instance.skiptext) || noskip)
						{
							yield return new WaitForSeconds(speed);
						}
						break;
					}
					case Commands.Movewait:
					{
						EntityControl temptail = GetEntity(temp[1], (caller != null) ? caller.entity : null);
						while (temptail.forcemove)
						{
							yield return null;
						}
						break;
					}
					case Commands.Move:
					case Commands.Moveahead:
					{
						EntityControl entity4 = GetEntity(caller, temp[1]);
						entity4.backsprite = false;
						float num33 = 0f;
						if (temp[3] != "null")
						{
							num33 = Convert.ToSingle(temp[3]);
						}
						else
						{
							entity4.ignorey = true;
						}
						float multiplier = 1f;
						int state = 1;
						int stopstate = 0;
						if (temp.Length > 5)
						{
							multiplier = Convert.ToSingle(temp[5]);
						}
						if (temp.Length > 6)
						{
							state = Convert.ToInt32(temp[6]);
						}
						if (temp.Length > 7)
						{
							stopstate = Convert.ToInt32(temp[7]);
						}
						entity4.MoveTowards(new Vector3(Convert.ToSingle(temp[2]), (com == Commands.Move) ? num33 : 0f, Convert.ToSingle(temp[4])) + ((com == Commands.Move) ? Vector3.zero : entity4.transform.position), multiplier, state, stopstate);
						break;
					}
					case Commands.Forcewait:
					case Commands.Fwait:
						if (pausemenu == null)
						{
							yield return new WaitForSeconds(Convert.ToSingle(temp[1]));
							instance.skiptext = false;
						}
						break;
					case Commands.Wait:
						if (!instance.skiptext || minibubble)
						{
							yield return new WaitForSeconds(Convert.ToSingle(temp[1]));
						}
						break;
					case Commands.Face:
					{
						if (temp[1] == "party")
						{
							for (int num27 = 0; num27 < instance.playerdata.Length; num27++)
							{
								if (caller != null)
								{
									instance.playerdata[num27].entity.FaceTowards(GetEntity(caller, temp[2]).transform.position);
								}
								else
								{
									instance.playerdata[num27].entity.FaceTowards(GetEntity(temp[2]).transform.position);
								}
							}
							break;
						}
						EntityControl entity2;
						EntityControl entity3;
						if (caller != null)
						{
							entity2 = GetEntity(temp[1], caller.entity);
							entity3 = GetEntity(temp[2], caller.entity);
						}
						else
						{
							entity2 = GetEntity(temp[1]);
							entity3 = GetEntity(temp[2]);
						}
						entity2.flip = entity3.transform.position.x > entity2.transform.position.x;
						if (temp.Length > 3 && temp[3] == "true")
						{
							entity3.FaceTowards(entity2.transform.position);
						}
						break;
					}
					case Commands.Camangle:
						instance.camangleoffset = new Vector3(Convert.ToSingle(temp[1]), Convert.ToSingle(temp[2]), Convert.ToSingle(temp[3]));
						break;
					case Commands.Camoffset:
						instance.camoffset = new Vector3(Convert.ToSingle(temp[1]), Convert.ToSingle(temp[2]), Convert.ToSingle(temp[3]));
						break;
					case Commands.Camspeed:
					{
						float num30 = Convert.ToSingle(temp[1]);
						if (num30 > 0f)
						{
							instance.camspeed = num30;
						}
						else
						{
							instance.camspeed = 0.1f;
						}
						break;
					}
					case Commands.Camtarget:
						if (temp.Length > 2)
						{
							instance.camtarget = null;
							instance.camtargetpos = new Vector3(Convert.ToSingle(temp[1]), Convert.ToSingle(temp[2]), Convert.ToSingle(temp[3]));
						}
						else if (temp[1] == "null")
						{
							instance.camtargetpos = null;
							instance.camtarget = null;
						}
						else
						{
							instance.camtargetpos = null;
							instance.camtarget = GetEntity(temp[1], (caller != null) ? caller.entity : null).transform;
						}
						break;
					case Commands.Savecamera:
						SaveCameraPosition(true);
						break;
					case Commands.Loadcamera:
						SaveCameraPosition(false);
						break;
					case Commands.Flip:
						if (temp[1] == "party")
						{
							bool? flag = null;
							if (temp.Length > 2)
							{
								flag = Convert.ToBoolean(temp[2]);
							}
							for (int num12 = 0; num12 < instance.playerdata.Length; num12++)
							{
								if (!flag.HasValue)
								{
									instance.playerdata[num12].entity.flip = !instance.playerdata[num12].entity.flip;
								}
								else
								{
									instance.playerdata[num12].entity.flip = flag.Value;
								}
							}
						}
						else
						{
							EntityControl entityControl = ((!(caller != null)) ? GetEntity(temp[1]) : GetEntity(temp[1], caller.entity));
							if (temp.Length == 2)
							{
								entityControl.flip = !entityControl.flip;
							}
							else
							{
								entityControl.flip = Convert.ToBoolean(temp[2]);
							}
						}
						break;
					case Commands.Warp:
					case Commands.Transfer:
						end = true;
						transferi = ((!temp[1].Contains("var")) ? Convert.ToInt32(temp[1]) : instance.flagvar[Convert.ToInt32(temp[1].Replace("var", ""))]);
						if (transferi >= Enum.GetNames(typeof(Maps)).Length)
						{
							transferi = 1;
						}
						transfer = ((temp.Length <= 2) ? new Vector3?(Vector3.zero) : new Vector3?(new Vector3(Convert.ToSingle(temp[2]), Convert.ToSingle(temp[3]), Convert.ToSingle(temp[4]))));
						break;
					case Commands.Loadmap:
						if (temp.Length == 1)
						{
							LoadMap();
						}
						else
						{
							LoadMap(Convert.ToInt32(temp[1]));
						}
						yield return null;
						break;
					case Commands.Checkallquests:
						if (instance.boardquests[2].Count >= 60)
						{
							text = OrganizeLines("|blank|" + GetDialogueText(Convert.ToInt32(temp[1])), linebreak.Value, size.x, fonttype);
							k = -1;
							instance.skiptext = false;
							skipi = true;
						}
						break;
					case Commands.Checkvar:
						if (temp[1] == "atleast")
						{
							if (instance.flagvar[Convert.ToInt32(temp[2])] < (temp[3].Contains("var") ? instance.flagvar[Convert.ToInt32(temp[3].Replace("var", ""))] : Convert.ToInt32(temp[3])))
							{
								text = OrganizeLines("|blank|" + GetDialogueText(Convert.ToInt32(temp[4])), linebreak.Value, size.x, fonttype);
								k = -1;
								instance.skiptext = false;
								skipi = true;
							}
						}
						else if (temp[1] == "moreand")
						{
							if (instance.flagvar[Convert.ToInt32(temp[2])] >= (temp[3].Contains("var") ? instance.flagvar[Convert.ToInt32(temp[3].Replace("var", ""))] : Convert.ToInt32(temp[3])))
							{
								text = OrganizeLines("|blank|" + GetDialogueText(Convert.ToInt32(temp[4])), linebreak.Value, size.x, fonttype);
								k = -1;
								instance.skiptext = false;
								skipi = true;
							}
						}
						else if (instance.flagvar[Convert.ToInt32(temp[1])] == Convert.ToInt32(temp[2]))
						{
							text = OrganizeLines("|blank|" + GetDialogueText(Convert.ToInt32(temp[3])), linebreak.Value, size.x, fonttype);
							k = -1;
							instance.skiptext = false;
							skipi = true;
						}
						break;
					case Commands.Common:
						text = OrganizeLines("|blank|" + commondialogue[Convert.ToInt32(temp[1])], linebreak.Value, size.x, fonttype);
						k = -1;
						instance.skiptext = false;
						skipi = true;
						break;
					case Commands.Fademusic:
						if (temp.Length == 1)
						{
							FadeMusic(0.025f);
						}
						else
						{
							FadeMusic(Convert.ToSingle(temp[1]));
						}
						break;
					case Commands.FadeIn:
					case Commands.FadeOut:
					{
						float y2 = 0.1f;
						int redirect = 0;
						int num74 = 0;
						if (temp.Length > 1)
						{
							y2 = Convert.ToSingle(temp[1]);
						}
						if (temp.Length > 2)
						{
							redirect = Convert.ToInt32(temp[2]);
						}
						if (temp.Length > 3 && char.IsNumber(temp[3][0]))
						{
							num74 = Convert.ToInt32(temp[3]);
						}
						PlayTransition((int)Enum.Parse(typeof(Transitions), temp[0], true), redirect, y2, (num74 == 0) ? Color.black : instance.textcolors[num74]);
						if (com == Commands.FadeIn && temp.Length > 3 && temp[3] == "kill" && caller != null)
						{
							caller.entity.talking = false;
							yield return new WaitForSeconds(0.75f + (1f - y2));
							UnityEngine.Object.Destroy(caller.gameObject);
							PlayTransition(1, redirect, y2, Color.black);
							yield return new WaitForSeconds(0.75f + (1f - y2));
						}
						break;
					}
					case Commands.Align:
					{
						if (!(caller != null))
						{
							break;
						}
						EntityControl temptail = ((map.chompy != null) ? map.chompy.following : null);
						bool a2 = temp[2] == "true";
						bool tmes = instance.overridefollower;
						bool flipback = false;
						instance.overridefollower = true;
						TeleportFollowers(2.5f, TPDir.Away, player.transform);
						float num56 = caller.entity.ccol.radius * 3f + 0.1f;
						if (temp.Length >= 4)
						{
							if (char.IsNumber(temp[3][0]))
							{
								num56 = Convert.ToSingle(temp[3]);
							}
							else
							{
								flipback = Convert.ToBoolean(temp[3]);
							}
						}
						Vector3 vector = caller.transform.position + instance.globalcamdir.right.normalized * num56;
						Vector3 vector2 = instance.globalcamdir.right.normalized;
						switch (temp[1])
						{
						case "left":
							vector = caller.transform.position - instance.globalcamdir.right.normalized * num56;
							vector2 = -instance.globalcamdir.right.normalized;
							break;
						case "near":
							if (!(player.entity.campos.x < caller.entity.campos.x))
							{
								break;
							}
							goto case "left";
						case "front":
							if (caller.entity.flip)
							{
								break;
							}
							goto case "left";
						}
						if (caller.pusher != null)
						{
							caller.pusher.enabled = false;
						}
						for (int num57 = 0; num57 < instance.playerdata.Length; num57++)
						{
							if (instance.playerdata[num57].entity.transform != player.transform)
							{
								instance.playerdata[num57].entity.forcejump = true;
							}
							instance.playerdata[num57].entity.backsprite = false;
							instance.playerdata[num57].entity.MoveTowards(vector + vector2 * num57 * 1.3f, 1f, 1, 0, true, caller.transform.position);
							Physics.IgnoreCollision(instance.playerdata[num57].entity.ccol, caller.entity.ccol, true);
						}
						if (map.chompy != null)
						{
							map.chompy.following = instance.playerdata[instance.playerdata.Length - 1].entity;
							map.chompy.MoveTowards(map.chompy.following.forcetarget + vector2 * 0.95f + instance.globalcamdir.forward * -0.45f, 1f, 1, 0, true);
							map.chompy.forcejump = true;
							Physics.IgnoreCollision(map.chompy.ccol, caller.entity.ccol, true);
						}
						if (a2 && textbox != null && textbox.GetComponent<DialogueAnim>() != null)
						{
							textbox.GetComponent<DialogueAnim>().shrink = !textbox.GetComponent<DialogueAnim>().shrink;
						}
						float y2 = 0f;
						while (!PartyIsNotMoving())
						{
							for (int num58 = 0; num58 < instance.playerdata.Length; num58++)
							{
								instance.playerdata[num58].entity.backsprite = false;
								if (!instance.playerdata[num58].entity.forcemove)
								{
									instance.playerdata[num58].entity.FaceTowards(caller.transform.position);
								}
							}
							y2 += framestep;
							if (y2 > 300f)
							{
								for (int num59 = 0; num59 < instance.playerdata.Length; num59++)
								{
									instance.playerdata[num59].entity.transform.position = instance.playerdata[num59].entity.forcetarget;
								}
							}
							yield return null;
						}
						if (map.chompy != null)
						{
							while (map.chompy.forcemove)
							{
								if (y2 > 300f)
								{
									map.chompy.transform.position = map.chompy.forcetarget;
								}
								else
								{
									y2 += framestep;
								}
								yield return null;
							}
							Physics.IgnoreCollision(map.chompy.ccol, caller.entity.ccol, false);
							map.chompy.FaceTowards(caller.transform.position);
							map.chompy.animstate = 0;
						}
						if (a2 && textbox != null && textbox.GetComponent<DialogueAnim>() != null)
						{
							textbox.GetComponent<DialogueAnim>().shrink = !textbox.GetComponent<DialogueAnim>().shrink;
						}
						for (int num60 = 0; num60 < instance.playerdata.Length; num60++)
						{
							Physics.IgnoreCollision(instance.playerdata[num60].entity.ccol, caller.entity.ccol, false);
							instance.playerdata[num60].entity.backsprite = false;
							instance.playerdata[num60].entity.FaceTowards(caller.transform.position);
						}
						if (caller.pusher != null)
						{
							caller.pusher.enabled = true;
						}
						instance.skiptext = false;
						instance.overridefollower = tmes;
						initialflip = player.entity.flip;
						if (flipback)
						{
							caller.entity.FaceTowards(player.transform.position);
						}
						if (map.chompy != null)
						{
							map.chompy.following = temptail;
						}
						break;
					}
					case Commands.Ignorenext:
						ignorenext = Convert.ToInt32(temp[1]);
						break;
					case Commands.Sizemulti:
						temp = new string[3]
						{
							"",
							string.Concat(size.x * Convert.ToSingle(temp[1])),
							string.Concat(size.y * Convert.ToSingle(temp[2]))
						};
						goto case Commands.Size;
					case Commands.Size:
						while (true)
						{
							if (!locksize || (temp.Length > 3 && temp[3] == "force"))
							{
								if (temp[1] == "unpause" && pausemenu == null && instance.inlist)
								{
									size = new Vector2((temp[2] == "x") ? size.x : Convert.ToSingle(temp[2]), (temp[3] == "y") ? size.y : Convert.ToSingle(temp[(temp.Length == 3) ? 2 : 3]));
								}
								else if (temp[1] != "unpause")
								{
									size = ((temp.Length <= 2) ? new Vector2(Convert.ToSingle(temp[1]), Convert.ToSingle(temp[1])) : new Vector2((temp[1] == "x") ? size.y : Convert.ToSingle(temp[1]), (temp[2] == "y") ? size.y : Convert.ToSingle(temp[2])));
									if (temp.Length > 3 && temp[3] == "lock")
									{
										locksize = true;
									}
								}
								if (ndd != null)
								{
									ndd.transform.localScale = new Vector3(size.x * langOffset, size.y, 1f) * 0.07f;
								}
								if (!backtracking && dialogue)
								{
									tempdiag = tempdiag + "|" + command + "|";
								}
								bleepvolume = size.magnitude;
								break;
							}
							if (temp.Length <= 3 || !(temp[3] == "unlock"))
							{
								break;
							}
							locksize = false;
						}
						break;
					case Commands.Minibubble:
					{
						if (temp.Length == 1)
						{
							minibubble = true;
							bubbles.Add(parent.gameObject.GetComponent<MiniBubble>());
							break;
						}
						string text9 = ((temp[1][0] != '@') ? GetDialogueText(Convert.ToInt32(temp[1])) : temp[1].Replace("}", ",").Replace("{", "|").Remove(0, 1));
						new Vector3(0f, 1f, 10f);
						EntityControl entityControl3 = (testing ? player.entity : ((!(caller != null)) ? GetEntity(temp[2]) : GetEntity(temp[2], caller.entity)));
						if (!(entityControl3 != null))
						{
							break;
						}
						Vector3 pos;
						if (temp.Length > 4)
						{
							pos = new Vector3(Convert.ToSingle(temp[3]), Convert.ToSingle(temp[4]), 10f);
						}
						else
						{
							float x = MainCamera.WorldToViewportPoint(entityControl3.transform.position).x - 0.5f;
							float y = 0.85f;
							if (temp.Length == 4)
							{
								y = Convert.ToSingle(temp[3]);
							}
							pos = GetMinibubblePos(x, y);
						}
						bubbles.Add(MiniBubble.SetUp(text9, entityControl3, pos, 10 + 3 * bubbles.Count));
						break;
					}
					case Commands.Destroyminibubble:
						if (temp.Length == 1)
						{
							for (int num45 = 0; num45 < bubbles.Count; num45++)
							{
								if (bubbles[num45] != null)
								{
									bubbles[num45].DestroyThis();
								}
							}
						}
						else
						{
							int num46 = Convert.ToInt32(temp[1]);
							if (bubbles.Count <= num46 + 1 && bubbles[num46] != null)
							{
								bubbles[num46].DestroyThis();
							}
						}
						break;
					case Commands.Halt:
						while (true)
						{
							yield return null;
						}
					case Commands.Waitminibubble:
						if (temp.Length == 1)
						{
							while (bubbles.Count > 0)
							{
								for (int num44 = 0; num44 < bubbles.Count; num44++)
								{
									if (bubbles[num44] == null)
									{
										bubbles.RemoveAt(num44);
									}
								}
								yield return null;
							}
						}
						else
						{
							int redirect = Convert.ToInt32(temp[1]);
							while (bubbles[redirect] != null)
							{
								yield return null;
							}
						}
						break;
					case Commands.Showmoney:
						instance.showmoney = 10f;
						break;
					case Commands.Hidemoney:
						instance.showmoney = -1f;
						break;
					case Commands.Event:
						if (char.IsNumber(temp[1][0]))
						{
							events.StartEvent(Convert.ToInt32(temp[1]), caller);
							tempoverf = true;
							tempevent = true;
							end = true;
						}
						else
						{
							instance.inevent = Convert.ToBoolean(temp[1]);
						}
						break;
					case Commands.Camlimit:
						map.camlimitneg = VectorFromString(new string[3]
						{
							temp[1],
							temp[2],
							temp[3]
						});
						map.camlimitneg = VectorFromString(new string[3]
						{
							temp[4],
							temp[5],
							temp[6]
						});
						break;
					case Commands.Innsleep:
					{
						if (textbox != null && textbox.GetComponent<DialogueAnim>() != null)
						{
							textbox.GetComponent<DialogueAnim>().shrink = true;
						}
						Vector3? position2 = null;
						if (temp.Length > 1)
						{
							position2 = VectorFromString(new string[3]
							{
								temp[1],
								temp[2],
								temp[3]
							});
						}
						Vector3? camn = null;
						Vector3? camp = null;
						if (temp.Length > 5)
						{
							camn = VectorFromString(new string[3]
							{
								temp[5],
								temp[6],
								temp[7]
							});
							camp = VectorFromString(new string[3]
							{
								temp[8],
								temp[9],
								temp[10]
							});
						}
						chaptername = instance.StartCoroutine(InnSleep(caller, position2, temp.Length > 4 && temp[4] == "true", false, camn, camp));
						while (chaptername != null)
						{
							yield return null;
						}
						end = true;
						break;
					}
					case Commands.Lore:
						instance.flagstring[0] = Resources.Load<TextAsset>("Data/Dialogues" + languageid + "/LoreText").ToString().Split('\n')[instance.flagvar[0]].Split('@')[1] + "|break||hide||fwait,0.05||boxstyle,3||fwait,0.05||hide||goto,1|";
						temp = new string[3] { "string", "0", "true" };
						goto case Commands.String;
					case Commands.Waitcn:
						while (chaptername != null)
						{
							yield return null;
						}
						break;
					case Commands.Addquest:
					{
						if (temp.Length == 2)
						{
							AddQuest(Convert.ToInt32(temp[1]));
							break;
						}
						int num32 = Convert.ToInt32(temp[2]);
						if (num32 > 0)
						{
							UpdateJounal();
						}
						ChangeBoardQuest(Convert.ToInt32(temp[1]), num32);
						break;
					}
					case Commands.Transitionsort:
					{
						SpriteRenderer transitionSprite = GetTransitionSprite();
						if (transitionSprite != null)
						{
							transitionSprite.sortingOrder = Convert.ToInt32(temp[1]);
						}
						break;
					}
					case Commands.Checkregional:
						if ((temp.Length < 4 && !instance.regionalflags[Convert.ToInt32(temp[1])]) || (temp.Length == 4 && instance.regionalflags[Convert.ToInt32(temp[1])] == Convert.ToBoolean(temp[3])))
						{
							text = OrganizeLines("|blank|" + GetDialogueText(Convert.ToInt32(temp[2])), linebreak.Value, size.x, fonttype);
							instance.skiptext = false;
							skipi = true;
							k = -1;
						}
						break;
					case Commands.Limit:
						linebreak = ((!(temp[1] == "null")) ? new float?(Convert.ToSingle(temp[1])) : ((float?)null));
						break;
					case Commands.Addprize:
						AddPrizeMedal(Convert.ToInt32(temp[1]));
						break;
					case Commands.Itemname:
						if (temp.Length >= 3 && temp[2] == "1")
						{
							instance.flagstring[(temp.Length > 2) ? Convert.ToInt32(temp[2]) : 0] = (instance.flags[681] ? menutext[59] : badgedata[IntFromString(temp[1]), 0]);
						}
						else
						{
							instance.flagstring[(temp.Length > 2) ? Convert.ToInt32(temp[2]) : 0] = itemdata[0, IntFromString(temp[1]), 0];
						}
						break;
					case Commands.Itemvalue:
						if (temp.Length >= 3 && temp[2] == "1")
						{
							instance.flagvar[(temp.Length > 2) ? Convert.ToInt32(temp[2]) : 0] = (instance.flags[681] ? 35 : Convert.ToInt32(badgedata[IntFromString(temp[1]), 5]));
						}
						else
						{
							instance.flagvar[(temp.Length > 2) ? Convert.ToInt32(temp[2]) : 0] = Convert.ToInt32(itemdata[0, IntFromString(temp[1]), 4]);
						}
						break;
					case Commands.Particle:
						PlayParticle(temp[1], GetEntity(caller, temp[2]).transform.position + VectorFromString(new string[3]
						{
							temp[3],
							temp[4],
							temp[5]
						}));
						break;
					case Commands.Createitem:
					{
						Vector3 startpos = player.transform.position;
						int timer = 300;
						if (temp.Length > 3)
						{
							startpos = new Vector3(Convert.ToSingle(temp[3]), Convert.ToSingle(temp[4]), Convert.ToSingle(temp[5]));
						}
						if (temp.Length > 6)
						{
							timer = Convert.ToInt32(temp[6]);
						}
						NPCControl nPCControl = EntityControl.CreateItem(startpos, Convert.ToInt32(temp[1]), Convert.ToInt32(temp[2]), Vector3.zero, timer);
						nPCControl.name = nPCControl.name.Insert(0, "Fixed");
						nPCControl.insideid = instance.insideid;
						nPCControl.freezeconstraints = true;
						break;
					}
					case Commands.Alwaysactive:
						GetEntity(temp[1], caller.entity).alwaysactive = Convert.ToBoolean(temp[2]);
						break;
					case Commands.Updateanim:
						GetEntity(temp[1], caller.entity).UpdateAnimSpecific();
						break;
					case Commands.Removebadgeshop:
					{
						int num13 = Convert.ToInt32(temp[1]);
						int num14 = ((!(temp[2] == "var")) ? Convert.ToInt32(temp[2]) : instance.flagvar[Convert.ToInt32(temp[3])]);
						int[] array3 = instance.avaliablebadgepool[num13].ToArray();
						for (int num15 = 0; num15 < array3.Length; num15++)
						{
							if (array3[num15] == num14)
							{
								instance.avaliablebadgepool[num13].RemoveAt(num15);
								instance.avaliablebadgepool[num13].Insert(num15, -1);
								instance.badgeshops[num13].Remove(num14);
								if (num13 == 1)
								{
									instance.flagvar[66]++;
								}
								break;
							}
						}
						break;
					}
					case Commands.Deathsmoke:
						if (temp.Length == 1)
						{
							DeathSmoke(parent.transform.position);
						}
						else if (temp.Length == 2)
						{
							DeathSmoke(GetEntity(temp[1], caller.entity).transform.position);
						}
						else if (temp.Length == 3)
						{
							DeathSmoke(GetEntity(temp[1], caller.entity).transform.position, Vector3.one * Convert.ToSingle(temp[2]));
						}
						break;
					case Commands.Addboard:
					{
						int id = ((!char.IsNumber(temp[1][0])) ? ((int)Enum.Parse(typeof(BoardQuests), temp[1])) : Convert.ToInt32(temp[1]));
						ChangeBoardQuest(id);
						break;
					}
					case Commands.Textangle:
						if (temp.Length == 1)
						{
							textholder.transform.localEulerAngles = Vector3.zero;
							break;
						}
						textholder.transform.localEulerAngles = VectorFromString(new string[3]
						{
							temp[1],
							temp[2],
							temp[3]
						});
						break;
					case Commands.Completequest:
						CompleteQuest(Convert.ToInt32(temp[1]));
						break;
					case Commands.Takeopenquests:
					{
						int[] array2 = instance.boardquests[0].ToArray();
						List<int> list2 = new List<int>();
						if (array2.Length > 1 || array2[0] != 0)
						{
							for (int num4 = 0; num4 < array2.Length; num4++)
							{
								if (array2[num4] != 23 && array2[num4] != 21 && array2[num4] != 9 && array2[num4] != 10 && array2[num4] != 8)
								{
									int num5 = Convert.ToInt32(boardquestdata[array2[num4], 3]);
									if (num5 > -1)
									{
										instance.flags[num5] = true;
									}
								}
								else
								{
									list2.Add(array2[num4]);
								}
							}
							if (array2.Length - list2.Count != 0)
							{
								List<int> list3 = new List<int>(array2);
								for (int num6 = list2.Count - 1; num6 >= 0; num6--)
								{
									list3.Remove(list2[num6]);
								}
								instance.boardquests[1].AddRange(list3.ToArray());
								instance.boardquests[1].Remove(0);
								instance.boardquests[0] = new List<int>((list2.Count == 0) ? new int[1] : list2.ToArray());
								break;
							}
						}
						goto case Commands.Goto;
					}
					case Commands.Activateselectedquest:
					{
						if (temp.Length > 1)
						{
							instance.flagvar[0] = GetValueFromString(temp[1]);
						}
						instance.boardquests[0].Remove(instance.flagvar[0]);
						ChangeBoardQuest(instance.flagvar[0], 1);
						int num = Convert.ToInt32(boardquestdata[instance.flagvar[0], 3]);
						if (num > -1)
						{
							instance.flags[num] = true;
						}
						if (instance.boardquests[0].Count == 0)
						{
							instance.boardquests[0].Add(0);
						}
						instance.flagstring[11] = BoardString();
						instance.flags[2] = true;
						break;
					}
					case Commands.Layer:
						{
							layer = Convert.ToInt32(temp[1]);
							break;
						}
						IL_6b47:
						newValue2 = text7;
						text = text.Replace("|" + command + "|", newValue2);
						k--;
						skipi = true;
						break;
					}
				}
				if (ignorenext > 0)
				{
					ignorenext--;
				}
				if (!skipi)
				{
					k += command.Length + 1;
				}
				skipi = false;
			}
			else if (text[k] == ' ')
			{
				writen++;
				tempdiag += " ";
				currentoffset += 0.3f * size.x;
				if (dialogue)
				{
					SetTalk(dialogue, true);
				}
				if (ndd != null)
				{
					ndd.text += " ";
				}
			}
			else if (text[k] != '\r')
			{
				if (dialogue && speed == 0f)
				{
					instance.inputcooldown = 5f;
				}
				int redirect = fonttype;
				if (!fontlock)
				{
					if ((languageid == 3 && dialogue && !instance.numberprompt) || Regex.IsMatch(text[k].ToString() ?? "", "\\p{IsHiragana}|\\p{IsKatakana}|\\p{IsKangxiRadicals}|\\p{IsCJKUnifiedIdeographs}"))
					{
						redirect = 3;
						fonttype = 3;
					}
					else if ((languageid == 6 && dialogue && !instance.numberprompt) || Regex.IsMatch(text[k].ToString() ?? "", "\\p{IsCyrillic}"))
					{
						redirect = 4;
						fonttype = 4;
					}
					else if ((languageid == 5 && dialogue && !instance.numberprompt) || Regex.IsMatch(text[k].ToString() ?? "", "\\p{IsHangulJamo}|\\p{IsHangulSyllables}|\\p{IsHangulCompatibilityJamo}"))
					{
						redirect = 5;
						fonttype = 5;
					}
				}
				if (!single)
				{
					writen++;
					SetTalk(dialogue, true);
					if (dialogue && k < text.Length - 1)
					{
						PlayBleep(bleep, bleeppitch, bleepvolume, k);
						tempdiag += text[k];
					}
					TextMesh letter = GetEmptyLetter();
					if (letter != null)
					{
						if (ui3d)
						{
							letter.gameObject.layer = 15;
						}
						else if (!tridimensional)
						{
							letter.gameObject.layer = layer;
						}
						else
						{
							letter.gameObject.layer = 0;
						}
						letter.tag = "Letter";
						SetLetter(ref letter, redirect, text[k].ToString() ?? "", textholder.transform, new Vector2(currentoffset, currentline - 0.1f) + letteroffset, instance.textcolors[colorindex], sort, new Vector3(size.x, size.y, 1f) * 0.07f);
						if (dropshadow.HasValue)
						{
							MeshRenderer component4 = letter.GetComponent<MeshRenderer>();
							ds = GetEmptyLetter();
							ds.gameObject.layer = letter.gameObject.layer;
							SetLetter(ref ds, redirect, letter.text, textholder.transform, letter.transform.localPosition + (Vector3)dropshadow.Value, fadeletter ? Color.clear : new Color(0f, 0f, 0f, 0.5f), component4.sortingOrder, letter.transform.localScale);
							if (fadeletter)
							{
								instance.StartCoroutine(GradualColor(ds.GetComponent<MeshRenderer>(), new Color(0f, 0f, 0f, 0.5f), 200f));
							}
							component4.sortingOrder++;
						}
						if (rainbow || wavy || glitchy || shaky || fadeletter)
						{
							FontEffects fontEffects3 = letter.gameObject.AddComponent<FontEffects>();
							fontEffects3.SetEffects(shaky, wavy, rainbow, glitchy, fadeletter, fonttype, k);
							fontEffects3.superglitch = superglitch;
						}
						currentoffset += GetLetterOffset(text[k], redirect, size.x);
					}
					if (currentoffset > maxlenght)
					{
						maxlenght = currentoffset;
					}
					textwidth = maxlenght;
					lasttextcenter = new Vector3(position.x - maxlenght / 2f, position.y, position.z).x;
					if (centralize)
					{
						textholder.transform.localPosition = new Vector3(position.x - maxlenght / 2f, position.y, position.z);
					}
					if (minibubble || (dialogue && ((speed > 0f && !instance.skiptext) || noskip)))
					{
						yield return new WaitForSeconds(speed);
					}
					if (((dialogue && speed > 0f && !instance.skiptext) || minibubble) && char.IsPunctuation(text[k]) && text[k] != '\'' && k + 1 < text.Length - 1 && text[k + 1] != '|' && text[k + 1] != ')' && text[k + 1] != '¿' && text[k + 1] != '¡' && text[k] != '\'' && text[k] != '/' && text[k] != '¿' && text[k] != ')' && text[k] != '¡' && (!char.IsPunctuation(text[k + 1]) || text[k + 1] != '.' || text[k + 1] != '!' || text[k + 1] != '?' || text[k + 1] != ')' || text[k + 1] != '？' || text[k + 1] != '、' || text[k + 1] != '。' || text[k + 1] != '！' || text[k + 1] != '¿' || text[k + 1] != '¡'))
					{
						yield return new WaitForSeconds(0.15f);
					}
				}
				else
				{
					if (ndd == null)
					{
						do
						{
							ndd = GetEmptyLetter();
							if (ndd == null)
							{
								yield return null;
							}
						}
						while (ndd == null);
						SetLetter(ref ndd, redirect, "", textholder.transform, Vector3.zero, instance.textcolors[colorindex], sort, new Vector3(size.x * langOffset, size.y, 1f) * 0.07f);
						ndd.gameObject.layer = layer;
					}
					currentoffset += GetLetterOffset(ndd, size.x * langOffset);
					ndd.text += text[k];
					ndd.transform.localPosition = new Vector3((!centralize) ? 0f : (0f - GetTextLenght(ndd.text, redirect) / 2f), currentline) * langOffset + (Vector3)letteroffset;
					if (dropshadow.HasValue)
					{
						if (ds == null)
						{
							ds = GetEmptyLetter();
							SetLetter(ref ds, redirect, "", ndd.transform, dropshadow.Value, new Color(0f, 0f, 0f, 0.5f), sort - 1, Vector3.one);
						}
						ds.text = ndd.text;
						ds.gameObject.layer = ndd.gameObject.layer;
					}
				}
				if (backbox != null)
				{
					backbox.transform.localPosition = new Vector3(currentoffset / 2f, size.y / 2f + 0.1f, -5f);
					backbox.transform.localScale = new Vector3(currentoffset / 5f * size.x, size.y * 1.5f, 1f);
				}
			}
			if (!dialogue)
			{
				continue;
			}
			SetTalk(dialogue, false);
			while (instance.prompt || instance.itemlist != null)
			{
				yield return null;
			}
			if (instance.promptpick > -1 && instance.promptpointers.Length != 0)
			{
				string text13 = "";
				if (questboardpromp)
				{
					text13 = ((instance.promptpick != 0) ? "|break||loadcamera||end|" : ("|activateselectedquest||break|" + ((!instance.flags[64]) ? ("|tail,null||blank||boxstyle,4|" + menutext[170] + "|flag,64,true||break|") : "") + "|loadcamera||end|"));
				}
				if (instance.flagvar[0] == -555)
				{
					textbox.GetComponent<DialogueAnim>().shrink = false;
				}
				text = "|blank|" + GetText(promptmenu, instance.promptpointers[instance.promptpick]);
				if (linebreak.HasValue)
				{
					text = OrganizeLines(text, linebreak.Value, size.x, fonttype) + text13;
				}
				DialogueAnim dialogueAnim2 = instance.promptbox.GetComponent<DialogueAnim>();
				if (dialogueAnim2 == null)
				{
					dialogueAnim2 = instance.promptbox.gameObject.AddComponent<DialogueAnim>();
				}
				dialogueAnim2.shrink = true;
				UnityEngine.Object.Destroy(instance.promptbox.gameObject, 0.5f);
				instance.promptpick = -1;
				k = -1;
				yield return null;
			}
			if (instance.inlist)
			{
				if (listredirect.HasValue && (listredirect.Value == -2 || listredirect.Value == -3))
				{
					caller.entity.ccol.enabled = true;
					caller.entity.rigid.useGravity = true;
					caller.entity.rigid.constraints = RigidbodyConstraints.FreezeRotation;
					caller.transform.parent = map.transform;
					caller.bounces = 0;
					caller.StartCoroutine(caller.entity.LateVelocity(RandomItemBounce(6f, 12f)));
					UnityEngine.Object.Destroy(caller.entity.sprite.transform.GetChild(0).gameObject);
					caller.entity.Unfix(true);
					caller.touchcooldown = 70f;
					caller.tossed = true;
					caller.timer = 600f;
					if (listredirect.Value == -2)
					{
						caller.entity.animstate = instance.flagvar[1];
						caller.entity.itemstate = instance.flagvar[1];
						caller.entity.basestate = instance.flagvar[1];
						instance.items[listtype].Remove(instance.flagvar[1]);
						instance.items[listtype].Add(instance.flagvar[0]);
					}
					end = true;
					listredirect = null;
					player.actioncooldown = 30f;
					if (eventtoss > -1)
					{
						eventcall = eventtoss;
					}
				}
				else if (listredirect.HasValue && listredirect.Value != -1)
				{
					text = OrganizeLines("|blank|" + GetDialogueText(listredirect.Value), linebreak.Value, size.x, fonttype);
					k = -1;
				}
			}
			SetTalk(dialogue, true);
			instance.inlist = false;
			if (player != null)
			{
				player.npc = new List<NPCControl>();
			}
		}
		if (dialogue)
		{
			if (transfer.HasValue && caller != null)
			{
				caller.interactcd = 15f;
			}
			SetTalk(dialogue, false);
			instance.waitinput = true;
			instance.skiptext = false;
			while (instance.waitinput && !end)
			{
				yield return null;
			}
			if (bleeps != null)
			{
				bleeps.loop = false;
				instance.StartCoroutine(FadeSound(bleeps, 0.1f));
			}
			TextMesh[] componentsInChildren2 = textholder.GetComponentsInChildren<TextMesh>();
			for (int num82 = 0; num82 < componentsInChildren2.Length; num82++)
			{
				DisableLetter(componentsInChildren2[num82]);
			}
			UnityEngine.Object.Destroy(textholder);
			if (!tempevent && !instance.inevent)
			{
				instance.minipause = false;
			}
			instance.message = false;
			if (cameraoffset.magnitude > 0.1f)
			{
				instance.camoffset = camtoffset;
			}
			instance.overridefollower = tempoverf;
			if (textbox != null)
			{
				textbox.GetComponent<DialogueAnim>().shrink = true;
				UnityEngine.Object.Destroy(textbox, 1f);
			}
			if (player != null && !instance.inevent)
			{
				player.entity.rigid.constraints = tcons;
				player.actioncooldown = 20f;
				EntityControl[] array18 = returnentitycol.ToArray();
				for (int num83 = 0; num83 < array18.Length; num83++)
				{
					Physics.IgnoreCollision(array18[num83].ccol, player.entity.ccol, false);
					if (array18[num83].npcdata != null)
					{
						if (array18[num83].npcdata.pusher != null)
						{
							Physics.IgnoreCollision(array18[num83].npcdata.pusher, player.entity.ccol, false);
						}
						if (array18[num83].npcdata.scol != null)
						{
							Physics.IgnoreCollision(array18[num83].npcdata.scol, player.entity.ccol, false);
						}
						if (array18[num83].npcdata.boxcol != null)
						{
							Physics.IgnoreCollision(array18[num83].npcdata.boxcol, player.entity.ccol, false);
						}
					}
				}
			}
			instance.waitinput = false;
			if (!instance.inevent && battle == null)
			{
				EndOfMessage();
			}
		}
		else if (minibubble && parent.GetComponent<MiniBubble>() != null)
		{
			parent.GetComponent<MiniBubble>().DestroyThis();
		}
		yield return null;
		if (transfer.HasValue)
		{
			instance.StartCoroutine(TransferMap(transferi, player.transform.position, transfer.Value, transfer.Value));
		}
		if (eventcall > -1)
		{
			events.StartEvent(eventtoss, caller);
			eventtoss = -1;
		}
		if (tokenbox != null)
		{
			UnityEngine.Object.Destroy(tokenbox.gameObject);
		}
		if (instance.items != null)
		{
			instance.items[1].Remove(110);
		}
		if (caller != null)
		{
			NPCControl.NPCType entitytype = caller.entitytype;
			if (entitytype == NPCControl.NPCType.Object && caller.objecttype == NPCControl.ObjectTypes.Item && caller.hit)
			{
				UnityEngine.Object.Destroy(caller.gameObject);
			}
		}
		if (!instance.inlist && !instance.prompt)
		{
			instance.flags[349] = false;
		}
	}

	private static int IntFromString(string input)
	{
		if (input.Contains("var"))
		{
			return instance.flagvar[Convert.ToInt32(input.Replace("var", ""))];
		}
		if (input.Contains("v"))
		{
			return instance.flagvar[Convert.ToInt32(input.Replace("v", ""))];
		}
		return Convert.ToInt32(input);
	}

	public static void EndOfMessage()
	{
		if (instance.flags[347])
		{
			for (int i = 0; i < 2; i++)
			{
				if (instance.badgeshops[i].Count == 0)
				{
					instance.flags[587 + i] = true;
				}
			}
		}
		if (instance.flags[470] && !instance.librarystuff[0, 41])
		{
			instance.librarystuff[0, 41] = true;
		}
		if (instance.flags[351] && !instance.librarystuff[0, 42])
		{
			instance.librarystuff[0, 42] = true;
		}
		if (instance.flags[605] && !instance.boardquests[2].Contains(27))
		{
			CompleteQuest(27);
		}
		if (map != null)
		{
			map.currentline = -1;
		}
		instance.CheckAchievement();
	}

	public static bool AllActive(EntityControl[] e)
	{
		for (int i = 0; i < e.Length; i++)
		{
			if (e[i].npcdata != null && !e[i].npcdata.hit)
			{
				return false;
			}
		}
		return true;
	}

	public static void SetLetter(ref TextMesh letter, int fontid, string text, Transform parent, Vector3 pos, Color color, int sortorder, Vector3 size)
	{
		SetFont(letter, fontid);
		if (parent != null)
		{
			letter.transform.parent = parent;
		}
		letter.transform.localPosition = pos;
		letter.text = text;
		letter.anchor = TextAnchor.LowerLeft;
		letter.color = color;
		letter.transform.localEulerAngles = Vector3.zero;
		letter.transform.localScale = size;
		MeshRenderer component = letter.GetComponent<MeshRenderer>();
		component.material.color = color;
		component.sortingOrder = sortorder;
	}

	public static Vector3 GetMinibubblePos(Transform target, float y)
	{
		return GetMinibubblePos(MainCamera.WorldToViewportPoint(target.position).x - 0.5f, y);
	}

	public static Vector3 GetMinibubblePos(Transform target)
	{
		return GetMinibubblePos(MainCamera.WorldToViewportPoint(target.position).x - 0.5f, 0.85f);
	}

	public static Vector3 GetMinibubblePos(float x, float y)
	{
		return new Vector3(Mathf.Clamp(x * 33f, -5f, 5f), y, 10f);
	}

	public static void ResetEntitySpeed()
	{
		for (int i = 0; i < map.entities.Length; i++)
		{
			if (map.entities[i] != null && map.entities[i].anim != null)
			{
				map.entities[i].anim.speed = 1f;
			}
		}
		for (int j = 0; j < instance.playerdata.Length; j++)
		{
			instance.playerdata[j].entity.anim.speed = 1f;
		}
	}

	public static void ResetEntitySpeed(bool all)
	{
		if (!all)
		{
			ResetEntitySpeed();
			return;
		}
		EntityControl[] array = UnityEngine.Object.FindObjectsOfType<EntityControl>();
		for (int i = 0; i < array.Length; i++)
		{
			if (!array[i].iskill && array[i].originalid > -1 && array[i].anim != null)
			{
				array[i].anim.speed = 1f;
			}
		}
	}

	public static EntityControl FindEntity(int id)
	{
		EntityControl[] array = UnityEngine.Object.FindObjectsOfType<EntityControl>();
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i].animid == id)
			{
				return array[i];
			}
		}
		return null;
	}

	public static void SortLights()
	{
		SortLights(null);
	}

	public static int MultipleKeys(bool hold)
	{
		int num = 0;
		for (int i = 0; i < InputIO.keys.Length; i++)
		{
			if (GetKey(i, hold))
			{
				num++;
			}
		}
		return num;
	}

	public static void RefreshHUDValues()
	{
		for (int i = 0; i < instance.playerdata.Length; i++)
		{
			instance.playerdata[i].hpt = instance.playerdata[i].hp;
		}
		instance.tpt = instance.tp;
		instance.moneyt = instance.money;
	}

	public static void SortLights(MeshRenderer[] r)
	{
		if (r == null)
		{
			r = UnityEngine.Object.FindObjectsOfType<MeshRenderer>();
		}
		for (int i = 0; i < r.Length; i++)
		{
			if (r[i] != null && r[i].material.shader == fakelight)
			{
				r[i].material.renderQueue = 2500 + (int)(MainCamera.WorldToViewportPoint(r[i].transform.position).z * (float)((battle == null) ? 100 : 10));
			}
		}
	}

	public static void FixEntities()
	{
		EntityControl[] array = UnityEngine.Object.FindObjectsOfType<EntityControl>();
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i].npcdata == null || ((array[i].npcdata.entitytype == NPCControl.NPCType.NPC || array[i].npcdata.entitytype == NPCControl.NPCType.Enemy) && !array[i].item))
			{
				array[i].overrideflip = false;
				array[i].overrideanim = false;
				array[i].rigid.useGravity = true;
				array[i].rigid.isKinematic = false;
				array[i].onground = true;
				array[i].rigid.velocity = Vector3.zero;
				if (array[i].anim != null)
				{
					array[i].anim.speed = 1f;
				}
			}
		}
	}

	private static char GetKoreanChar(int[] ids)
	{
		return Convert.ToChar(ids[0] * 588 + ids[1] * 28 + ids[2] + 44032);
	}

	private void UpdateKoreanPrompt(int type)
	{
		for (int i = 3; i <= promptbox.childCount - 4; i++)
		{
			Renderer componentInChildren = promptbox.GetChild(i).GetComponentInChildren<Renderer>();
			if (componentInChildren != null)
			{
				if (koreanHL.Contains(i))
				{
					componentInChildren.material.color = Color.red;
				}
				else if (i >= koreanLimit[type].x && i <= koreanLimit[type].y)
				{
					componentInChildren.material.color = Color.black;
				}
				else
				{
					componentInChildren.material.color = new Color(0f, 0f, 0f, 0.5f);
				}
			}
		}
	}

	public static void ChangeLayer(Transform obj, int layer)
	{
		Transform[] componentsInChildren = obj.GetComponentsInChildren<Transform>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].gameObject.layer = layer;
		}
	}

	private void ChangeLetterPrompt(int specific = -1)
	{
		if (specific > -1)
		{
			letterprompt = specific;
		}
		else
		{
			int num = letterprompt;
			if ((uint)(num - 101) > 1u)
			{
				letterprompt++;
				if (letterprompt >= letterPromptHelp.Length)
				{
					letterprompt = 0;
				}
			}
			else
			{
				letterprompt = 4;
			}
		}
		CreateLetterPrompt(letterprompt, instance.promptbox.GetComponentInChildren<SpriteRenderer>().color);
	}

	private static void CreateLetterPrompt(int id, Color color)
	{
		color = Color.white;
		if (id == -1)
		{
			switch (languageid)
			{
			case 3:
				id = 1;
				break;
			case 6:
				id = 3;
				break;
			case 5:
				id = 4;
				break;
			default:
				id = 0;
				break;
			}
		}
		bool flag = instance.promptbox != null;
		if (flag && instance.promptbox != null)
		{
			for (int i = 0; i < instance.promptbox.childCount; i++)
			{
				DestroyText(instance.promptbox.GetChild(i), false);
			}
			UnityEngine.Object.Destroy(instance.promptbox.gameObject);
		}
		instance.inputcooldown = 10f;
		string[] array = instance.flagstring[2].Split(',');
		instance.prompt = true;
		instance.numberprompt = true;
		instance.letterprompt = id;
		instance.flagvar[0] = -555;
		instance.flagvar[1] = 0;
		instance.flagstring[0] = "";
		instance.flagvar[10] = Convert.ToInt32(array[4]);
		instance.flagstring[1] = Resources.Load<TextAsset>("Data/LetterPrompt" + id).ToString();
		instance.maxoptions = 0;
		instance.option = 0;
		instance.promptbox = Create9Box(new Vector3(0f, -1.25f, 10f), new Vector2(15f, 7.5f), 1, -3, color, !flag);
		if (flag)
		{
			instance.promptbox.transform.localScale = Vector3.one;
		}
		Transform obj = Create9Box(new Vector3(0f, 3.75f, 10f), new Vector2(13f, 2f), 1, -3, color, false);
		obj.transform.parent = instance.promptbox.transform;
		obj.transform.localScale = Vector3.one;
		obj.transform.localPosition = new Vector3(0f, 5f);
		instance.StartCoroutine(SetText("|center|" + ((array[3][0] == '@') ? array[3] : GetDialogueText(Convert.ToInt32(array[3]))), new Vector3(0f, 4.75f, 9f), instance.promptbox));
		listtype = Convert.ToInt32(array[1]);
		listredirect = Convert.ToInt32(array[2]);
		listcancel = listredirect.Value;
		instance.npromptholder = new GameObject("letter holder").transform;
		instance.npromptholder.transform.parent = instance.promptbox.transform;
		instance.npromptholder.transform.localPosition = new Vector3(0f, 2.7f, 0.05f);
		instance.npromptholder.transform.localEulerAngles = Vector3.zero;
		instance.npromptholder.transform.localScale = Vector3.one;
		List<int> list = new List<int>();
		float num = -6.35f;
		float num2 = 1.8f;
		bool flag2 = false;
		int num3 = 0;
		for (int j = 0; j < instance.flagstring[1].Length; j++)
		{
			if (instance.flagstring[1][j] == '\n')
			{
				flag2 = true;
				num3++;
				num2 -= ((instance.letterprompt == 4 && num3 < 3) ? 1.1f : 0.75f);
				num = -6.35f;
				continue;
			}
			if (instance.flagstring[1][j] == '{')
			{
				if (!flag2)
				{
					instance.flagvar[1]++;
				}
				num += 0.65f;
				list.Add(instance.maxoptions - 1);
				continue;
			}
			if (!flag2)
			{
				instance.flagvar[1]++;
			}
			string text = instance.flagstring[1][j].ToString() + "|choicewave," + instance.maxoptions + ",true|";
			instance.maxoptions++;
			instance.StartCoroutine(SetText("|center|" + text, 0, null, false, false, new Vector3(num, num2, 0f), Vector3.zero, new Vector3(1f, 1f, 1f), instance.promptbox, null));
			num += 0.65f;
		}
		instance.flagstring[1] = instance.flagstring[1].Replace("\n", "").Replace("{", "");
		instance.multilist = list.ToArray();
		instance.maxoptions += 3;
		instance.StartCoroutine(SetText("|center|" + menutext[74] + "|choicewave," + (instance.maxoptions - 3) + "|", 0, null, false, false, new Vector3(-5f, -3f, 0f), Vector3.zero, new Vector3(1f, 1f, 1f), instance.promptbox, null));
		instance.StartCoroutine(SetText("|center|" + menutext[192] + "|choicewave," + (instance.maxoptions - 2) + "|", 0, null, false, false, new Vector3(0f, -3f, 0f), Vector3.zero, new Vector3(1f, 1f, 1f), instance.promptbox, null));
		instance.StartCoroutine(SetText("|center|" + ((languageid == 6) ? "|sizemulti,0.8,1|" : "") + menutext[42] + "|choicewave," + (instance.maxoptions - 1) + "|", 0, null, false, false, new Vector3(5f, -3f, 0f), Vector3.zero, new Vector3(1f, 1f, 1f), instance.promptbox, null));
		new GameObject("button").AddComponent<ButtonSprite>().SetUp(6, -1, letterPromptHelp[id], new Vector3(-2.25f, -1.9f), Vector3.one * 0.5f, 10, instance.promptbox);
		if (instance.letterprompt == 4)
		{
			koreanHL = new int[2] { -1, -1 };
			instance.flagvar[6] = 0;
			instance.UpdateKoreanPrompt(0);
		}
		instance.RefreshNumberPrompt();
	}

	public static TextMesh GetEmptyLetter()
	{
		for (int i = 0; i < letterpool.Length; i++)
		{
			if (letterpool[i] == null)
			{
				letterpool[i] = NewLetter(i.ToString());
				return letterpool[i];
			}
			if (letterpool[i].text == "")
			{
				return letterpool[i];
			}
		}
		return null;
	}

	public static bool AddExp(int ammount)
	{
		instance.partyexp += ammount;
		if (!sounds[4].isPlaying)
		{
			PlaySound("Exp2", 4, 1.5f + UnityEngine.Random.Range(-0.1f, 0.1f), 1f);
		}
		else if (!sounds[5].isPlaying)
		{
			PlaySound("Exp2", 5, 1.5f + UnityEngine.Random.Range(-0.1f, 0.1f), 1f);
		}
		else if (!sounds[6].isPlaying)
		{
			PlaySound("Exp2", 6, 1.5f + UnityEngine.Random.Range(-0.1f, 0.1f), 1f);
		}
		else
		{
			PlaySound("Exp2", 7, 1.5f + UnityEngine.Random.Range(-0.1f, 0.1f), 1f);
		}
		if (instance.partyexp >= instance.neededexp)
		{
			instance.partyexp -= instance.neededexp;
			return true;
		}
		return false;
	}

	public static void DeathSmoke(Vector3 pos, Vector3 size, int render)
	{
		deathpart.transform.localScale = size;
		deathpart.transform.position = pos;
		deathpart.Emit(20);
		deathpart.GetComponent<Renderer>().material.renderQueue = render;
	}

	public static void DeathSmoke(Vector3 pos, Vector3 size)
	{
		DeathSmoke(pos, size, 3000);
	}

	public static void DeathSmoke(Vector3 pos)
	{
		DeathSmoke(pos, Vector3.one);
	}

	public static void HitPart(Vector3 pos)
	{
		hitpart.transform.position = pos;
		hitpart.Play();
	}

	public static void PlayBleep(AudioClip bleep, float bleeppitch, float bleepvol, int i)
	{
		if (i % 2 == 0 && bleep != null && !bleeps.isPlaying)
		{
			bleeps.clip = bleep;
			bleeps.volume = bleepvol * ((pausemenu == null) ? bleepvolume : pausemenu.dvolume);
			bleeps.pitch = bleeppitch + UnityEngine.Random.Range(-0.05f, 0.05f);
			bleeps.Play();
		}
	}

	public static bool ArrayIsEmpty(object[] input, bool stringcheck)
	{
		if (stringcheck)
		{
			return ArrayIsEmpty(input);
		}
		for (int i = 0; i < input.Length; i++)
		{
			if (input[i] != null)
			{
				return false;
			}
		}
		return true;
	}

	public static bool ArrayIsEmpty(object[] input)
	{
		for (int i = 0; i < input.Length; i++)
		{
			if (input[i] != null && input[i].ToString() != "null")
			{
				return false;
			}
		}
		return true;
	}

	public static void UpdateJounal()
	{
		UpdateJounal(Library.Discovery, -1);
	}

	public static void UpdateJounal(Library type, int variable)
	{
		if (type != Library.Bestiary || !instance.inbattle)
		{
			if (variable > -1)
			{
				instance.librarystuff[(int)type, variable] = true;
			}
			instance.discoveryhud = 350f;
			instance.discoverymessage.GetComponentInChildren<Animator>().PlayInFixedTime((type == Library.Logbook) ? "Arch" : "Disc");
			for (int i = 0; i < 3; i++)
			{
				instance.discoverymessage.GetChild(i + 1).gameObject.SetActive((type == Library.Logbook) ? (i + 1 == 3) : (i + 1 == 1));
			}
		}
		else
		{
			instance.librarystuff[(int)type, variable] = true;
		}
	}

	private static EntityControl GetEntity(NPCControl caller, string args)
	{
		EntityControl entityControl = null;
		if (caller != null)
		{
			return GetEntity(args, caller.entity);
		}
		return GetEntity(Convert.ToInt32(args));
	}

	private static void SetTalk(bool dialogue, bool talkstate)
	{
		if (dialogue && instance.tailtarget != null)
		{
			instance.tailtarget.GetComponent<EntityControl>().talking = talkstate;
			if (talkstate)
			{
				instance.tailtarget.GetComponent<EntityControl>().backsprite = false;
			}
		}
	}

	public static float GetTextLenght(string text, int fontid)
	{
		float num = 0f;
		for (int i = 0; i < text.Length; i++)
		{
			num = ((text[i] != ' ') ? (num + GetLetterOffset(text[i], fontid, 1f)) : (num + 0.3f));
		}
		return num;
	}

	public static void HealParticle(Transform parent, Vector3 size, Vector3 offset)
	{
		HealParticle(parent, size, offset, false);
	}

	public static void HealParticle(Transform parent, Vector3 size, Vector3 offset, bool UI)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate(Resources.Load("Prefabs/Particles/Heal")) as GameObject;
		gameObject.transform.parent = parent;
		gameObject.transform.localScale = size;
		gameObject.transform.localPosition = offset;
		if (UI)
		{
			gameObject.layer = 5;
		}
		UnityEngine.Object.Destroy(gameObject, 3f);
	}

	public static void CreateCursor(Transform parent)
	{
		instance.cursor = new GameObject("Cursor").AddComponent<SpriteRenderer>();
		instance.cursor.sprite = cursorsprite[0];
		instance.cursor.GetComponent<SpriteRenderer>().sortingOrder = 1;
		instance.cursor.transform.parent = parent;
		instance.cursor.gameObject.layer = 5;
		instance.cursor.transform.localScale = Vector3.one;
		instance.cursor.transform.localEulerAngles = Vector3.zero;
		instance.cursor.gameObject.AddComponent<SpriteBounce>().MessageBounce();
	}

	public static Vector3 RandomItemBounce(float range, float height)
	{
		range /= 2f;
		Vector3 vector = ClampMagnitude(RandomVector(range, range), range, range);
		return new Vector3(vector.x, height, vector.y);
	}

	public static void DestroyTemp(ref GameObject obj, float time)
	{
		if (obj != null)
		{
			obj.transform.position = new Vector3(0f, -9999f);
			UnityEngine.Object.Destroy(obj.gameObject, time);
			obj = null;
		}
	}

	public static void DestroyTemp(GameObject obj, float time)
	{
		if (obj != null)
		{
			obj.transform.position = new Vector3(0f, -9999f);
			UnityEngine.Object.Destroy(obj.gameObject, time);
		}
	}

	private static int[] GetLibraryIDs(int index)
	{
		List<int> list = new List<int>();
		for (int i = 0; i < librarylimit[index]; i++)
		{
			list.Add(libraryorder[index, i]);
		}
		return list.ToArray();
	}

	public static IEnumerator DelayedPosition(Transform obj, Vector3 pos, float delay, bool local)
	{
		if (delay > 0f)
		{
			yield return new WaitForSeconds(delay);
		}
		else
		{
			yield return null;
		}
		if (local)
		{
			obj.transform.localPosition = pos;
		}
		else
		{
			obj.transform.position = pos;
		}
	}

	public static void DisableRender(Renderer render, float tolerance, Vector3 offset)
	{
		if (CheckIfCamera(render.transform.position, tolerance, offset))
		{
			render.shadowCastingMode = ShadowCastingMode.On;
		}
		else
		{
			render.shadowCastingMode = ShadowCastingMode.ShadowsOnly;
		}
	}

	public static void DisableRender(GameObject render, float tolerance, Vector3 offset)
	{
		render.SetActive(CheckIfCamera(render.transform.position, tolerance, offset));
	}

	public static void ShakeScreen(Vector3 ammount, float time)
	{
		ShakeScreen(ammount, time, false);
	}

	public static void ShakeScreen(float ammount, float time, bool dontreset)
	{
		ShakeScreen(Vector3.one * ammount, time, dontreset);
	}

	public static void ShakeScreen(Vector3 ammount, float time, bool dontreset)
	{
		camposshake = instance.camoffset;
		screenshake = new Vector3(ammount.x, ammount.y / 2f, 0f);
		if (time > 0f)
		{
			if (dontreset)
			{
				instance.Invoke("StopScreenShake", time);
			}
			else
			{
				instance.Invoke("StopScreenShakeReturn", time);
			}
		}
	}

	public static void ShakeScreen(float ammount, float time)
	{
		ShakeScreen(Vector3.one * ammount, time);
	}

	public static void ShakeScreen(float time)
	{
		ShakeScreen(Vector3.one * 0.1f, time);
	}

	public static void ShakeScreen()
	{
		ShakeScreen(Vector3.one * 0.1f, -1f);
	}

	public static void ShakeScreen(Vector3 ammount)
	{
		ShakeScreen(ammount, -1f);
	}

	public void StopScreenShakeReturn()
	{
		screenshake = Vector3.zero;
		MainCamera.transform.localPosition = camposshake;
	}

	public void StopScreenShake()
	{
		screenshake = Vector3.zero;
	}

	public static void FadeMusic(float fadespeed)
	{
		ChangeMusic((AudioClip)null, fadespeed);
	}

	private static bool CheckIfCamera(Vector3 campos, float tolerance, Vector3 offset)
	{
		campos = MainCamera.WorldToViewportPoint(campos + offset);
		if (campos.x > -0.5f && campos.x < 1.5f && campos.y > -0.5f && campos.y < 1.5f)
		{
			return campos.z > tolerance;
		}
		return false;
	}

	public static int GetTPCost(int player, int id)
	{
		return GetTPCost(player, id, false);
	}

	public static int GetTPCost(int player, int id, bool matchid)
	{
		if (matchid)
		{
			int[] array = instance.playerdata[player].skills.ToArray();
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] == id)
				{
					id = i;
					break;
				}
			}
		}
		id = instance.playerdata[player].skills[id];
		int num = Convert.ToInt32(skilldata[id, 2]);
		int num2 = Mathf.Abs(num);
		if (BadgeIsEquipped(28) && (id == 16 || id == 2 || id == 18 || id == 24))
		{
			num2++;
		}
		if (BadgeIsEquipped(72) && (id == 11 || id == 45))
		{
			num2 += BadgeHowManyEquipped(72, instance.playerdata[player].trueid);
		}
		if (num2 == 0)
		{
			return 0;
		}
		return Mathf.Clamp(num2 - BadgeHowManyEquipped(25, instance.playerdata[player].trueid) - BadgeHowManyEquipped(72, instance.playerdata[player].trueid), 1, 99) * ((num >= 0) ? 1 : (-1));
	}

	public static int HowManyItem(int type, int id)
	{
		if (instance.items[type] == null || instance.items[type].Count == 0)
		{
			return 0;
		}
		int num = 0;
		for (int i = 0; i < instance.items[type].Count; i++)
		{
			if (instance.items[type][i] == id)
			{
				num++;
			}
		}
		return num;
	}

	public static IEnumerator ArcMovement(GameObject obj, Vector3 startpos, Vector3 targetpos, Vector3 spin, float height, float frametime, bool destroyonend)
	{
		float a = 0f;
		do
		{
			if (obj == null)
			{
				yield break;
			}
			obj.transform.Rotate(spin * TieFramerate(1f));
			obj.transform.position = BeizierCurve3(startpos, targetpos, height, a / frametime);
			a += TieFramerate(1f);
			yield return null;
		}
		while (a < frametime + 1f);
		if (obj != null)
		{
			if (destroyonend)
			{
				UnityEngine.Object.Destroy(obj);
			}
			else
			{
				obj.transform.position = targetpos;
			}
		}
		yield return null;
	}

	public static string GetBadgeName(int id)
	{
		return menutext[268].Replace("i", "replace1").Replace("m", "replace2").Replace("replace1", badgedata[id, 0])
			.Replace("replace2", menutext[159]);
	}

	public static IEnumerator ArcMovement(GameObject obj, Vector3 targetpos, float height, float frametime)
	{
		instance.StartCoroutine(ArcMovement(obj, obj.transform.position, targetpos, Vector3.zero, height, frametime, false));
		yield return null;
	}

	public static void RefreshBadgeOrder()
	{
		List<int[]> list = new List<int[]>();
		for (int i = 0; i < badgeorder.Length; i++)
		{
			for (int j = 0; j < instance.badges.Count; j++)
			{
				if (instance.badges[j][0] == badgeorder[i])
				{
					list.Add(instance.badges[j]);
				}
			}
		}
		instance.badges = list;
	}

	public static int[] OrganizeArrayInt(int[] inputarray, int[] order)
	{
		List<int> list = new List<int>();
		for (int i = 0; i < order.Length; i++)
		{
			for (int j = 0; j < inputarray.Length; j++)
			{
				if (inputarray[j] == order[i])
				{
					list.Add(inputarray[j]);
					break;
				}
			}
		}
		return list.ToArray();
	}

	private static int[] GetBadgeIDs()
	{
		if (instance.badges.Count == 0)
		{
			if (!(pausemenu != null))
			{
				return new int[1] { -1 };
			}
			return new int[0];
		}
		List<int> list = new List<int>();
		int[][] array = instance.badges.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			list.Add(array[i][0]);
		}
		return list.ToArray();
	}

	public static int[] GetEquippedBadgeIDs()
	{
		if (instance.badges.Count == 0)
		{
			if (!(pausemenu != null))
			{
				return new int[1] { -1 };
			}
			return new int[0];
		}
		List<int> list = new List<int>();
		int[][] array = instance.badges.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i][1] > -2)
			{
				list.Add(i);
			}
		}
		return list.ToArray();
	}

	private static int[] GetLibraryOrder(int id)
	{
		int[] array = new int[librarylimit[id]];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = libraryorder[id, i];
		}
		return array;
	}

	private static int[] GetOverralQuests()
	{
		List<int> list = new List<int>();
		int[] array = instance.boardquests[1].ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i] != 0)
			{
				list.Add(array[i]);
			}
		}
		array = instance.boardquests[2].ToArray();
		for (int j = 0; j < array.Length; j++)
		{
			if (array[j] != 0)
			{
				list.Add(-array[j]);
			}
		}
		return list.ToArray();
	}

	public static Vector3[] GetPartyPos(bool inorder)
	{
		List<Vector3> list = new List<Vector3>();
		if (inorder)
		{
			for (int i = 0; i < 3; i++)
			{
				EntityControl entity = GetEntity(-(4 + i));
				if (entity != null)
				{
					list.Add(entity.transform.position);
				}
			}
		}
		else
		{
			for (int j = 0; j < instance.playerdata.Length; j++)
			{
				list.Add(instance.playerdata[j].entity.transform.position);
			}
		}
		return list.ToArray();
	}

	public static void DestroyPlayers(bool remake, bool inorder)
	{
		Vector3[] partyPos = GetPartyPos(inorder);
		for (int i = 0; i < instance.playerdata.Length; i++)
		{
			UnityEngine.Object.Destroy(instance.playerdata[i].entity.gameObject);
		}
		if (remake)
		{
			SetPlayers(partyPos);
		}
	}

	public static Vector3 MultiplyVector(Vector3 a, Vector3 b)
	{
		return new Vector3(a.x * b.x, a.y * b.y, a.z * b.z);
	}

	private static int[] GetQuestsBoard(int type)
	{
		int[] array = instance.boardquests[type].ToArray();
		bool flag = type != 0 || Convert.ToInt32(map.name) == 30;
		List<int> list = new List<int>();
		for (int i = 0; i < array.Length; i++)
		{
			if (Convert.ToInt32(map.name) == 0 || (array[i] != 30 && array[i] != 26 && (array[i] < 11 || array[i] > 17) && (flag || (array[i] != 10 && array[i] != 9 && array[i] != 8 && array[i] != 23 && array[i] != 21))))
			{
				list.Add(array[i]);
			}
		}
		if (list.Count == 0)
		{
			list.Add(0);
		}
		return list.ToArray();
	}

	public static int[] GetBosses()
	{
		List<int> list = new List<int>();
		for (int i = 0; i < instance.multilist.Length; i++)
		{
			if (instance.multilist[i] == -2)
			{
				if (instance.flags[555])
				{
					list.Add(-2);
				}
			}
			else if (instance.multilist[i] == -1)
			{
				if (instance.flags[118])
				{
					list.Add(-1);
				}
			}
			else if (instance.enemyencounter[instance.multilist[i], 0] > 0)
			{
				list.Add(instance.multilist[i]);
			}
		}
		return list.ToArray();
	}

	public static void PlaySoundAt(string sound, float volume, Vector3 position)
	{
		AudioSource.PlayClipAtPoint(Resources.Load<AudioClip>("Audio/Sounds/" + sound), position, GetSoundDistance(position) * volume * soundvolume);
	}

	public static int[] GradualFill(int startat, int ammount)
	{
		int[] array = new int[ammount];
		for (int i = 0; i < ammount; i++)
		{
			array[i] = startat + i;
		}
		return array;
	}

	public static int[] GradualFill(int ammount)
	{
		return GradualFill(0, ammount);
	}

	public static int[] GetSettings()
	{
		List<int> list = new List<int>();
		list.AddRange(new int[24]
		{
			0, 1, 2, 21, 19, 20, 3, 4, 6, 7,
			8, 9, 11, 10, 16, 12, 23, 13, 17, 22,
			25, 18, 14, 15
		});
		if (pausemenu.vsyc == 1)
		{
			list.Remove(12);
		}
		if (pausemenu.joystick == 0)
		{
			list.Remove(22);
		}
		if (pausemenu.joystick < 3 || pausemenu.joystick > 4)
		{
			list.Remove(17);
		}
		if (pausemenu.joystick != 5)
		{
			list.Remove(18);
		}
		return list.ToArray();
	}

	public static string[] Controllers()
	{
		string[] joystickNames = Input.GetJoystickNames();
		List<string> list = new List<string>();
		for (int i = 0; i < joystickNames.Length; i++)
		{
			if (joystickNames[i].Length > 1)
			{
				list.Add(joystickNames[i]);
			}
		}
		return list.ToArray();
	}

	public static int[] SamiraMissing()
	{
		int[][] array = instance.samiramusics.ToArray();
		List<int> list = new List<int>();
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i][1] < 1)
			{
				list.Add(i);
			}
		}
		return list.ToArray();
	}

	public static void ShowItemList(int type, Vector2 position, bool showdescription, bool sell)
	{
		if (instance.itemlist == null)
		{
			listY = -1;
			instance.multiselect = new List<int>();
		}
		listpos = position;
		listcanceled = false;
		int num = type;
		int num2 = type;
		int[,] array = null;
		switch (type)
		{
		case -3:
		case -2:
		case -1:
			listvar = instance.playerdata[Mathf.Abs(type) - 1].skills.ToArray();
			num = -1;
			listredirect = null;
			break;
		case 2:
			num = 0;
			goto default;
		case 10:
		case 11:
		case 12:
		case 13:
			listvar = OrganizeArrayInt(GetLibraryIDs(type - 10), GetLibraryOrder(type - 10));
			listredirect = null;
			break;
		case 21:
			listvar = GetOverralQuests();
			listredirect = null;
			break;
		case 22:
			listvar = GradualFill(instance.flagvar[15]);
			listredirect = -66;
			break;
		case 14:
		case 15:
		case 16:
			listvar = GetQuestsBoard(type - 14);
			listredirect = null;
			break;
		case 17:
			if (InputIO.IsConsole)
			{
				if (pausemenu.calledfrommain)
				{
					listvar = new int[8] { 0, 1, 2, 19, 20, 9, 22, 25 };
				}
				else
				{
					listvar = new int[9] { 0, 1, 2, 19, 20, 9, 22, 25, 15 };
				}
			}
			else
			{
				listvar = GetSettings();
			}
			listredirect = null;
			break;
		case 18:
		{
			num = -1;
			List<int> list = new List<int>();
			int[][] array3 = instance.samiramusics.ToArray();
			if (SamiraMissing().Length != 0)
			{
				list.Add(-1);
			}
			for (int k = 0; k < array3.Length; k++)
			{
				list.Add(array3[k][0]);
			}
			listvar = list.ToArray();
			break;
		}
		case 19:
		case 30:
		{
			List<int> list = new List<int>();
			for (int j = 0; j < InputIO.keys.Length; j++)
			{
				list.Add(j);
			}
			listvar = list.ToArray();
			listredirect = null;
			break;
		}
		case 20:
			listvar = new int[7] { 0, 3, 1, 2, 4, 5, 6 };
			listredirect = null;
			break;
		case 3:
			listvar = GradualFill(instance.badges.Count);
			if (instance.pause)
			{
				listredirect = null;
			}
			break;
		case 9:
			listvar = new int[5] { 0, 271, 1, 2, 3 };
			num = -1;
			listredirect = null;
			break;
		case 23:
		{
			int[][] array2 = instance.statbonus.ToArray();
			if (array2.Length == 0)
			{
				listvar = new int[1] { -1 };
				break;
			}
			listvar = new int[array2.Length];
			array = new int[array2.Length, 2];
			for (int i = 0; i < array2.Length; i++)
			{
				listvar[i] = array2[i][0];
				array[i, 0] = array2[i][1];
				array[i, 1] = array2[i][2];
			}
			break;
		}
		case 24:
			listvar = GetBosses();
			break;
		case 25:
			listvar = instance.multilist;
			break;
		case 26:
			listvar = GradualFill(termacadeprize.GetLength(0));
			break;
		case 27:
			type = 0;
			num = 0;
			listvar = GradualFill(Enum.GetNames(typeof(Items)).Length);
			break;
		case 28:
			type = 3;
			num = 3;
			listvar = GradualFill(Enum.GetNames(typeof(BadgeTypes)).Length);
			break;
		case 29:
			type = -1;
			num = -1;
			listvar = GradualFill(Enum.GetNames(typeof(Skills)).Length);
			break;
		case 31:
			type = 31;
			num = 31;
			listvar = GradualFill(Enum.GetNames(typeof(Maps)).Length);
			break;
		case 32:
			type = 3;
			num = 3;
			listvar = instance.multilist;
			listredirect = null;
			break;
		case 33:
			listvar = OrganizeArrayInt(instance.multilist, CardGame.order);
			break;
		case 34:
			showdescription = !instance.flags[681];
			listvar = caravanorder;
			break;
		case 35:
			num = 0;
			type = 0;
			listvar = instance.multilist;
			break;
		default:
			num = 0;
			if (instance.items[type].Count == 0)
			{
				listvar = new int[1] { -1 };
			}
			else
			{
				listvar = instance.items[type].ToArray();
			}
			break;
		}
		instance.maxoptions = listvar.Length;
		if (listlow != listY || listtype == 20)
		{
			listY = listlow;
			if (instance.itemlist == null)
			{
				listmax = listammount;
				listlow = 0;
				instance.option = 0;
				listcursor = 0;
				CreateCursor(GUICamera.transform);
				instance.inlist = true;
				instance.cursor.transform.position = GUICamera.transform.position;
			}
			if (instance.itemlist != null)
			{
				for (int l = 0; l < instance.itemlist.childCount; l++)
				{
					DestroyText(instance.itemlist.GetChild(l), false);
				}
				UnityEngine.Object.Destroy(instance.itemlist.gameObject);
			}
			instance.itemlist = new GameObject("ItemList").transform;
			if (instance.questboardobj != null)
			{
				instance.itemlist.parent = instance.questboardobj;
			}
			else
			{
				instance.itemlist.parent = GUICamera.transform;
			}
			instance.itemlist.localPosition = new Vector3(position.x, position.y, 10f);
			if (listtype == 20)
			{
				instance.StartCoroutine(SetText("|color,4||center||sort,20|" + languagehelp[listvar[instance.option]], new Vector3(1f, 4f), instance.itemlist));
			}
			float num3 = 0.25f;
			if (listlow > 0 && instance.maxoptions > listammount)
			{
				if (!instance.pause && (type <= 16 || type == 18 || type >= 22 || type == 20))
				{
					SpriteRenderer spriteRenderer = new GameObject("UpArrow").AddComponent<SpriteRenderer>();
					spriteRenderer.gameObject.layer = 5;
					spriteRenderer.transform.parent = instance.itemlist;
					if (type >= 14 && type <= 16)
					{
						spriteRenderer.sprite = guisprites[1];
						spriteRenderer.transform.localPosition = new Vector2(8.8f, num3 + 0.2f);
					}
					else
					{
						spriteRenderer.sprite = guisprites[3];
						spriteRenderer.transform.localPosition = new Vector2(1f, num3 + 0.7f);
					}
					spriteRenderer.transform.localEulerAngles = new Vector3(0f, 0f, 180f);
					spriteRenderer.sortingOrder = 3;
					if (type == 20)
					{
						spriteRenderer.sortingOrder = 10;
					}
				}
				else if (pausemenu != null)
				{
					SpriteRenderer component = NewUIObject("uparrow", null, Vector3.zero, Vector3.one, guisprites[1]).GetComponent<SpriteRenderer>();
					component.transform.parent = instance.itemlist;
					component.transform.localEulerAngles = new Vector3(0f, 0f, 180f);
					float x = 11f;
					if (pausemenu.windowid == 3)
					{
						x = 9.5f;
					}
					else if (pausemenu.windowid == 4)
					{
						x = 11.25f;
					}
					else if (pausemenu.windowid == 5)
					{
						x = 8.5f;
					}
					else if (pausemenu.windowid == 1)
					{
						x = 7f;
					}
					component.transform.localPosition = new Vector2(x, num3 + 0.3f);
					component.transform.localScale = Vector3.one * 1.25f;
				}
			}
			if (overridedlist != null)
			{
				LoadList(overridedlist);
				overridedlist = null;
			}
			string text = "";
			float x2 = 0.55f;
			if (IsInMultiList())
			{
				new GameObject().AddComponent<ButtonSprite>().SetUp(7, -1, menutext[281], new Vector3(-0.35f, -3.75f), Vector3.one * 0.4f, 99, instance.itemlist);
				NewUIObject("box", instance.itemlist, new Vector3(1.35f, -3.75f), new Vector3(0.75f, 1.25f, 1f), guisprites[0], -5).GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.75f);
			}
			for (int m = listlow; m < listmax && m < instance.maxoptions; m++)
			{
				SpriteRenderer spriteRenderer2 = new GameObject("Bar" + m).AddComponent<SpriteRenderer>();
				if (!instance.pause && (type < 14 || type == 18 || type >= 22))
				{
					spriteRenderer2.sprite = guisprites[0];
				}
				spriteRenderer2.gameObject.layer = 5;
				spriteRenderer2.transform.parent = instance.itemlist;
				spriteRenderer2.sortingOrder = -1;
				if (!instance.pause)
				{
					spriteRenderer2.transform.localScale = new Vector3(1.15f, 1f, 1f);
				}
				spriteRenderer2.transform.localPosition = new Vector2(1.4f, num3);
				spriteRenderer2.color = (instance.multiselect.Contains(m) ? itemlistbg[1] : itemlistbg[0]);
				num3 -= 0.7f;
				float x3 = -2f;
				float y = -0.15f;
				Vector2 size = new Vector2(0.75f, 0.75f);
				switch (type)
				{
				case 22:
				{
					string[] array7 = Resources.Load<TextAsset>("Data/Dialogues" + languageid + "/LoreText").ToString().Split('\n')[m].Split('@');
					text = array7[0];
					if (m == instance.option)
					{
						instance.flagstring[0] = array7[1];
					}
					x3 = -2.65f;
					break;
				}
				case 26:
				{
					bool flag3 = termacadeprize[listvar[m], 0] == 2;
					int num19 = termacadeprize[listvar[m], 1];
					SpriteRenderer spriteRenderer7 = new GameObject("itemsprite").AddComponent<SpriteRenderer>();
					spriteRenderer7.transform.parent = spriteRenderer2.transform;
					spriteRenderer7.transform.localPosition = new Vector2(-2.5f, 0f);
					spriteRenderer7.gameObject.layer = 5;
					spriteRenderer7.transform.localScale = new Vector3(x2, 0.6f, 1f);
					if (flag3 && instance.flags[681])
					{
						spriteRenderer7.sprite = guisprites[190];
						text = menutext[59];
					}
					else
					{
						spriteRenderer7.sprite = itemsprites[flag3 ? 1 : 0, num19];
						text = (flag3 ? badgedata[num19, 0] : itemdata[0, num19, 0]);
					}
					if (termacadeprize[listvar[m], 3] == 1 && instance.flags[termacadeprize[listvar[m], 4]])
					{
						instance.StartCoroutine(SetText("|sort,10||size,0.6,0.75||color,1|" + menutext[190], new Vector3(1f, -0.15f), spriteRenderer2.transform));
						break;
					}
					instance.StartCoroutine(SetText("|sort,10||size,0.6,0.75|" + termacadeprize[listvar[m], 2], new Vector3(1.4f, -0.15f), spriteRenderer2.transform));
					NewUIObject("tp", spriteRenderer2.transform, new Vector3(2.55f, 0f), new Vector3(0.6f, 0.65f, 1f), itemsprites[0, 110], 10).GetComponent<SpriteRenderer>();
					break;
				}
				case 0:
				case 1:
				case 2:
				{
					SpriteRenderer spriteRenderer6 = new GameObject("itemsprite").AddComponent<SpriteRenderer>();
					if (listvar[m] == -1)
					{
						text = "|color,1|" + menutext[20];
						break;
					}
					spriteRenderer6.sprite = itemsprites[num, listvar[m]];
					spriteRenderer6.transform.parent = spriteRenderer2.transform;
					spriteRenderer6.transform.localPosition = new Vector2(-2.5f, 0f);
					spriteRenderer6.gameObject.layer = 5;
					spriteRenderer6.transform.localScale = new Vector3(x2, 0.6f, 1f);
					text = itemdata[num, listvar[m], 0];
					if (instance.pause)
					{
						spriteRenderer2.transform.localPosition = new Vector3(1f, num3 + 0.7f, 0f);
						size = Vector2.one;
						spriteRenderer6.transform.localScale = new Vector3(0.9f, 0.9f, 1f);
						spriteRenderer6.transform.localPosition = new Vector2(-2.75f, 0f);
						num3 -= 0.2f;
					}
					break;
				}
				default:
				{
					string text6;
					string text5;
					switch (type)
					{
					case 33:
						x3 = -2.65f;
						text = enemynames[instance.cardgame.carddata[listvar[m]].enemyid];
						instance.flagvar[6] = instance.option;
						break;
					case 31:
						x3 = -2.65f;
						text = Enum.GetNames(typeof(Maps))[listvar[m]];
						break;
					case 24:
						x3 = -2.65f;
						switch (listvar[m])
						{
						case -2:
							text = map.dialogues[87];
							break;
						case -1:
							text = map.dialogues[13];
							break;
						case 51:
							text = map.dialogues[74];
							break;
						case 85:
							text = map.dialogues[75];
							break;
						case 92:
							text = map.dialogues[76];
							break;
						case 74:
							text = map.dialogues[77];
							break;
						default:
							text = enemynames[listvar[m]];
							break;
						}
						instance.flagvar[6] = instance.option;
						break;
					case 23:
						x3 = -2.65f;
						if (listvar[m] == -1)
						{
							text = "|color,1|" + menutext[20];
							instance.flagvar[0] = -1;
							break;
						}
						text = string.Concat((StatBonus)listvar[m], " x", array[m, 0], " to ", (array[m, 1] == -1) ? "All" : menutext[46 + array[m, 1]]);
						instance.flagvar[1] = instance.option;
						break;
					case -3:
					case -2:
					case -1:
					{
						int num16 = Mathf.Abs(type) - 1;
						int num17 = ((num2 != 29) ? GetTPCost(num16, m) : 0);
						int tpcost = num17;
						text6 = "";
						text5 = "";
						bool flag2 = BadgeIsEquipped(72, instance.playerdata[num16].trueid) || num17 < 0;
						num17 = Mathf.Abs(num17);
						if (instance.pause)
						{
							num3 -= 0.2f;
							y = -0.2f;
							size = Vector2.one;
							x3 = -0.75f;
							instance.StartCoroutine(SetText("|sort,10||font,0|" + ((num17 == 0) ? "  -" : num17.ToString().PadLeft(2, ' ')), new Vector3(6.9f, -0.2f), spriteRenderer2.transform));
							if (num17 != 0)
							{
								NewUIObject("tp", spriteRenderer2.transform, new Vector3(8.45f, 0f), Vector3.one * 0.6f * (flag2 ? 0.65f : 1f), guisprites[flag2 ? 24 : 28], 10).GetComponent<SpriteRenderer>();
							}
							text6 = "|single|";
						}
						else
						{
							if (!HasSkillCost(tpcost, num16))
							{
								text6 = "|color,1|";
							}
							x3 = -2.65f;
							instance.StartCoroutine(SetText("|sort,10||size,0.75||font,0|" + text6 + num17.ToString().PadLeft(2, ' '), new Vector3(1.6f, -0.15f), spriteRenderer2.transform));
							NewUIObject("tp", spriteRenderer2.transform, new Vector3(2.55f, 0f), new Vector3(0.45f, 0.5f, 1f) * (flag2 ? 0.65f : 1f), guisprites[flag2 ? 24 : 28], 10).GetComponent<SpriteRenderer>();
							int num18 = listvar[m];
							if (num18 <= 11)
							{
								if (num18 == 2)
								{
									goto IL_13fe;
								}
								if (num18 != 3)
								{
									if (num18 == 11)
									{
										goto IL_1425;
									}
								}
								else
								{
									text5 += " |size,0.55,0.6|";
									if (BadgeIsEquipped(67))
									{
										text5 += "|icon,195|";
									}
								}
							}
							else if (num18 <= 18)
							{
								if (num18 == 16)
								{
									goto IL_13ab;
								}
								if (num18 == 18)
								{
									goto IL_13fe;
								}
							}
							else
							{
								if (num18 == 24)
								{
									goto IL_13ab;
								}
								if (num18 == 45)
								{
									goto IL_1425;
								}
							}
						}
						goto IL_148f;
					}
					default:
						{
							if ((type >= 10 && type <= 13) || type == 21)
							{
								x3 = -0.5f;
								size = Vector2.one;
								if (type == 21)
								{
									text = boardquestdata[Mathf.Abs(listvar[m]), 0];
									float[] array4 = new float[12]
									{
										6f, 6f, 6f, 3f, 6f, 6f, 6f, 6f, 6f, 6f,
										6f, 6f
									};
									if (languageid == 4)
									{
										text = "|sizemulti,0.7,1|" + text;
									}
									if (listvar[m] < 0)
									{
										if (Mathf.Abs(listvar[m]) >= 11 && Mathf.Abs(listvar[m]) <= 17)
										{
											NewUIObject("icon", spriteRenderer2.transform, new Vector3(-0.3f, 0.2f), Vector3.one * 0.75f, guisprites[StartMenu.psprite[Mathf.Abs(listvar[m]) - 11]], 5);
											text = "\t" + text + "|tab," + array4[languageid] + "|";
										}
										else
										{
											NewUIObject("check", spriteRenderer2.transform, new Vector3(-0.3f, 0.2f), Vector3.one, guisprites[113], 5).GetComponent<SpriteRenderer>().color = Color.green;
											text = "\t" + text + "|tab," + array4[languageid] * 0.85f + "|";
										}
									}
									else if (Mathf.Abs(listvar[m]) >= 11 && Mathf.Abs(listvar[m]) <= 17)
									{
										NewUIObject("icon", spriteRenderer2.transform, new Vector3(-0.3f, 0.25f), Vector3.one * 0.75f, guisprites[StartMenu.psprite[Mathf.Abs(listvar[m]) - 11]], 5).GetComponent<SpriteRenderer>().color = Color.black;
										text = "\t" + text + "|tab," + array4[languageid] + "|";
									}
								}
								else
								{
									text = (m + 1).ToString().PadLeft(3, '0') + " - ";
									text = ((num - 10 == 1) ? ((!instance.librarystuff[1, listvar[m]]) ? (text + menutext[59]) : (text + enemynames[listvar[m]])) : ((num - 10 == 2 && instance.librarystuff[2, m]) ? (text + itemdata[0, libraryorder[2, m], 0]) : ((num - 10 == 2 || !instance.librarystuff[num - 10, libraryorder[num - 10, (num - 10 == 0 || num - 10 == 3) ? m : listvar[m]]]) ? (text + menutext[59]) : (text + librarydata[num - 10, libraryorder[num - 10, m], 0]))));
									if (languageid == 4 && num - 10 == 0)
									{
										text = "|sizemulti,0.7,1|" + text;
									}
								}
								if (AsianLang() && pausemenu != null)
								{
									text = text.Insert(0, "|size,0.75,y,lock|");
								}
								text = text.Insert(0, "|single|");
								break;
							}
							if (type >= 14 && type <= 16)
							{
								x3 = 0f;
								size = Vector2.one;
								text = boardquestdata[listvar[m], 0];
								if (pausemenu == null)
								{
									if (languageid == 4)
									{
										text = "|size,0.5,0.8,lock|" + text;
									}
									else if (AsianLang())
									{
										text = text.Insert(0, "|size,0.6,0.8,lock|");
									}
								}
								break;
							}
							switch (type)
							{
							case 17:
								text = ((settingsindex[listvar[m]] != 36 || !pausemenu.calledfrommain) ? menutext[settingsindex[listvar[m]]] : menutext[37]);
								if (settingsindex[listvar[m]] != 35 && settingsindex[listvar[m]] != 36 && settingsindex[listvar[m]] != 37 && settingsindex[listvar[m]] != 231 && settingsindex[listvar[m]] != 256)
								{
									float num4 = 3.75f;
									for (int n = 0; n < 2; n++)
									{
										SpriteRenderer component2 = NewUIObject("slider" + n, spriteRenderer2.transform, new Vector3(num4, 0f), Vector3.one, guisprites[1]).GetComponent<SpriteRenderer>();
										component2.sortingOrder = 10;
										if (n == 0)
										{
											component2.transform.localEulerAngles = new Vector3(0f, 0f, -90f);
										}
										else
										{
											component2.transform.localEulerAngles = new Vector3(0f, 0f, 90f);
										}
										num4 += 5f;
									}
								}
								switch (settingsindex[listvar[m]])
								{
								case 33:
								case 34:
								case 160:
								{
									float num6 = 4.45f;
									for (int num7 = 0; num7 < 10; num7++)
									{
SpriteRenderer component3 = NewUIObject(
    "pip",
    spriteRenderer2.transform,
    new Vector3(num6, 0f),
    Vector3.one / 4f,
    guisprites[59]
).GetComponent<SpriteRenderer>();

component3.sortingOrder = 10 + num7;

if ((settingsindex[listvar[m]] == 33 && Mathf.RoundToInt(pausemenu.mvolume * 10f) > num7) ||
    (settingsindex[listvar[m]] == 34 && Mathf.RoundToInt(pausemenu.svolume * 10f) > num7) ||
    (settingsindex[listvar[m]] == 160 && Mathf.RoundToInt(pausemenu.dvolume * 10f) > num7))
{
    component3.sprite = guisprites[42];
    component3.color = Color.yellow;
    component3.transform.localScale = Vector3.one / 3f;
}
										num6 += 0.4f;
									}
									break;
								}
								case 245:
									instance.StartCoroutine(SetText("|center||size,0.75|" + menutext[pausemenu.mash ? 247 : 246], new Vector3(6.25f, -0.15f), spriteRenderer2.transform));
									break;
								case 261:
									instance.StartCoroutine(SetText("|center||size,0.75|" + menutext[pausemenu.monoaudio ? 262 : 263], new Vector3(6.25f, -0.15f), spriteRenderer2.transform));
									break;
								case 30:
								case 31:
								{
									int num5 = 41;
									if ((settingsindex[listvar[m]] == 31 && pausemenu.lowtex) || (settingsindex[listvar[m]] == 30 && pausemenu.lowshadow))
									{
										num5 = 40;
									}
									instance.StartCoroutine(SetText("|center||size,0.75|" + menutext[num5], new Vector3(6.25f, -0.15f), spriteRenderer2.transform));
									break;
								}
								case 282:
								{
									int num5 = 38;
									if (!pausemenu.snap)
									{
										num5 = 39;
									}
									instance.StartCoroutine(SetText("|center||size,0.75|" + menutext[num5], new Vector3(6.25f, -0.15f), spriteRenderer2.transform));
									break;
								}
								case 28:
									instance.StartCoroutine(SetText("|center||size,0.75|" + (int)resolution[pausemenu.resolutionid].x + "x" + (int)resolution[pausemenu.resolutionid].y, new Vector3(6.25f, -0.15f), spriteRenderer2.transform));
									break;
								case 157:
								{
									int num5;
									switch (pausemenu.joystick)
									{
									default:
										num5 = 218 + pausemenu.joystick;
										break;
									case 4:
										num5 = 224;
										break;
									case 5:
										num5 = 230;
										break;
									}
									instance.StartCoroutine(SetText("|center||size,0.75|" + menutext[num5], new Vector3(6.25f, -0.15f), spriteRenderer2.transform));
									break;
								}
								case 222:
								{
									if (pausemenu.joystick == 4)
									{
										instance.StartCoroutine(SetText("|center||size,0.75|" + menutext[precjoystring[pausemenu.joystickid]], new Vector3(6.25f, -0.15f), spriteRenderer2.transform));
										break;
									}
									string text2 = "";
									if (pausemenu.joystickid > -1)
									{
										try
										{
											string text3 = Controllers()[pausemenu.joystickid].Replace("Controller", "Contr.");
											for (int num8 = 0; num8 < 20; num8++)
											{
												if (num8 < text3.Length)
												{
													text2 += text3[num8];
												}
											}
										}
										catch
										{
											pausemenu.joystickid = -1;
										}
									}
									instance.StartCoroutine(SetText("|center||size,0.75|" + ((pausemenu.joystickid == -1) ? menutext[223] : ("|size,0.65,0.75|" + text2)), new Vector3(6.25f, -0.15f), spriteRenderer2.transform));
									break;
								}
								case 239:
									instance.StartCoroutine(SetText("|center||size,0.75|" + menutext[keepmusicafterbattle ? 240 : 241], new Vector3(6.25f, -0.15f), spriteRenderer2.transform));
									break;
								case 29:
								case 183:
								case 255:
								{
									int num5 = 38;
									if ((settingsindex[listvar[m]] == 183 && pausemenu.vsyc == 0) || (settingsindex[listvar[m]] == 29 && !pausemenu.fulls) || (settingsindex[listvar[m]] == 255 && !pausemenu.pauseunfocus))
									{
										num5 = 39;
									}
									instance.StartCoroutine(SetText("|center||size,0.75|" + menutext[num5], new Vector3(6.25f, -0.15f), spriteRenderer2.transform));
									break;
								}
								case 270:
									if (pausemenu.analog > 2)
									{
										pausemenu.analog = 0;
									}
									instance.StartCoroutine(SetText("|center||size,0.75|" + menutext[(new int[3] { 39, 40, 41 })[pausemenu.analog]], new Vector3(6.25f, -0.15f), spriteRenderer2.transform));
									break;
								case 80:
								{
									int num5 = 81;
									if (pausemenu.fps == 1)
									{
										num5 = 82;
									}
									else if (pausemenu.fps == 2)
									{
										num5 = 107;
									}
									instance.StartCoroutine(SetText("|center||size,0.75|" + menutext[num5], new Vector3(6.25f, -0.15f), spriteRenderer2.transform));
									break;
								}
								case 116:
								{
									int num5 = 38;
									if (!MainCamera.GetComponent<FXAA>().enabled)
									{
										num5 = 39;
									}
									instance.StartCoroutine(SetText("|center||size,0.75|" + menutext[num5], new Vector3(6.25f, -0.15f), spriteRenderer2.transform));
									break;
								}
								case 140:
								{
									int num5 = 38;
									if (nowindeffect)
									{
										num5 = 39;
									}
									instance.StartCoroutine(SetText("|center||size,0.75|" + menutext[num5], new Vector3(6.25f, -0.15f), spriteRenderer2.transform));
									break;
								}
								case 147:
								case 156:
								{
									int num5 = 39;
									if ((settingsindex[listvar[m]] == 147 && enableoutline == 1) || (settingsindex[listvar[m]] == 156 && particlelevel == 1))
									{
										num5 = 40;
									}
									else if ((settingsindex[listvar[m]] == 147 && enableoutline == 2) || (settingsindex[listvar[m]] == 156 && particlelevel == 2))
									{
										num5 = 41;
									}
									instance.StartCoroutine(SetText("|center||size,0.75|" + menutext[num5], new Vector3(6.25f, -0.15f), spriteRenderer2.transform));
									break;
								}
								case 32:
									instance.StartCoroutine(SetText("|center||size,0.75|" + ((downsample == 0) ? menutext[39] : (downsamples[downsample] * 100f + "%")), new Vector3(6.25f, -0.15f), spriteRenderer2.transform));
									break;
								}
								text = text.Insert(0, "|single|");
								break;
							case 19:
								text = "|button," + m + ",0| " + ((languageid == 6 && m >= 4 && m < 8) ? "|sizemulti,0.7,1|" : "") + menutext[m + 88];
								break;
							case 30:
							{
								string text4 = "";
								if (m == 0)
								{
									text = menutext[254];
									float num11 = 0f;
									for (int num12 = 0; num12 < 6; num12++)
									{
										Transform transform2 = NewUIObject("ic " + num12, spriteRenderer2.transform, new Vector3(num11, 0f), Vector3.one, GetButtonSpriteD(num12), spriteRenderer2.sortingOrder + 1).transform;
										int num21 = 5;
										num11 = ((m != 0 && m != 5) ? (num11 + 0.5f) : (num11 + 0.75f));
									}
								}
								else if (m - 1 < 4)
								{
									int num13 = 0;
									int num14 = 0;
									switch (m)
									{
									case 1:
										num13 = 166;
										num14 = 167;
										break;
									case 2:
										num13 = 164;
										num14 = 165;
										break;
									case 3:
										num13 = 21;
										num14 = 19;
										break;
									case 4:
										num13 = 12;
										num14 = 20;
										break;
									}
									text = "|icon," + num13 + ",0.5||icon," + num14 + ",0.5||size,0.8,1| " + menutext[m + 249];
									text4 = ((joybinds[m - 1] != -55) ? (menutext[255] + " " + Mathf.Abs(joybinds[m - 1])) : "???");
								}
								else
								{
									int num15 = 13;
									if (pausemenu.joystickid == 1)
									{
										num15 = 86;
									}
									else if (pausemenu.joystickid == 2)
									{
										num15 = 127;
									}
									text = "|icon," + (num15 + m - 5) + ",0.5||size,0.6,1| " + menutext[m + 87];
									text4 = ((joybinds[m - 1] != -55) ? (menutext[256] + " " + Mathf.Abs(joybinds[m - 1])) : "???");
								}
								if (text4.Length > 0)
								{
									instance.StartCoroutine(SetText("|center||size,0.75|" + text4, new Vector3(6.25f, -0.15f), spriteRenderer2.transform));
								}
								break;
							}
							case 20:
								size = Vector2.one;
								text = "|sort,20||color,4|" + languagenames[listvar[m]];
								break;
							case 18:
							{
								x3 = -2.65f;
								if (listvar[m] == -1)
								{
									text = "|color,1|" + commondialogue[152];
									break;
								}
								int[] array6 = instance.samiramusics[m + (listvar.Contains(-1) ? (-1) : 0)];
								text = ((array6[1] != -1) ? ("|color,3|" + musicnames[array6[0]]) : musicnames[array6[0]]);
								break;
							}
							case 33:
							case 34:
								if (listvar[m] > -1)
								{
									bool flag = type == 34 && instance.flags[681];
									text = "|single|" + (flag ? menutext[59] : badgedata[listvar[m], 0]);
									SpriteRenderer spriteRenderer5 = new GameObject("itemsprite").AddComponent<SpriteRenderer>();
									spriteRenderer5.sprite = (flag ? guisprites[190] : itemsprites[1, listvar[m]]);
									spriteRenderer5.transform.parent = spriteRenderer2.transform;
									spriteRenderer5.gameObject.layer = 5;
									y = -0.2f;
									spriteRenderer5.transform.parent = spriteRenderer2.transform;
									spriteRenderer5.transform.localPosition = new Vector2(-2.5f, 0f);
									spriteRenderer5.gameObject.layer = 5;
									spriteRenderer5.transform.localScale = new Vector3(x2, 0.6f, 1f);
								}
								break;
							case 3:
								if (listvar.Length == 0 || listvar[m] > -1)
								{
									int num9 = instance.badges[listvar[m]][0];
									text = "|single|" + badgedata[num9, 0];
									SpriteRenderer spriteRenderer3 = new GameObject("itemsprite").AddComponent<SpriteRenderer>();
									spriteRenderer3.sprite = itemsprites[1, num9];
									spriteRenderer3.transform.parent = spriteRenderer2.transform;
									spriteRenderer3.gameObject.layer = 5;
									if (instance.pause)
									{
										x3 = 0.35f;
										num3 -= 0.2f;
										y = -0.3f;
										spriteRenderer3.transform.localScale = new Vector3(0.7f, 0.7f, 1f);
										spriteRenderer3.transform.localPosition = new Vector2(-0.25f, 0f);
										size = Vector2.one;
										if (instance.badges[listvar[m]][1] > -2)
										{
											SpriteRenderer spriteRenderer4 = new GameObject("Bar" + m).AddComponent<SpriteRenderer>();
											spriteRenderer4.sprite = guisprites[0];
											spriteRenderer4.gameObject.layer = 5;
											spriteRenderer4.transform.parent = spriteRenderer2.transform;
											spriteRenderer4.sortingOrder = -1;
											spriteRenderer4.transform.localPosition = new Vector2(4.3f, 0f);
											spriteRenderer4.transform.localScale = new Vector3(1.8f, 1f, 1f);
											spriteRenderer4.color = new Color(1f, 1f, 1f, 0.5f);
											if (instance.badges[listvar[m]][1] == -1)
											{
												spriteRenderer4.color = instance.menucolors[3];
											}
											else
											{
												int num10 = instance.badges[listvar[m]][1];
												NewUIObject("charicon", spriteRenderer4.transform, new Vector3(1.2f, 0f), new Vector3(0.225f, 0.4f, 1f), guisprites[5 + num10], 10).GetComponent<SpriteRenderer>();
												Color color = instance.charcolor[num10];
												if (num10 == 1)
												{
													color = new Color(color.r - 0.1f, color.g - 0.1f, color.b - 0.1f);
												}
												spriteRenderer4.color = color;
											}
										}
									}
									else
									{
										y = -0.2f;
										spriteRenderer3.transform.parent = spriteRenderer2.transform;
										spriteRenderer3.transform.localPosition = new Vector2(-2.5f, 0f);
										spriteRenderer3.gameObject.layer = 5;
										spriteRenderer3.transform.localScale = new Vector3(x2, 0.6f, 1f);
									}
									if (!sell)
									{
										instance.StartCoroutine(SetText("|font,0|" + Mathf.Clamp(Convert.ToInt32(badgedata[num9, 2]), 0, instance.flags[613] ? 1 : 999), 0, null, false, false, new Vector3(7.5f, -0.2f), Vector3.zero, size, spriteRenderer2.transform, null));
										NewUIObject("mpicon", spriteRenderer2.transform, new Vector3(8.8f, 0f), Vector3.one * 0.4f, guisprites[61], 20);
									}
								}
								else
								{
									text = "|color,1|" + menutext[23];
								}
								break;
							case 9:
							{
								x3 = -2.6f;
								int[] array5 = new int[5] { 1, 271, 2, 0, 3 };
								if ((m == 4 && !battle.canflee) || ((m == 0 || m == 1) && !AllPartyFree() && GetAlivePlayerAmmount() > 1) || (m == 2 && battle.disablespy))
								{
									text = "|color,1|" + menutext[(array5[m] > 3) ? array5[m] : (65 + array5[m])];
									break;
								}
								text = menutext[(array5[m] > 3) ? array5[m] : (65 + array5[m])];
								if (m == 0)
								{
									text += " |size,0.55,0.6||button,6|";
								}
								if (m == 3 && !instance.playerdata[battle.currentturn].didnothing)
								{
									text.Insert(0, "|single|");
									text += " |size,0.55,0.6|";
									if (BadgeIsEquipped(62, instance.playerdata[battle.currentturn].trueid))
									{
										text += "|icon,187|";
									}
									if (BadgeIsEquipped(56, instance.playerdata[battle.currentturn].trueid))
									{
										text += "|icon,188|";
									}
									if (BadgeIsEquipped(61, instance.playerdata[battle.currentturn].trueid))
									{
										text += "|icon,189|";
									}
								}
								if (m == 4)
								{
									text.Insert(0, "|single|");
									text += " |size,0.55,0.6|";
									if (BadgeIsEquipped(48))
									{
										text += "|icon,221|";
									}
									if (BadgeIsEquipped(5))
									{
										text += "|icon,222|";
									}
								}
								if (m == 2 && BadgeIsEquipped(17))
								{
									text.Insert(0, "|single|");
									text += " |size,0.55,0.6||icon,219|";
								}
								break;
							}
							}
							break;
						}
						IL_13ab:
						text5 += " |size,0.55,0.6|";
						if (BadgeIsEquipped(57))
						{
							text5 += "|icon,192|";
						}
						if (BadgeIsEquipped(41))
						{
							text5 += "|icon,191|";
						}
						if (BadgeIsEquipped(22))
						{
							text5 += "|icon,193|";
						}
						goto IL_13fe;
						IL_1425:
						text5 += " |size,0.55,0.6|";
						if (BadgeIsEquipped(74, instance.playerdata[battle.currentturn].trueid))
						{
							text5 += "|icon,218|";
						}
						goto IL_148f;
						IL_13fe:
						text5 += " |size,0.55,0.6|";
						if (BadgeIsEquipped(28))
						{
							text5 += "|icon,194|";
						}
						goto IL_148f;
						IL_148f:
						text = text6 + skilldata[listvar[m], 0] + text5;
						break;
					}
					break;
				}
				}
				instance.StartCoroutine(SetText((instance.pause ? ((type == 3 || type == 32) ? "  " : "") : "|size,0.6,0.8|") + text, 0, null, false, false, new Vector3(x3, y), Vector3.zero, size, spriteRenderer2.transform, null));
			}
			if (instance.maxoptions > listammount && instance.option < instance.maxoptions - 1 && listlow < instance.maxoptions - listammount)
			{
				if (!instance.pause && (type <= 16 || type == 18 || type >= 22 || type == 20))
				{
					SpriteRenderer spriteRenderer8 = new GameObject("DownArrow").AddComponent<SpriteRenderer>();
					spriteRenderer8.gameObject.layer = 5;
					spriteRenderer8.transform.parent = instance.itemlist;
					if (type >= 14 && type <= 16)
					{
						spriteRenderer8.sprite = guisprites[1];
						spriteRenderer8.transform.localPosition = new Vector2(8.8f, num3 + 0.5f);
					}
					else
					{
						spriteRenderer8.transform.localPosition = new Vector2(1f, num3);
						spriteRenderer8.sprite = guisprites[3];
					}
					spriteRenderer8.sortingOrder = 3;
					if (type == 20)
					{
						spriteRenderer8.sortingOrder = 10;
					}
				}
				else if (pausemenu != null)
				{
					SpriteRenderer component4 = NewUIObject("downarrow", null, Vector3.zero, Vector3.one, guisprites[1]).GetComponent<SpriteRenderer>();
					component4.transform.parent = instance.itemlist;
					component4.transform.localEulerAngles = default(Vector3);
					float x4 = 11f;
					if (pausemenu.windowid == 3)
					{
						x4 = 9.5f;
					}
					else if (pausemenu.windowid == 4)
					{
						x4 = 11.25f;
					}
					else if (pausemenu.windowid == 5)
					{
						x4 = 8.5f;
					}
					else if (pausemenu.windowid == 1)
					{
						x4 = 7f;
					}
					component4.transform.localPosition = new Vector2(x4, num3 + 0.5f);
					component4.transform.localScale = Vector3.one * 1.25f;
				}
			}
		}
		if (showdescription && listvar.Length != 0)
		{
			string text7 = "";
			string text8 = "";
			if (listdescbox != null)
			{
				UnityEngine.Object.Destroy(listdescbox.gameObject);
			}
			if (type < 14 || type > 16)
			{
				bool flag4 = false;
				if ((type >= 0 && type < 9) || type == 33 || type == 34)
				{
					if (listvar[instance.option] > -1)
					{
						switch (type)
						{
						case 33:
						case 34:
							text7 = badgedata[listvar[instance.option], 1];
							text8 = badgedata[listvar[instance.option], 0];
							break;
						case 3:
						{
							int num20 = instance.badges[listvar[instance.option]][0];
							text7 = badgedata[num20, 1];
							text8 = badgedata[num20, 0];
							break;
						}
						default:
							if (type == 33)
							{
								text7 = badgedata[listvar[instance.option], 1];
								text8 = badgedata[listvar[instance.option], 0];
							}
							else
							{
								text7 = itemdata[num, listvar[instance.option], 2];
								text8 = itemdata[num, listvar[instance.option], 0];
							}
							break;
						}
					}
					else
					{
						flag4 = true;
					}
				}
				else if (type >= -3 && type <= -1)
				{
					text7 = skilldata[listvar[instance.option], 1];
				}
				else
				{
					switch (type)
					{
					case 9:
					{
						int[] array8 = new int[5] { 1, 272, 2, 0, 3 };
						text7 = menutext[(array8[instance.option] > 3) ? array8[instance.option] : (69 + array8[instance.option])];
						break;
					}
					case 17:
						text7 = menutext[0];
						break;
					case 26:
						text7 = ((termacadeprize[listvar[instance.option], 0] != 2) ? itemdata[0, termacadeprize[listvar[instance.option], 1], 2] : ((!instance.flags[681]) ? badgedata[termacadeprize[listvar[instance.option], 1], 1] : menutext[59]));
						break;
					}
				}
				if (!flag4)
				{
					Transform transform = Create9Box(Vector3.zero, new Vector2(11f, 3f), 0, -3, Color.white, true);
					transform.parent = instance.itemlist;
					transform.transform.localEulerAngles = Vector3.zero;
					transform.transform.localPosition = new Vector3(-7.5f, -4.25f);
					if (sell)
					{
						instance.showmoney = 10f;
						instance.flagvar[10] = Mathf.Clamp(Mathf.FloorToInt(Convert.ToInt32(itemdata[listtype, listvar[instance.option], 4]) / 2), 1, 999);
						text7 = text8 + " - " + menutext[49] + "|line|" + text7;
					}
					instance.StartCoroutine(SetText("|single||singlebreak," + itemdescbreak + "|" + text7, 0, null, false, false, new Vector3(-5.25f, 0.6f), Vector3.zero, new Vector2(0.75f, 0.75f), transform, null));
					listdescbox = transform;
				}
			}
			else
			{
				listdescbox = NewChild("descbox", instance.itemlist);
				if (listvar[instance.option] != 0)
				{
					NewUIObject("Image", listdescbox, new Vector3(10.5f, 0f), Vector3.one * 2f, librarysprites[Convert.ToInt32(boardquestdata[listvar[instance.option], 4])], 10).tag = "Text";
					instance.StartCoroutine(SetText("|size,0.75||sort,1|" + menutext[104] + " " + boardquestdata[listvar[instance.option], 2] + "|line||halfline|" + menutext[105] + " |stars," + boardquestdata[listvar[instance.option], 5] + "|", new Vector3(12f, 0.35f), listdescbox));
				}
				instance.StartCoroutine(SetText("|sort,1||single|" + ((languageid == 4) ? "|singlebreak,10||sizemulti,0.8,1|" : "|singlebreak,6|") + boardquestdata[listvar[instance.option], 1].Split('}')[0].Split('{')[0], 0, null, false, false, new Vector3(9.9f, -1.75f), Vector3.zero, new Vector2(0.65f, 0.75f), listdescbox, null));
			}
		}
		instance.itemlist.localEulerAngles = Vector3.zero;
	}

	public static Vector3 ChildScale(Vector3 scale, Transform parent, bool swapZY)
	{
		if (swapZY)
		{
			return new Vector3(scale.x / parent.localScale.x, scale.y / parent.localScale.z, scale.z / parent.localScale.y);
		}
		return new Vector3(scale.x / parent.localScale.x, scale.y / parent.localScale.y, scale.z / parent.localScale.z);
	}

	public static Transform NewChild(string name, Transform parent)
	{
		Transform obj = new GameObject(name).transform;
		obj.parent = parent;
		obj.localPosition = Vector3.zero;
		obj.localEulerAngles = Vector3.zero;
		obj.localScale = Vector3.one;
		return obj;
	}

	public static bool HasQuest(int questid)
	{
		for (int i = 0; i < instance.boardquests.Length; i++)
		{
			if (instance.boardquests[i].Contains(questid))
			{
				return true;
			}
		}
		return false;
	}

	public static int GetFreePlayerAmmount()
	{
		int num = 0;
		for (int i = 0; i < instance.playerdata.Length; i++)
		{
			if (instance.playerdata[i].cantmove <= 0 && instance.playerdata[i].hp > 0 && HasCondition(BattleCondition.Sleep, instance.playerdata[i]) <= 0 && HasCondition(BattleCondition.Numb, instance.playerdata[i]) <= 0 && HasCondition(BattleCondition.Freeze, instance.playerdata[i]) <= 0 && HasCondition(BattleCondition.EventStop, instance.playerdata[i]) <= 0 && HasCondition(BattleCondition.Eaten, instance.playerdata[i]) <= 0)
			{
				num++;
			}
		}
		return num;
	}

	public static Vector3 CardinalSnap(Vector3 angle, bool cameratied)
	{
		Vector3 zero = Vector3.zero;
		zero = ((angle.y > 45f && angle.y < 135f) ? Vector3.left : ((angle.y >= 135f && angle.y < 225f) ? Vector3.forward : ((!(angle.y >= 225f) || !(angle.y < 315f)) ? Vector3.back : Vector3.right)));
		if (cameratied)
		{
			return (zero + instance.globalcamdir.forward.normalized).normalized;
		}
		return zero;
	}

	public static Vector3 CardinalSnap8(Vector3 angle, bool cameratied)
	{
		Vector3 zero = Vector3.zero;
		zero = ((angle.y > 22.5f && angle.y < 67.5f) ? (Vector3.left + Vector3.back) : ((angle.y >= 67.5f && angle.y < 112.5f) ? Vector3.left : ((angle.y >= 122.5f && angle.y < 157.5f) ? (Vector3.left + Vector3.forward) : ((angle.y >= 157.5f && angle.y < 202.5f) ? Vector3.forward : ((angle.y >= 202.5f && angle.y < 247.5f) ? (Vector3.right + Vector3.forward) : ((angle.y >= 247.5f && angle.y < 292.5f) ? Vector3.right : ((!(angle.y >= 292.5f) || !(angle.y < 337.5f)) ? Vector3.back : (Vector3.right + Vector3.back))))))));
		if (cameratied)
		{
			return (zero + instance.globalcamdir.forward.normalized).normalized;
		}
		return zero.normalized;
	}

	public static float ClampToMinMax(float v, float min, float max)
	{
		return ClampToMinMax(v, min, max, false);
	}

	public static float ClampToMinMax(float v, float min, float max, bool lowest)
	{
		float num = Mathf.Lerp(min, max, 0.5f);
		if (lowest)
		{
			if (v < num)
			{
				return min;
			}
			return max;
		}
		if (v < num)
		{
			return max;
		}
		return min;
	}

	public static bool InBetween(float v, float a, float b)
	{
		if (v >= a)
		{
			return v < b;
		}
		return false;
	}

	public static Vector3 CardinalSnap(Vector3 obj, int directions)
	{
		float num = 360f / (float)directions;
		return new Vector3(obj.x, (float)Mathf.RoundToInt(obj.y / num) * num, obj.z);
	}

	public static void LookAt(Transform obj, Vector3 targetp)
	{
		obj.LookAt(targetp);
		obj.localEulerAngles = new Vector3(0f, obj.localEulerAngles.y, 0f);
	}

	public static void LookAt(Transform obj, Vector3 targetp, bool keepangle)
	{
		if (!keepangle)
		{
			LookAt(obj, targetp);
		}
		Vector3 localEulerAngles = obj.localEulerAngles;
		obj.LookAt(targetp);
		obj.localEulerAngles = new Vector3(localEulerAngles.x, obj.localEulerAngles.y, localEulerAngles.z);
	}

	public static Vector3 CardinalSnap(Vector3 angle)
	{
		return CardinalSnap(angle, false);
	}

	public static float CardinalSnap(float angle)
	{
		return CardinalSnap(angle, 4);
	}

	public static Vector3 CardinalSnap(Transform obj, int directions)
	{
		return CardinalSnap(obj.transform.eulerAngles, directions);
	}

	public static Vector3 CardinalSnap(Transform obj)
	{
		return CardinalSnap(obj, 4);
	}

	public static float CardinalSnap(float angle, int directions)
	{
		float num = 360 / directions;
		return (float)Mathf.RoundToInt(angle / num) * num;
	}

	public static int GetFreePlayerAmmount(bool hponly)
	{
		if (hponly)
		{
			int num = 0;
			for (int i = 0; i < instance.playerdata.Length; i++)
			{
				if (instance.playerdata[i].hp > 0)
				{
					num++;
				}
			}
			return num;
		}
		return GetFreePlayerAmmount();
	}

	public static int GetAlivePlayerAmmount()
	{
		int num = 0;
		for (int i = 0; i < instance.playerdata.Length; i++)
		{
			if (instance.playerdata[i].hp > 0 && instance.playerdata[i].eatenby == null)
			{
				num++;
			}
		}
		return num;
	}

	public static void ApplySettings()
	{
		if (music != null)
		{
			for (int i = 0; i < music.Length; i++)
			{
				if (instance.inmusicrange == -1)
				{
					music[i].volume = musicvolume;
				}
			}
		}
		if (sounds != null)
		{
			for (int j = 0; j < sounds.Length; j++)
			{
				sounds[j].volume = soundvolume;
			}
		}
		QualitySettings.vSyncCount = ((vsync != 0) ? Mathf.Clamp(Mathf.FloorToInt((float)Screen.currentResolution.refreshRate / 60f), 1, 4) : 0);
		if (fps == 0)
		{
			Application.targetFrameRate = 30;
		}
		else if (fps == 1)
		{
			Application.targetFrameRate = 60;
		}
		else if (fps == 2)
		{
			Application.targetFrameRate = -1;
		}
		if (lowshadows)
		{
			QualitySettings.shadowResolution = ShadowResolution.Low;
		}
		else
		{
			QualitySettings.shadowResolution = ShadowResolution.High;
		}
		if (lowtexture)
		{
			QualitySettings.masterTextureLimit = 1;
		}
		else
		{
			QualitySettings.masterTextureLimit = 0;
		}
		if (usejoystick == 5)
		{
			joyid = forcejoystick;
		}
		else if ((forcejoystick > -1 && usejoystick == 3) || (usejoystick > 0 && joystick))
		{
			InputIO.GetJoyButtons();
			forcecontrollerupdate = true;
		}
		if (AudioSettings.speakerMode != (AudioSpeakerMode)(monoaudio ? 1 : 2))
		{
			string text = ((music[0].clip != null && music[0].isPlaying) ? music[0].clip.name : null);
			float time = ((text != null) ? music[0].time : 0f);
			AudioSettings.speakerMode = (monoaudio ? AudioSpeakerMode.Mono : AudioSpeakerMode.Stereo);
			if (text != null)
			{
				ChangeMusic(text);
				music[0].Play();
				music[0].time = time;
			}
		}
		if (!InputIO.IsConsole)
		{
			Application.runInBackground = !pauseonfocus;
			if (Application.isMobilePlatform)
			{
				Screen.SetResolution(Screen.resolutions[0].width, Screen.resolutions[0].height, false);
			}
			else
			{
				Screen.SetResolution((int)resolution[resolutionindex].x, (int)resolution[resolutionindex].y, fullscreen);
			}
		}
		else
		{
			Application.runInBackground = false;
		}
		if (map != null)
		{
			map.RefreshSoundVolume();
		}
		if (analog > 2)
		{
			analog = 0;
		}
	}

	public static bool AllPartyFree()
	{
		for (int i = 0; i < instance.playerdata.Length; i++)
		{
			if (instance.playerdata[i].cantmove > 0)
			{
				return false;
			}
		}
		return true;
	}

	public static int BadgeHowManyEquipped(int id)
	{
		return BadgeHowManyEquipped(id, -1);
	}

	public static int BadgeHowManyEquipped(int id, int playerid)
	{
		int num = 0;
		for (int i = 0; i < instance.badges.Count; i++)
		{
			if (instance.badges[i][0] == id && instance.badges[i][1] > -2 && playerid == instance.badges[i][1])
			{
				num++;
			}
		}
		return num;
	}

	public static bool BadgeIsEquipped(int id)
	{
		return BadgeIsEquipped(id, -1);
	}

	public static bool CheckActiveEntities(int[] ids)
	{
		for (int i = 0; i < ids.Length; i++)
		{
			if (ids[i] >= 0)
			{
				if (!map.entities[ids[i]].npcdata.hit)
				{
					return false;
				}
			}
			else if (map.entities[Mathf.Abs(ids[i])].npcdata.hit)
			{
				return false;
			}
		}
		return true;
	}

	public static bool BadgeIsEquipped(int id, int playerid)
	{
		bool result = false;
		for (int i = 0; i < instance.badges.Count; i++)
		{
			if (instance.badges[i][0] == id && instance.badges[i][1] > -2)
			{
				if (playerid == -1)
				{
					return true;
				}
				if (playerid == instance.badges[i][1])
				{
					result = true;
				}
			}
		}
		return result;
	}

	public static void ReloadSave()
	{
		StopSound("CrowdChatter");
		FadeMusic(0.1f);
		if (battle != null)
		{
			battle.StopAllCoroutines();
			UnityEngine.Object.Destroy(battle.battlemap.gameObject);
		}
		events.StopAllCoroutines();
		for (int i = 0; i < instance.playerdata.Length; i++)
		{
			if (instance.playerdata[i].entity != null)
			{
				UnityEngine.Object.Destroy(instance.playerdata[i].entity.gameObject);
			}
		}
		RenderSettings.skybox = null;
		if (map != null)
		{
			UnityEngine.Object.Destroy(map.gameObject);
		}
		events.StartEvent(22, null);
	}

	public static void ApplyBadges()
	{
		instance.speedup = true;
		instance.maxtp = instance.basetp;
		for (int i = 0; i < instance.playerdata.Length; i++)
		{
			ResetPlayerStat(i);
		}
		int[][] array = instance.badges.ToArray();
		for (int j = 0; j < array.GetLength(0); j++)
		{
			if (array[j][1] <= -2)
			{
				continue;
			}
			string[] array2 = badgedata[array[j][0], 4].Split(';');
			for (int k = 0; k < array2.Length; k++)
			{
				string[] array3 = array2[k].Split(',');
				if (array[j][1] > -1)
				{
					for (int l = 0; l < instance.playerdata.Length; l++)
					{
						if (instance.playerdata[l].trueid == array[j][1])
						{
							switch ((BadgeEffects)Enum.Parse(typeof(BadgeEffects), array3[0]))
							{
							case BadgeEffects.HPUP:
								instance.playerdata[l].maxhp += Convert.ToInt32(array3[1]);
								break;
							case BadgeEffects.AttackMultiply:
								instance.playerdata[l].atk *= Mathf.FloorToInt(Convert.ToSingle(array3[1]));
								break;
							case BadgeEffects.AttackUp:
								instance.playerdata[l].atk += Convert.ToInt32(array3[1]);
								break;
							case BadgeEffects.DefenseUp:
								instance.playerdata[l].def += Convert.ToInt32(array3[1]);
								break;
							case BadgeEffects.LockItems:
								instance.playerdata[l].lockitems = true;
								break;
							case BadgeEffects.LockSkills:
								instance.playerdata[l].lockskills = true;
								break;
							case BadgeEffects.LockRelayPass:
								instance.playerdata[l].lockrelayreceive = true;
								break;
							case BadgeEffects.LockRelay:
								instance.playerdata[l].locktri = true;
								break;
							case BadgeEffects.SleepRes:
								instance.playerdata[l].sleepres += Convert.ToInt32(array3[1]);
								break;
							case BadgeEffects.PoisonRes:
								instance.playerdata[l].poisonres += Convert.ToInt32(array3[1]);
								break;
							case BadgeEffects.NumbRes:
								instance.playerdata[l].numbres += Convert.ToInt32(array3[1]);
								break;
							case BadgeEffects.FreezeRes:
								instance.playerdata[l].freezeres += Convert.ToInt32(array3[1]);
								break;
							}
						}
					}
					continue;
				}
				switch ((BadgeEffects)Enum.Parse(typeof(BadgeEffects), array3[0]))
				{
				case BadgeEffects.TPUP:
					instance.maxtp += Convert.ToInt32(array3[1]);
					break;
				case BadgeEffects.SpeedUp:
					instance.speedup = true;
					break;
				case BadgeEffects.AttackUp:
				{
					for (int m = 0; m < instance.playerdata.Length; m++)
					{
						instance.playerdata[m].atk += Convert.ToInt32(array3[1]);
					}
					break;
				}
				}
			}
		}
		instance.tp = Mathf.Clamp(instance.tp, 0, instance.maxtp);
		for (int n = 0; n < instance.playerdata.Length; n++)
		{
			instance.playerdata[n].hp = Mathf.Clamp(instance.playerdata[n].hp, 0, instance.playerdata[n].maxhp);
		}
	}

	public static GameObject CreateRock(Vector3 pos, Vector3 size, Vector3 rotation)
	{
		GameObject obj = UnityEngine.Object.Instantiate(Resources.Load("Prefabs/Objects/CrackedRock")) as GameObject;
		obj.transform.position = pos;
		obj.transform.localEulerAngles = rotation;
		obj.transform.localScale = size;
		UnityEngine.Object.Destroy(obj.GetComponent<MeshCollider>());
		UnityEngine.Object.Destroy(obj.GetComponent<Fader>());
		return obj;
	}

	public static void CrackRock(Transform parent, bool destroyparent)
	{
		(UnityEngine.Object.Instantiate(Resources.Load("Prefabs/Objects/CrackRockBreak"), parent.position, Quaternion.Euler(parent.eulerAngles)) as GameObject).transform.localScale = parent.localScale;
		if (destroyparent)
		{
			UnityEngine.Object.Destroy(parent.gameObject);
		}
	}

	public static void AddBadge(int id)
	{
		instance.badges.Add(new int[2] { id, -2 });
	}

	public static void SetUpList(int type, bool showdescription, bool sell)
	{
		storeid = 0;
		listtype = type;
		listammount = 6;
		listdesc = showdescription;
		listsell = sell;
		instance.inputcooldown = 5f;
	}

	private static void ResetPlayerStat(int id)
	{
		instance.playerdata[id].lockitems = false;
		instance.playerdata[id].lockskills = false;
		instance.playerdata[id].locktri = false;
		instance.playerdata[id].lockrelayreceive = false;
		instance.playerdata[id].maxhp = instance.playerdata[id].basehp;
		instance.playerdata[id].atk = instance.playerdata[id].baseatk;
		instance.playerdata[id].def = instance.playerdata[id].basedef;
		instance.playerdata[id].poisonres = 0;
		instance.playerdata[id].sleepres = 0;
		instance.playerdata[id].freezeres = 0;
		instance.playerdata[id].numbres = 0;
	}

	public static void ResetList()
	{
		listcursor = 0;
		instance.option = 0;
		listlow = 0;
		listmax = listammount;
		listY = -1;
	}

	public static int[] SaveList()
	{
		return new int[5] { instance.option, listcursor, listlow, listmax, listoption };
	}

	public static void LoadList(int[] v)
	{
		instance.option = v[0];
		listcursor = v[1];
		listlow = v[2];
		listmax = v[3];
		listoption = v[4];
	}

	public static LoadData? Load(int file, bool lite)
	{
		try
		{
			string[] array = new string[1] { "" };
			if (InputIO.IsConsole)
			{
				array = InputIO.ReadFile("save" + file + ".dat").Split('\n');
			}
			else
			{
				try
				{
					if (!Directory.Exists("Saves"))
					{
						Directory.CreateDirectory("Saves");
						for (int i = 0; i < 3; i++)
						{
							if (File.Exists("save" + i + ".dat"))
							{
								File.Move("save" + i + ".dat", "Saves/save" + i + ".dat");
							}
						}
					}
					array = InputIO.Encrypt(InputIO.ReadFile("Saves/save" + file + ".dat")).Split('\n');
				}
				catch
				{
					return null;
				}
			}
			LoadData value = default(LoadData);
			string[] array2 = array[0].Split(',');
			if (!lite)
			{
				value.loadpos = new Vector3(Convert.ToSingle(array2[0]), Convert.ToSingle(array2[1]), Convert.ToSingle(array2[2]));
				array2 = array[7].Split('@');
				instance.badges = new List<int[]>();
				for (int j = 0; j < array2.Length; j++)
				{
					string[] array3 = array2[j].Split(',');
					List<int> list = new List<int>();
					if (array3.Length != 0)
					{
						for (int k = 0; k < array3.Length; k++)
						{
							if (array3[k] != "")
							{
								list.Add(Convert.ToInt32(array3[k]));
							}
						}
					}
					if (list.Count > 0)
					{
						instance.badges.Add(list.ToArray());
					}
				}
				array2 = array[9].Split('@');
				instance.statbonus = new List<int[]>();
				for (int l = 0; l < array2.Length; l++)
				{
					string[] array4 = array2[l].Split(',');
					List<int> list2 = new List<int>();
					if (array4.Length != 0)
					{
						for (int m = 0; m < array4.Length; m++)
						{
							if (array4[m] != "")
							{
								list2.Add(Convert.ToInt32(array4[m]));
							}
						}
					}
					if (list2.Count > 0)
					{
						instance.statbonus.Add(list2.ToArray());
					}
				}
				array2 = array[1].Split('@');
				int[] array5 = new int[array2.Length];
				for (int n = 0; n < array5.Length; n++)
				{
					array5[n] = Convert.ToInt32(array2[n].Split(',')[0]);
				}
				ChangeParty(array5, false);
				instance.playerdata = new BattleData[array5.Length];
				for (int num = 0; num < array2.Length; num++)
				{
					string[] array6 = array2[num].Split(',');
					instance.playerdata[num].trueid = array5[num];
					instance.playerdata[num].animid = array5[num];
					instance.playerdata[num].hp = Convert.ToInt32(array6[1]);
					instance.playerdata[num].maxhp = Convert.ToInt32(array6[2]);
					instance.playerdata[num].basehp = Convert.ToInt32(array6[3]);
					instance.playerdata[num].atk = Convert.ToInt32(array6[4]);
					instance.playerdata[num].baseatk = Convert.ToInt32(array6[5]);
					instance.playerdata[num].def = Convert.ToInt32(array6[6]);
					instance.playerdata[num].basedef = Convert.ToInt32(array6[7]);
					instance.playerdata[num].entityname = menutext[46 + array5[num]];
				}
			}
			else
			{
				value.challenges = new bool[6];
				try
				{
					if (array2.Length > 3)
					{
						for (int num2 = 0; num2 < value.challenges.Length; num2++)
						{
							value.challenges[num2] = array2[num2 + 3] == "True";
						}
					}
					if (array2.Length < 10)
					{
						value.filename = "|color,1|SLOT " + (file + 1) + " - NO FILE NAME";
					}
					else
					{
						value.filename = array2[9];
					}
				}
				catch
				{
					value.filename = "|color,1|SLOT " + (file + 1) + " - NO FILE NAME";
				}
			}
			array2 = array[2].Split(',');
			if (!lite)
			{
				instance.partylevel = Convert.ToInt32(array2[0]);
				instance.partyexp = Convert.ToInt32(array2[1]);
				instance.neededexp = Convert.ToInt32(array2[2]);
				instance.basetp = Convert.ToInt32(array2[3]);
				instance.tp = Convert.ToInt32(array2[4]);
				instance.money = Convert.ToInt32(array2[5]);
				instance.bp = Convert.ToInt32(array2[8]);
				instance.maxbp = Convert.ToInt32(array2[9]);
				instance.maxitems = Convert.ToInt32(array2[10]);
				instance.maxstorage = Convert.ToInt32(array2[11]);
				instance.clockhour = Convert.ToInt32(array2[12]);
				instance.clockmin = Convert.ToInt32(array2[13]);
				instance.clocksec = Convert.ToInt32(array2[14]);
				instance.areaid = Convert.ToInt32(array2[7]);
			}
			value.level = Convert.ToInt32(array2[0]);
			value.mapid = Convert.ToInt32(array2[6]);
			value.areaid = Convert.ToInt32(array2[7]);
			value.timeh = Convert.ToInt32(array2[12]);
			value.timem = Convert.ToInt32(array2[13]);
			value.times = Convert.ToInt32(array2[14]);
			value.progression = Convert.ToInt32(array2[15]);
			if (!lite)
			{
				array2 = array[3].Split('@');
				instance.avaliablebadgepool = new List<int>[array2.Length];
				for (int num3 = 0; num3 < array2.Length; num3++)
				{
					instance.avaliablebadgepool[num3] = new List<int>();
					string[] array7 = array2[num3].Split(',');
					if (array7.Length == 0)
					{
						continue;
					}
					for (int num4 = 0; num4 < array7.Length; num4++)
					{
						if (array7[num4] != "")
						{
							instance.avaliablebadgepool[num3].Add(Convert.ToInt32(array7[num4]));
						}
					}
				}
				array2 = array[4].Split('@');
				instance.badgeshops = new List<int>[array2.Length];
				for (int num5 = 0; num5 < array2.Length; num5++)
				{
					instance.badgeshops[num5] = new List<int>();
					string[] array8 = array2[num5].Split(',');
					if (array8.Length == 0)
					{
						continue;
					}
					for (int num6 = 0; num6 < array8.Length; num6++)
					{
						if (array8[num6] != "")
						{
							instance.badgeshops[num5].Add(Convert.ToInt32(array8[num6]));
						}
					}
				}
				array2 = array[5].Split('@');
				instance.boardquests = new List<int>[array2.Length];
				for (int num7 = 0; num7 < array2.Length; num7++)
				{
					instance.boardquests[num7] = new List<int>();
					string[] array9 = array2[num7].Split(',');
					if (array9.Length == 0)
					{
						continue;
					}
					for (int num8 = 0; num8 < array9.Length; num8++)
					{
						if (array9[num8] != "")
						{
							instance.boardquests[num7].Add(Convert.ToInt32(array9[num8]));
						}
					}
				}
				array2 = array[6].Split('@');
				instance.items = new List<int>[array2.Length];
				for (int num9 = 0; num9 < array2.Length; num9++)
				{
					instance.items[num9] = new List<int>();
					string[] array10 = array2[num9].Split(',');
					if (array10.Length == 0)
					{
						continue;
					}
					for (int num10 = 0; num10 < array10.Length; num10++)
					{
						if (array10[num10] != "")
						{
							instance.items[num9].Add(Convert.ToInt32(array10[num10]));
						}
					}
				}
				array2 = array[8].Split('@');
				instance.samiramusics = new List<int[]>();
				for (int num11 = 0; num11 < array2.Length; num11++)
				{
					string[] array11 = array2[num11].Split(',');
					List<int> list3 = new List<int>();
					if (array11.Length != 0)
					{
						for (int num12 = 0; num12 < array11.Length; num12++)
						{
							if (array11[num12] != "")
							{
								list3.Add(Convert.ToInt32(array11[num12]));
							}
						}
					}
					if (list3.Count > 0)
					{
						instance.samiramusics.Add(list3.ToArray());
					}
				}
				array2 = array[10].Split('@');
				instance.librarystuff = new bool[array2.Length, 256];
				for (int num13 = 0; num13 < array2.Length; num13++)
				{
					string[] array12 = array2[num13].Split(',');
					for (int num14 = 0; num14 < array12.Length; num14++)
					{
						instance.librarystuff[num13, num14] = Convert.ToBoolean(array12[num14]);
					}
				}
				array2 = array[11].Split(',');
				instance.flags = new bool[750];
				for (int num15 = 0; num15 < array2.Length; num15++)
				{
					if (num15 < instance.flags.Length)
					{
						instance.flags[num15] = Convert.ToBoolean(array2[num15]);
					}
				}
				array2 = array[12].Replace("|SPLIT|", "¬").Split('¬');
				instance.flagstring = new string[15];
				for (int num16 = 0; num16 < instance.flagstring.Length; num16++)
				{
					if (num16 < array2.Length)
					{
						instance.flagstring[num16] = array2[num16];
					}
				}
				if (instance.flagstring.Length <= 2)
				{
					instance.flagstring = new string[15];
				}
				array2 = array[13].Split(',');
				instance.flagvar = new int[70];
				for (int num17 = 0; num17 < array2.Length; num17++)
				{
					instance.flagvar[num17] = Convert.ToInt32(array2[num17]);
				}
				array2 = array[14].Split(',');
				instance.regionalflags = new bool[array2.Length];
				for (int num18 = 0; num18 < array2.Length; num18++)
				{
					instance.regionalflags[num18] = Convert.ToBoolean(array2[num18]);
				}
				array2 = array[15].Split(',');
				instance.crystalbflags = new bool[array2.Length];
				for (int num19 = 0; num19 < array2.Length; num19++)
				{
					instance.crystalbflags[num19] = Convert.ToBoolean(array2[num19]);
				}
				array2 = array[16].Split(',');
				instance.extrafollowers = new List<int>();
				for (int num20 = 0; num20 < array2.Length; num20++)
				{
					if (array2[num20] != "")
					{
						instance.extrafollowers.Add(Convert.ToInt32(array2[num20]));
					}
				}
				array2 = array[17].Split('@');
				instance.enemyencounter = new int[array2.Length, array2[0].Split(',').Length];
				for (int num21 = 0; num21 < array2.Length; num21++)
				{
					string[] array13 = array2[num21].Split(',');
					for (int num22 = 0; num22 < array13.Length; num22++)
					{
						instance.enemyencounter[num21, num22] = Convert.ToInt32(array13[num22]);
					}
				}
				ApplyBadges();
			}
			return value;
		}
		catch
		{
			Debug.Log("Save file " + file + " is corrupted.");
		}
		return null;
	}

	public static float[] Divisions(int divisions)
	{
		float num = 1f / (float)(divisions + 1);
		List<float> list = new List<float>();
		for (int i = 1; i <= divisions; i++)
		{
			list.Add(num * (float)i);
		}
		return list.ToArray();
	}

	public static IEnumerator LatePos(Transform obj, Vector3 pos, float delay, bool keeptrying)
	{
		if (keeptrying)
		{
			for (float i = 0f; i < delay; i += framestep)
			{
				if (obj != null)
				{
					obj.position = pos;
				}
				yield return null;
			}
		}
		else
		{
			yield return new WaitForSeconds(delay);
			if (obj != null)
			{
				obj.position = pos;
			}
		}
	}

	public static int SaveProgressIcons()
	{
		int num = 0;
		if (instance.flags[41])
		{
			num++;
		}
		if (instance.flags[88])
		{
			num++;
		}
		if (instance.flags[299])
		{
			num++;
		}
		if (instance.flags[345])
		{
			num++;
		}
		if (instance.flags[347])
		{
			num++;
		}
		if (instance.flags[346])
		{
			num++;
		}
		if (instance.flags[555])
		{
			num++;
		}
		return num;
	}

	public static bool Save(Vector3? savepos)
	{
		return InputIO.Save(savepos);
	}

	public static string PromptYesNo(int yes, int no)
	{
		return "|prompt,map,0.5,2," + yes + "," + no + ",@" + menutext[5] + ",@" + menutext[6] + "|";
	}

	public static string PromptYesNo()
	{
		return PromptYesNo(-11, -11);
	}

	private static void GetRewardTokens(int score)
	{
		if (!battleresult)
		{
			instance.flagvar[1] = Mathf.FloorToInt((float)score / 200f);
		}
		else if (instance.flagvar[6] == 0)
		{
			instance.flagvar[1] = Mathf.FloorToInt((float)score / 95f);
		}
		else
		{
			instance.flagvar[1] = Mathf.FloorToInt((float)score / 140f);
		}
		instance.flagvar[1] = Mathf.Clamp(instance.flagvar[1], 2, 999999);
	}

	public static bool AnyKeyButThis(int id, bool hold)
	{
		for (int i = 0; i < InputIO.keys.Length; i++)
		{
			if (i != id && GetKey(i, hold))
			{
				return true;
			}
		}
		return false;
	}

	public static IEnumerator TransferMap(int targetmap, Vector3 targetpos)
	{
		instance.StartCoroutine(TransferMap(targetmap, player.transform.position, targetpos, targetpos, null));
		yield return null;
	}

	public static IEnumerator TransferMap(int targetmap, Vector3 moveto, Vector3 tppos, Vector3 othermovepos)
	{
		instance.StartCoroutine(TransferMap(targetmap, moveto, tppos, othermovepos, null));
		yield return null;
	}

	public static IEnumerator TransferMap(int targetmap, Vector3 moveto, Vector3 tppos, Vector3 othermovepos, NPCControl caller)
	{
		roomtransition = true;
		Vector3[] tdata = null;
		int[] tdataa = null;
		float jump = 0f;
		if (caller != null)
		{
			if (caller.data.Length > 1)
			{
				tdata = new Vector3[4]
				{
					caller.vectordata[3],
					caller.vectordata[4],
					caller.vectordata[5],
					caller.vectordata[6]
				};
				tdataa = caller.data;
			}
			jump = caller.entity.emoticonoffset.x;
		}
		if (map != null && map.entities != null)
		{
			for (int i = 0; i < map.entities.Length; i++)
			{
				if (map.entities[i] != null)
				{
					map.entities[i].BreakIce();
					if (map.entities[i].npcdata != null && map.entities[i].npcdata.boxcol != null)
					{
						map.entities[i].npcdata.boxcol.enabled = false;
					}
				}
			}
		}
		instance.minipause = true;
		player.CancelAction();
		while (pausemenu != null)
		{
			yield return null;
		}
		PlayTransition(0, 0, 0.1f, Color.black);
		if (!(caller != null) || caller.data.Length < 5 || caller.data[4] != 1)
		{
			player.entity.MoveTowards(moveto, 1f, 1, 0, true);
			instance.camtargetpos = player.transform.position;
			instance.camtarget = null;
			while (player.entity.forcemove)
			{
				yield return null;
			}
		}
		else
		{
			for (int j = 0; j < instance.playerdata.Length; j++)
			{
				instance.playerdata[j].entity.rigid.useGravity = false;
				instance.playerdata[j].entity.rigid.isKinematic = true;
			}
			yield return null;
		}
		SpriteRenderer t = instance.transitionobj[0].GetComponent<SpriteRenderer>();
		while (t.color.a < 0.9f)
		{
			yield return null;
		}
		t.color = new Color(t.color.r, t.color.g, t.color.b, 1f);
		yield return null;
		LoadMap(targetmap);
		if (!instance.inevent)
		{
			instance.insideid = -1;
		}
		instance.globalcooldown = 30f;
		instance.inmusicrange = -1;
		yield return null;
		MainCamera.transform.position = instance.playerdata[0].entity.transform.position + instance.camoffset;
		float tempsped = instance.camspeed;
		instance.camspeed = 1f;
		yield return null;
		for (int k = 0; k < instance.playerdata.Length; k++)
		{
			if (instance.playerdata[k].entity != null)
			{
				instance.playerdata[k].entity.transform.position = tppos + MainCamera.transform.forward * ((float)k / 10f);
				instance.playerdata[k].entity.rigid.velocity = Vector3.zero;
				instance.playerdata[k].entity.rigid.useGravity = true;
				instance.playerdata[k].entity.rigid.isKinematic = false;
				instance.playerdata[k].entity.onground = false;
			}
		}
		yield return null;
		for (int l = 0; l < map.entities.Length; l++)
		{
			if (map.entities[l] != null)
			{
				map.entities[l].CheckNear();
			}
		}
		yield return null;
		instance.ForceLoadSprites();
		yield return new WaitForSeconds(0.1f);
		for (int m = 0; m < instance.playerdata.Length; m++)
		{
			instance.playerdata[m].entity.FaceTowards(othermovepos);
		}
		instance.camtarget = player.transform;
		if (tdataa != null && tdataa.Length > 1)
		{
			if (tdataa[1] == 1)
			{
				instance.camoffset = tdata[0];
			}
			if (tdataa[2] == 1)
			{
				instance.camangleoffset = tdata[1];
			}
			if (tdataa[3] == 1)
			{
				map.camlimitpos = tdata[2];
				map.camlimitneg = tdata[3];
			}
		}
		yield return new WaitForSeconds(0.3f);
		instance.camspeed = tempsped;
		yield return null;
		PlayTransition(1, 0, 0.1f, Color.black);
		yield return null;
		if (jump > 0.1f)
		{
			player.jumproutine = player.StartCoroutine(player.JumpTo(othermovepos, jump));
			while (player.jumproutine != null)
			{
				for (int n = 0; n < instance.playerdata.Length; n++)
				{
					instance.playerdata[n].entity.animstate = 3;
					instance.playerdata[n].entity.onground = false;
				}
				yield return null;
			}
		}
		else
		{
			player.entity.MoveTowards(othermovepos, 1f, 1, 0, true);
			while (player.entity.forcemove)
			{
				yield return null;
			}
		}
		player.entity.DetectIgnoreSphere(true);
		instance.minipause = false;
		player.lastpos = player.transform.position;
		player.lastloadzone = player.transform.position;
		yield return null;
		player.entity.hitwall = false;
		player.npc = new List<NPCControl>();
		player.pausecooldown = 7f;
		roomtransition = false;
	}

	public void ForceLoadSprites()
	{
		instance.StartCoroutine(FLS());
	}

	private static IEnumerator FLS()
	{
		if (map.entities == null)
		{
			yield break;
		}
		SpriteRenderer[] r = new SpriteRenderer[map.entities.Length];
		for (int i = 0; i < r.Length; i++)
		{
			if (map.entities[i] != null && map.entities[i].sprite != null && map.entities[i].sprite.sprite != null)
			{
				r[i] = new GameObject().AddComponent<SpriteRenderer>();
				r[i].transform.parent = map.transform;
				r[i].transform.position = player.transform.position;
				r[i].sprite = map.entities[i].sprite.sprite;
			}
		}
		yield return null;
		for (int j = 0; j < r.Length; j++)
		{
			if (r[j] != null)
			{
				UnityEngine.Object.Destroy(r[j].gameObject);
			}
		}
	}

	private static string GetText(bool menu, int id)
	{
		if (!menu)
		{
			return GetDialogueText(id);
		}
		return menutext[id];
	}

	public static int GetEnemyPortrait(int id)
	{
		int num = Convert.ToInt32(enemydata[id, 43]);
		if (instance.flags[664])
		{
			switch ((Enemies)id)
			{
			case Enemies.CursedSkull:
				return 227;
			case Enemies.Krawler:
				return 226;
			case Enemies.Cape:
				return 225;
			}
		}
		if (num > -1)
		{
			return num;
		}
		return id;
	}

	public static Vector2 GetDirection([In] ref Vector2 a, [In] ref Vector2 b)
	{
		return (a - b).normalized;
	}

	public static Vector3 GetDirection([In] ref Vector3 a, [In] ref Vector3 b)
	{
		return (a - b).normalized;
	}

	public static Vector3 GetDirection([In] ref Vector3 a, [In] ref Vector3 b, bool ignoreY)
	{
		if (!ignoreY)
		{
			return GetDirection(ref a, ref b);
		}
		Vector3 a2 = new Vector3(a.x, 0f, a.z);
		Vector3 b2 = new Vector3(b.x, 0f, b.z);
		return GetDirection(ref a2, ref b2);
	}

	public static Vector3 GetDirection4(Vector3 a, Vector3 b, bool ignoreY)
	{
		if (ignoreY)
		{
			a = new Vector3(a.x, 0f, a.z);
			b = new Vector3(b.x, 0f, b.z);
		}
		Vector3 a2 = new Vector3(a.x, 0f, a.z);
		Vector3 b2 = new Vector3(b.x, 0f, b.z);
		return GetDirection(ref a2, ref b2);
	}

	public static IEnumerator MoveTowards(Transform obj, Vector3 target, float frametime, bool smooth, bool local)
	{
		float a = 0f;
		Vector3 s = (local ? obj.localPosition : obj.localPosition);
		do
		{
			if (local)
			{
				if (smooth)
				{
					obj.localPosition = SmoothLerp(s, target, a / frametime);
				}
				else
				{
					obj.localPosition = Vector3.Lerp(s, target, a / frametime);
				}
			}
			else if (smooth)
			{
				obj.position = SmoothLerp(s, target, a / frametime);
			}
			else
			{
				obj.position = Vector3.Lerp(s, target, a / frametime);
			}
			a += TieFramerate(1f);
			yield return null;
		}
		while (a < frametime);
	}

	public static IEnumerator MoveTowards(Transform obj, Vector3 start, Vector3 end, float frametime, bool smooth, Action<bool> caller)
	{
		instance.StartCoroutine(MoveTowards(obj, start, end, frametime, 0f, 0f, -1f, smooth, caller, null));
		yield return null;
	}

	public static IEnumerator MoveTowards(Transform obj, Vector3 start, Vector3 end, float frametime, float startdelay, float shrink, float destroytime, bool smooth, Action<bool> caller)
	{
		instance.StartCoroutine(MoveTowards(obj, start, end, frametime, startdelay, shrink, destroytime, smooth, caller, null));
		yield return null;
	}

	public static IEnumerator MoveTowards(Transform obj, Vector3 start, Vector3 end, float frametime, float startdelay, float shrink, float destroytime, bool smooth, Action<bool> caller, string soundatend)
	{
		if (startdelay > 0f)
		{
			yield return new WaitForSeconds(startdelay);
		}
		float a = 0f;
		do
		{
			if (smooth)
			{
				obj.transform.position = SmoothLerp(start, end, a / frametime);
			}
			else
			{
				obj.transform.position = Vector3.Lerp(start, end, a / frametime);
			}
			a += TieFramerate(1f);
			yield return null;
		}
		while (a < frametime + 1f);
		obj.transform.position = end;
		if (soundatend != null)
		{
			PlaySound(soundatend);
		}
		yield return null;
		if (shrink > 0f)
		{
			DialogueAnim dialogueAnim = obj.gameObject.AddComponent<DialogueAnim>();
			dialogueAnim.shrink = true;
			dialogueAnim.shrinkspeed = shrink;
		}
		if (destroytime > -1f)
		{
			if (destroytime == 0f)
			{
				UnityEngine.Object.Destroy(obj.gameObject);
			}
			else
			{
				UnityEngine.Object.Destroy(obj.gameObject, destroytime);
			}
		}
		if (caller != null)
		{
			caller(false);
		}
	}

	public static Vector3 GlobalScale(Vector3 desiredscale, Vector3 parentscale, bool flipZY)
	{
		if (flipZY)
		{
			return new Vector3(desiredscale.x / parentscale.x, desiredscale.z / parentscale.z, desiredscale.y / parentscale.y);
		}
		return new Vector3(desiredscale.x / parentscale.x, desiredscale.y / parentscale.y, desiredscale.z / parentscale.z);
	}

	public static void DialogueText(string text, Transform tailtarget, NPCControl caller)
	{
		instance.StartCoroutine(SetText(text, true, Vector3.zero, tailtarget, caller));
	}

	public static IEnumerator OpenQuestBoard(EntityControl caretaker, NPCControl caller)
	{
		instance.discoveryhud = 0f;
		SaveCameraPosition(true);
		instance.flags[2] = true;
		instance.option = 0;
		instance.showmoney = 0f;
		instance.hudcooldown = 0f;
		instance.minipause = true;
		if (instance.boardquests[0].Count > 1)
		{
			instance.boardquests[0].Remove(0);
		}
		yield return null;
		if (caretaker != null)
		{
			caretaker.FaceTowards(player.transform.position);
		}
		if (caller != null)
		{
			for (int i = 0; i < instance.playerdata.Length; i++)
			{
				instance.playerdata[i].entity.FaceTowards(caller.transform.position);
			}
			instance.boardcaller = caller;
			tempcamspeed = instance.camspeed;
			tempcamoffset = instance.camoffset;
			tempcamangleoffset = instance.camangleoffset;
			if (caller.vectordata[0].magnitude > 0.1f)
			{
				instance.camoffset = caller.vectordata[0];
			}
			if (caller.vectordata[1].magnitude > 0.1f)
			{
				instance.camangleoffset = caller.vectordata[1];
			}
			if (caller.vectordata[2].x > 0.01f)
			{
				instance.camspeed = caller.vectordata[2].x;
			}
			yield return new WaitForSeconds(caller.vectordata[2].y);
			for (int j = 0; j < instance.playerdata.Length; j++)
			{
				instance.playerdata[j].entity.FaceTowards(caller.transform.position);
			}
		}
		if (caretaker != null)
		{
			caretaker.FaceTowards(player.transform.position);
		}
		instance.questboardobj = NewUIObject("QuestBoard", null, new Vector3(0f, 0f, 10f)).transform;
		DialogueAnim dialogueAnim = NewUIObject("LeafSprite", instance.questboardobj, new Vector3(0f, -1.75f), Vector2.one, Resources.Load<Sprite>("Sprites/GUI/bigleaf")).AddComponent<DialogueAnim>();
		dialogueAnim.transform.localScale = Vector3.zero;
		dialogueAnim.targetscale = Vector3.one * 2f;
		dialogueAnim.GetComponent<SpriteRenderer>().color = new Color(0.9f, 0.7f, 0.1f);
		dialogueAnim.GetComponent<SpriteRenderer>().sortingOrder = -50;
		Create9Box(new Vector3(-3.75f, -0.3f, 10f), new Vector2(8f, 7.5f), 4, -10, Color.yellow, true).transform.parent = instance.questboardobj;
		Create9Box(new Vector3(4.75f, -0.3f, 10f), new Vector2(8.25f, 9f), 4, -5, Color.white, true).transform.parent = instance.questboardobj;
		for (int k = 0; k < 2; k++)
		{
			NewUIObject("arrow" + k, instance.questboardobj, (k == 0) ? new Vector3(0.2f, 4f, 10f) : new Vector3(-7.7f, 4f, 10f), Vector3.one, guisprites[1], 20).transform.localEulerAngles = ((k == 0) ? new Vector3(0f, 0f, 90f) : new Vector3(0f, 0f, 270f));
		}
		for (int l = 0; l < 2; l++)
		{
			int buttonid = 0;
			string description = (AsianLang() ? "|size,0.7,1|" : "");
			Vector3 position = Vector3.zero;
			switch (l)
			{
			case 0:
				buttonid = 4;
				description = "|sort,2|" + menutext[83];
				position = new Vector3(-8f, -4.5f, 10f);
				break;
			case 1:
				buttonid = 5;
				description = "|sort,2|" + menutext[43];
				position = new Vector3(-2.8f, -4.5f, 10f);
				break;
			}
			new GameObject("button " + l).AddComponent<ButtonSprite>().SetUp(buttonid, -1, description, position, Vector3.one * 0.5f, 10, null).transform.parent = instance.questboardobj;
		}
		float num = -1.6f;
		for (int m = 0; m < 3; m++)
		{
			SpriteRenderer component = NewUIObject("tab" + m, instance.questboardobj.GetChild(1), new Vector3(num, 4.25f), new Vector3(0.75f, 0.75f, 1f), guisprites[4]).GetComponent<SpriteRenderer>();
			component.color = instance.questcolors[m];
			component.sortingOrder = -5 - m * 2;
			num += 1.55f;
			float x = -0.5f;
			switch (languageid)
			{
			case 1:
				x = -0.75f;
				break;
			case 3:
				x = 0f;
				break;
			}
			instance.StartCoroutine(SetText((AsianLang() ? "|size,0.8,1.15|" : ((languageid == 6) ? "|size,1.2,1.5|" : "|size,1.5|")) + "|single||quarterline||center||sort," + (component.sortingOrder + 1) + "|" + menutext[84 + m], new Vector3(x, -0.25f), component.transform));
		}
		SetUpList(14, true, false);
		listammount = 10;
		ShowItemList(14, new Vector2(-8.5f, 2.55f), true, false);
		UpdateQuestBoard();
	}

	public static void ChangeBoardQuest(int id)
	{
		ChangeBoardQuest(id, 0);
	}

	private static string BoardString()
	{
		string text = "";
		for (int i = 0; i < instance.boardquests[0].Count; i++)
		{
			text = text + instance.boardquests[0][i] + ",";
		}
		return text;
	}

	public static void ChangeBoardQuest(int id, int type)
	{
		if (!instance.boardquests[type].Contains(id))
		{
			instance.boardquests[type].Remove(0);
			instance.boardquests[type].Add(id);
			if (type == 0 && id > 0)
			{
				instance.flags[2] = false;
			}
		}
	}

	public static void CompleteQuest(int id)
	{
		instance.boardquests[1].Remove(id);
		instance.boardquests[2].Add(id);
		instance.boardquests[2].Remove(0);
		if (instance.boardquests[1].Count == 0)
		{
			instance.boardquests[1].Add(0);
		}
		instance.discoveryhud = 350f;
		instance.flagvar[47]++;
		for (int i = 0; i < 3; i++)
		{
			instance.discoverymessage.GetChild(i + 1).gameObject.SetActive(i + 1 == 2);
		}
		instance.discoverymessage.GetComponentInChildren<Animator>().PlayInFixedTime("Quest");
	}

	public static ItemUse GetItemUse(int id)
	{
		return GetItemUse(id, 0);
	}

	public static ItemUse GetItemUse(int id, int itemtype)
	{
		ItemUse result = default(ItemUse);
		string[] array = itemdata[itemtype, id, 5].Split(';');
		result.usetype = new ItemUsage[array.Length];
		result.values = new int[array.Length];
		for (int i = 0; i < array.Length; i++)
		{
			string[] array2 = array[i].Split(',');
			result.usetype[i] = (ItemUsage)Enum.Parse(typeof(ItemUsage), array2[0]);
			result.values[i] = Convert.ToInt32(array2[1]);
		}
		return result;
	}

	public static int? GetItemHeal(int id)
	{
		ItemUse itemUse = GetItemUse(id);
		for (int i = 0; i < itemUse.usetype.Length; i++)
		{
			if (itemUse.usetype[i] == ItemUsage.HPRecover || itemUse.usetype[i] == ItemUsage.Revive || itemUse.usetype[i] == ItemUsage.AutoRevive)
			{
				return itemUse.values[i];
			}
			if (itemUse.usetype[i] == ItemUsage.HPRecoverAll || itemUse.usetype[i] == ItemUsage.ReviveAll)
			{
				return -itemUse.values[i];
			}
			if (itemUse.usetype[i] == ItemUsage.HPRecoverFull)
			{
				return 999;
			}
		}
		return null;
	}

	public static int DoItemEffect(ItemUsage type, int value, int? characterid)
	{
		switch (type)
		{
		case ItemUsage.HPRecover:
		case ItemUsage.Revive:
			PlaySound("Heal");
			if (type == ItemUsage.GradualHP)
			{
				value *= 2;
			}
			if (characterid.HasValue && instance.inbattle)
			{
				value += BadgeHowManyEquipped(74, lastitemuser);
			}
			instance.playerdata[characterid.Value].hp = Mathf.Clamp(instance.playerdata[characterid.Value].hp + value, 0, instance.playerdata[characterid.Value].maxhp);
			if (!instance.inbattle || !BadgeIsEquipped(24, instance.playerdata[characterid.Value].trueid))
			{
				break;
			}
			goto case ItemUsage.AddPoison;
		case ItemUsage.HPorDamage:
			if (battle == null)
			{
				if (characterid.HasValue)
				{
					value += BadgeHowManyEquipped(74, lastitemuser);
				}
				if (UnityEngine.Random.Range(0, 100) > 40)
				{
					PlaySound("Heal");
					instance.playerdata[characterid.Value].hp = Mathf.Clamp(instance.playerdata[characterid.Value].hp + value, 0, instance.playerdata[characterid.Value].maxhp);
				}
				else
				{
					PlaySound("Damage0");
					instance.playerdata[characterid.Value].hp = Mathf.Clamp(instance.playerdata[characterid.Value].hp - value, 1, instance.playerdata[characterid.Value].maxhp);
				}
			}
			if (!instance.inbattle || !BadgeIsEquipped(24, instance.playerdata[characterid.Value].trueid))
			{
				break;
			}
			goto case ItemUsage.AddPoison;
		case ItemUsage.HPRecoverAll:
		case ItemUsage.ReviveAll:
		{
			if (type == ItemUsage.GradualHPParty)
			{
				value *= 2;
			}
			PlaySound("Heal");
			if (characterid.HasValue && instance.inbattle)
			{
				value += BadgeHowManyEquipped(74, lastitemuser);
			}
			for (int l = 0; l < instance.playerdata.Length; l++)
			{
				if (instance.playerdata[l].eatenby == null && (type == ItemUsage.ReviveAll || instance.playerdata[l].hp > 0))
				{
					instance.playerdata[l].hp = Mathf.Clamp(instance.playerdata[l].hp + value, 0, instance.playerdata[l].maxhp);
					if (instance.inbattle && BadgeIsEquipped(24, instance.playerdata[l].trueid))
					{
						DoItemEffect(ItemUsage.AddPoison, 2, l);
					}
				}
			}
			break;
		}
		case ItemUsage.TPRecover:
			if (type == ItemUsage.GradualTP)
			{
				value *= 2;
			}
			PlaySound("Heal2");
			instance.tp = Mathf.Clamp(instance.tp + value, 0, instance.maxtp);
			if (!instance.inbattle || !BadgeIsEquipped(24, instance.playerdata[characterid.Value].trueid))
			{
				break;
			}
			goto case ItemUsage.AddPoison;
		case ItemUsage.TurnNextTurn:
			PlaySound("Heal3");
			if (instance.inbattle)
			{
				instance.playerdata[characterid.Value].moreturnnextturn++;
			}
			break;
		case ItemUsage.HPUP:
			PlaySound("StatUp");
			AddStatBonus(StatBonus.HP, value, characterid.Value);
			ApplyStatBonus();
			break;
		case ItemUsage.TPUP:
			PlaySound("StatUp");
			AddStatBonus(StatBonus.TP, value, -1);
			ApplyStatBonus();
			break;
		case ItemUsage.HPUPAll:
			PlaySound("StatUp");
			AddStatBonus(StatBonus.HP, value, -1);
			ApplyStatBonus();
			break;
		case ItemUsage.MPUP:
			PlaySound("StatUp");
			AddStatBonus(StatBonus.MP, value, -1);
			instance.maxbp += value;
			instance.bp += value;
			ApplyStatBonus();
			break;
		case ItemUsage.AttackUp:
			PlaySound("StatUp");
			AddStatBonus(StatBonus.Attack, value, characterid.Value);
			ApplyStatBonus();
			if (!instance.inbattle || !BadgeIsEquipped(24, instance.playerdata[characterid.Value].trueid))
			{
				break;
			}
			goto case ItemUsage.AddPoison;
		case ItemUsage.AtkDownAfter:
			instance.playerdata[characterid.Value].atkdownonloseatkup = true;
			break;
		case ItemUsage.ChargeUp:
			instance.playerdata[characterid.Value].charge = Mathf.Clamp(instance.playerdata[characterid.Value].charge + 1, 0, 3);
			if (!instance.inbattle || !BadgeIsEquipped(24, instance.playerdata[characterid.Value].trueid))
			{
				break;
			}
			goto case ItemUsage.AddPoison;
		case ItemUsage.DefenseUp:
			PlaySound("StatUp");
			AddStatBonus(StatBonus.Defense, value, characterid.Value);
			ApplyStatBonus();
			if (!instance.inbattle || !BadgeIsEquipped(24, instance.playerdata[characterid.Value].trueid))
			{
				break;
			}
			goto case ItemUsage.AddPoison;
		case ItemUsage.AddPoison:
			if (instance.inbattle && HasCondition(BattleCondition.Sturdy, instance.playerdata[characterid.Value]) <= 0)
			{
				PlaySound("Poison");
				if (!BadgeIsEquipped(7, instance.playerdata[characterid.Value].trueid) && !BadgeIsEquipped(66, instance.playerdata[characterid.Value].trueid))
				{
					SetCondition(BattleCondition.Poison, ref instance.playerdata[characterid.Value], BadgeIsEquipped(27, instance.playerdata[characterid.Value].trueid) ? 9999999 : (((type == ItemUsage.AddPoison) ? value : 2) + (BadgeIsEquipped(24, instance.playerdata[characterid.Value].trueid) ? 1 : 0)));
				}
			}
			break;
		case ItemUsage.AddNumb:
			if (instance.inbattle && HasCondition(BattleCondition.Sturdy, instance.playerdata[characterid.Value]) <= 0)
			{
				PlaySound("Shock");
				if (!BadgeIsEquipped(21, instance.playerdata[characterid.Value].trueid) && !BadgeIsEquipped(66, instance.playerdata[characterid.Value].trueid))
				{
					SetCondition(BattleCondition.Numb, ref instance.playerdata[characterid.Value], value);
				}
			}
			break;
		case ItemUsage.AddFreeze:
			if (instance.inbattle && HasCondition(BattleCondition.Sturdy, instance.playerdata[characterid.Value]) <= 0)
			{
				PlaySound("Freeze");
				if (!BadgeIsEquipped(33, instance.playerdata[characterid.Value].trueid) && !BadgeIsEquipped(66, instance.playerdata[characterid.Value].trueid))
				{
					SetCondition(BattleCondition.Freeze, ref instance.playerdata[characterid.Value], value);
				}
			}
			break;
		case ItemUsage.AddSleep:
			if (instance.inbattle && HasCondition(BattleCondition.Sturdy, instance.playerdata[characterid.Value]) <= 0)
			{
				PlaySound("Sleep");
				if (!BadgeIsEquipped(12, instance.playerdata[characterid.Value].trueid) && !BadgeIsEquipped(66, instance.playerdata[characterid.Value].trueid))
				{
					SetCondition(BattleCondition.Sleep, ref instance.playerdata[characterid.Value], value);
				}
			}
			break;
		case ItemUsage.CureFire:
			PlaySound("Heal3");
			RemoveCondition(BattleCondition.Fire, instance.playerdata[characterid.Value]);
			if (instance.inbattle && instance.playerdata[characterid.Value].battleentity.firepart != null)
			{
				UnityEngine.Object.Destroy(instance.playerdata[characterid.Value].battleentity.firepart.gameObject);
			}
			break;
		case ItemUsage.CureAll:
			PlaySound("Heal3");
			RemoveCondition(BattleCondition.Poison, instance.playerdata[characterid.Value]);
			RemoveCondition(BattleCondition.Sleep, instance.playerdata[characterid.Value]);
			RemoveCondition(BattleCondition.Freeze, instance.playerdata[characterid.Value]);
			RemoveCondition(BattleCondition.Numb, instance.playerdata[characterid.Value]);
			RemoveCondition(BattleCondition.Fire, instance.playerdata[characterid.Value]);
			RemoveCondition(BattleCondition.Inked, instance.playerdata[characterid.Value]);
			RemoveCondition(BattleCondition.Sticky, instance.playerdata[characterid.Value]);
			if (instance.inbattle)
			{
				if (instance.playerdata[characterid.Value].battleentity.firepart != null)
				{
					UnityEngine.Object.Destroy(instance.playerdata[characterid.Value].battleentity.firepart.gameObject);
				}
				instance.playerdata[characterid.Value].battleentity.BreakIce();
				instance.playerdata[characterid.Value].isasleep = false;
				instance.playerdata[characterid.Value].isnumb = false;
			}
			break;
		case ItemUsage.CurePoisonAll:
		{
			PlaySound("Heal3");
			for (int k = 0; k < instance.playerdata.Length; k++)
			{
				RemoveCondition(BattleCondition.Poison, instance.playerdata[k]);
			}
			break;
		}
		case ItemUsage.CurePoison:
			PlaySound("Heal3");
			RemoveCondition(BattleCondition.Poison, instance.playerdata[characterid.Value]);
			break;
		case ItemUsage.CureSleep:
			PlaySound("Heal3");
			instance.playerdata[characterid.Value].isasleep = false;
			RemoveCondition(BattleCondition.Sleep, instance.playerdata[characterid.Value]);
			break;
		case ItemUsage.CureFreeze:
			PlaySound("Heal3");
			RemoveCondition(BattleCondition.Freeze, instance.playerdata[characterid.Value]);
			if (instance.inbattle)
			{
				instance.playerdata[characterid.Value].battleentity.BreakIce();
			}
			break;
		case ItemUsage.CureNumb:
			PlaySound("Heal3");
			instance.playerdata[characterid.Value].isnumb = false;
			RemoveCondition(BattleCondition.Numb, instance.playerdata[characterid.Value]);
			break;
		case ItemUsage.CureParty:
		{
			PlaySound("Heal3");
			for (int j = 0; j < instance.playerdata.Length; j++)
			{
				if (instance.playerdata[j].hp <= 0 || !(instance.playerdata[j].eatenby == null))
				{
					continue;
				}
				RemoveCondition(BattleCondition.Poison, instance.playerdata[j]);
				RemoveCondition(BattleCondition.Sleep, instance.playerdata[j]);
				RemoveCondition(BattleCondition.Freeze, instance.playerdata[j]);
				RemoveCondition(BattleCondition.Numb, instance.playerdata[j]);
				RemoveCondition(BattleCondition.Fire, instance.playerdata[j]);
				RemoveCondition(BattleCondition.Sticky, instance.playerdata[j]);
				RemoveCondition(BattleCondition.Inked, instance.playerdata[j]);
				if (instance.inbattle)
				{
					if (instance.playerdata[j].battleentity.firepart != null)
					{
						UnityEngine.Object.Destroy(instance.playerdata[j].battleentity.firepart.gameObject);
					}
					instance.playerdata[j].battleentity.BreakIce();
					instance.playerdata[j].isasleep = false;
					instance.playerdata[j].isnumb = false;
				}
			}
			break;
		}
		case ItemUsage.HPto1:
			if (!instance.inbattle)
			{
				PlaySound("Damage0");
			}
			instance.playerdata[characterid.Value].hp = 1;
			break;
		case ItemUsage.GradualHP:
			if (!instance.inbattle)
			{
				goto case ItemUsage.HPRecover;
			}
			PlaySound("Heal3");
			SetCondition(BattleCondition.GradualHP, ref instance.playerdata[characterid.Value], value);
			if (!instance.inbattle || !BadgeIsEquipped(24, instance.playerdata[characterid.Value].trueid))
			{
				break;
			}
			goto case ItemUsage.AddPoison;
		case ItemUsage.GradualHPParty:
		{
			if (!instance.inbattle)
			{
				goto case ItemUsage.HPRecoverAll;
			}
			PlaySound("Heal3");
			for (int i = 0; i < instance.playerdata.Length; i++)
			{
				if (instance.playerdata[i].hp > 0)
				{
					SetCondition(BattleCondition.GradualHP, ref instance.playerdata[i], value);
				}
			}
			if (!instance.inbattle || !BadgeIsEquipped(24, instance.playerdata[characterid.Value].trueid))
			{
				break;
			}
			goto case ItemUsage.AddPoison;
		}
		case ItemUsage.GradualTP:
			if (!instance.inbattle)
			{
				goto case ItemUsage.TPRecover;
			}
			PlaySound("Heal3");
			SetCondition(BattleCondition.GradualTP, ref instance.playerdata[characterid.Value], value);
			if (!instance.inbattle || !BadgeIsEquipped(24, instance.playerdata[characterid.Value].trueid))
			{
				break;
			}
			goto case ItemUsage.AddPoison;
		}
		if (battle != null)
		{
			battle.UpdateConditionIcons();
		}
		return value;
	}

	public static void IgnoreRadius(Collider col, bool ignore)
	{
		NPCControl[] array = UnityEngine.Object.FindObjectsOfType<NPCControl>();
		if (array != null)
		{
			for (int i = 0; i < array.Length; i++)
			{
				Physics.IgnoreCollision(col, array[i].scol, ignore);
			}
		}
	}

	private static Sprite GetButtonSpriteD(int id)
	{
		switch (forcejoystick)
		{
		default:
			return null;
		}
	}

	public static void SwitchParty(bool battle)
	{
		if (!battle && instance.inevent)
		{
			return;
		}
		int num = 0;
		if (MainManager.battle == null)
		{
			PlaySound("Switch");
		}
		for (int i = 0; i < instance.playerdata.Length; i++)
		{
			if (i == 0)
			{
				num = ((!battle) ? instance.playerdata[i].animid : MainManager.battle.partypointer[i]);
			}
			int num2 = i + 1;
			if (num2 >= instance.playerdata.Length)
			{
				num2 = 0;
			}
			if (!battle)
			{
				instance.playerdata[i].animid = instance.playerdata[num2].animid;
				if (i == instance.playerdata.Length - 1)
				{
					instance.playerdata[i].animid = num;
				}
			}
			else
			{
				MainManager.battle.partypointer[i] = MainManager.battle.partypointer[num2];
				if (i == instance.playerdata.Length - 1)
				{
					MainManager.battle.partypointer[i] = num;
				}
			}
			if (!battle)
			{
				if (instance.switchicon != null)
				{
					instance.switchicon.sprite = guisprites[instance.playerdata[0].animid + 94];
					instance.switchicon.color = new Color(1f, 1f, 1f, 0.5f);
				}
				instance.playerdata[0].entity.emoticonoffset = new Vector3(0f, 1.8f + 0.25f * (float)instance.playerdata[0].animid, -0.1f);
			}
		}
	}

	public static EntityControl GetEntity(string id)
	{
		return GetEntity(id, null);
	}

	public static EntityControl GetEntity(string id, EntityControl caller)
	{
		if (!instance.inbattle)
		{
			if (id == "this")
			{
				return instance.tailtarget.GetComponent<EntityControl>();
			}
			if (id == "caller")
			{
				return caller;
			}
			if (define != null && define.Count > 0)
			{
				string[][] array = define.ToArray();
				for (int i = 0; i < define.Count; i++)
				{
					if (array[i][0].ToLower() == id.ToLower())
					{
						return GetEntity(Convert.ToInt32(array[i][1]));
					}
				}
			}
		}
		return GetEntity(Convert.ToInt32(id));
	}

	public static int[] PrizeBadges(bool caravan)
	{
		List<int> list = new List<int>();
		for (int i = 0; i < instance.prizeflags.Length; i++)
		{
			if ((caravan && instance.flagvar[instance.prizeflags[i]] == 2) || (!caravan && instance.flagvar[instance.prizeflags[i]] == 1))
			{
				list.Add(caravan ? instance.prizeids[i] : instance.prizeflags[i]);
			}
		}
		if (list.Count > 0)
		{
			return list.ToArray();
		}
		return null;
	}

	public static int GetEnemyPrizeID(int flagvalue)
	{
		for (int i = 0; i < instance.prizeflags.Length; i++)
		{
			if (instance.prizeflags[i] == flagvalue)
			{
				return i;
			}
		}
		return -1;
	}

	public static EntityControl[] GetEntities(int[] ids)
	{
		return GetEntities(ids, null);
	}

	public static EntityControl[] GetEntities(int[] ids, EntityControl[] ads)
	{
		List<EntityControl> list = new List<EntityControl>();
		for (int i = 0; i < ids.Length; i++)
		{
			list.Add(GetEntity(ids[i]));
		}
		if (ads != null)
		{
			list.AddRange(ads);
		}
		return list.ToArray();
	}

	public static Vector3 SmoothLerp(Vector3 a, Vector3 b, float t)
	{
		return SmoothLerp(a, b, t, 0f, 0f);
	}

	public static Vector3 SmoothLerp(Vector3 a, Vector3 b, float t, float onlythrough, float onlyafter)
	{
		if (onlythrough > 0f && t < onlythrough)
		{
			return Vector3.Lerp(a, b, t);
		}
		if (onlyafter > 0f && t > onlyafter)
		{
			return Vector3.Lerp(a, b, t);
		}
		return new Vector3(Mathf.SmoothStep(a.x, b.x, t), Mathf.SmoothStep(a.y, b.y, t), Mathf.SmoothStep(a.z, b.z, t));
	}

	public static EntityControl GetEntity(int id)
	{
		if (id >= 1000)
		{
			return map.tempfollowers[id - 1000];
		}
		if (!instance.inbattle)
		{
			switch (id)
			{
			case -1:
				if (instance.playerdata.Length != 0 && instance.playerdata[0].entity != null)
				{
					return instance.playerdata[0].entity;
				}
				break;
			case -2:
				if (instance.playerdata.Length > 1 && instance.playerdata[1].entity != null)
				{
					return instance.playerdata[1].entity;
				}
				break;
			case -3:
				if (instance.playerdata.Length > 2 && instance.playerdata[2].entity != null)
				{
					return instance.playerdata[2].entity;
				}
				break;
			case -4:
			{
				for (int j = 0; j < instance.playerdata.Length; j++)
				{
					if (instance.playerdata[j].entity.animid == 0)
					{
						return instance.playerdata[j].entity;
					}
				}
				break;
			}
			case -5:
			{
				for (int i = 0; i < instance.playerdata.Length; i++)
				{
					if (instance.playerdata[i].entity.animid == 1)
					{
						return instance.playerdata[i].entity;
					}
				}
				break;
			}
			case -6:
			{
				for (int k = 0; k < instance.playerdata.Length; k++)
				{
					if (instance.playerdata[k].entity.animid == 2)
					{
						return instance.playerdata[k].entity;
					}
				}
				break;
			}
			default:
				if (map != null && id < map.entities.Length && id >= 0 && map.entities[id] != null)
				{
					return map.entities[id];
				}
				break;
			}
		}
		else
		{
			if (id >= 0)
			{
				if (id < instance.playerdata.Length)
				{
					return instance.playerdata[id].battleentity;
				}
				return battle.enemydata[id - instance.playerdata.Length].battleentity;
			}
			if (battle.extraentities != null || battle.extraentities.Length != 0)
			{
				return battle.extraentities[Mathf.Abs(id + 1)];
			}
		}
		return null;
	}
}
