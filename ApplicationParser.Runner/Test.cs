namespace ApplicationParser.Runner
{

	public static class Application 
	{
		public const string Name = "Heretik";
		public const string Guid = "072513f0-ccb4-429e-b267-f0f34c0b6ce4";
	}
	public static class ObjectTypeGuids
	{
		public const string Document = "15c36703-74ea-4ff8-9dfb-ad30ece7530d";
		public const string ContractAnalysisJob = "1781f1b0-c245-48bf-a4b1-e6fefff52692";
		public const string HeretikConfiguration = "dbd4aa67-1be4-4c73-b0f0-6520444fecb4";
	}

	public static class DocumentFieldGuids
	{
		public const string FileName = "72c9c4fa-9c18-4b57-b855-d7f1e496179d";
		public const string ContractType = "4f888890-7da3-4cf7-b289-f40814dc5bae";
		public const string SectionType = "9c8231af-19af-410b-9d44-f3b67e96bf15";
		public const string DocumentsAnalyzed = "5079b253-9fdd-4519-a122-4913f3d7b84e";
		public const string SectionsIdentified = "3739a643-d4b3-4bc1-972f-fa9621be83f2";
		public const string Confidence = "994bc78f-7971-4a1a-8914-896195c51a28";
	}
	public static class ContractAnalysisJobFieldGuids
	{
		public const string DocumentsAnalyzed = "f045d647-01be-46fb-8270-7cd4035e5d8b";
		public const string DataSource = "1adc9877-238b-4ef7-8fbd-394914549f92";
		public const string JobType = "96afdafd-2e65-41f5-873c-77a1d35cb0c6";
		public const string ContractsAnalyzed = "e11617b6-6553-4043-b821-bbf1f5886fe5";
	}
	public static class HeretikConfigurationFieldGuids
	{
	}

	public static class ContractTypeChoiceGuids
	{
		public const string ArticlesofIncorporation = "5bfdf152-11a4-4824-a6d5-dab5726dfe3e";
		public const string Indemnification = "99384588-1416-4aee-a60b-d05a9ddff9d0";
		public const string Merger = "329e2fc6-bec5-45ed-9290-cf4867fb995c";
		public const string Purchaser = "f432bc49-2284-42d2-beb4-6fe03fad9118";
		public const string ByLaws = "de3fcc77-acec-4f9f-b95f-cb5b4f79f28a";
		public const string ConsultingAgreement = "ab2758be-58e1-4518-868a-c13e85afa1cb";
		public const string Compensation = "c41b4f05-e62d-45db-8340-444cea69f328";
	}
	public static class SectionTypeChoiceGuids
	{
		public const string Grant = "797590f5-b2e0-4dce-ba89-d4200649d68b";
		public const string Vesting = "03333e1d-dce6-4e39-9267-ba9f422b6cd1";
		public const string Payout = "37709845-babb-436a-a47d-60874beb9d85";
		public const string Tax = "21be3a7a-3774-4770-9902-4976e60c07be";
		public const string General = "ed57d0ae-a30f-4193-a6bc-b74da1d071e3";
		public const string FeesandExpenses = "6ba1c3bd-307d-4226-8f28-9feb8fe2e2df";
		public const string Product = "915e5a2a-367e-4d5e-9764-9204d2ce8234";
		public const string Confidentiality = "1d5efba9-1e3a-4661-9c73-e2b54f04ba04";
		public const string Indemnification = "44d5f5de-d875-4d50-b70c-99c0bf9e70dc";
		public const string Assignment = "347584cc-a615-4019-b6bd-838e81ca89b1";
		public const string Notices = "ebd31cfd-98a4-491d-9c09-a66c9f89ec57";
		public const string SurvivalofTerms = "409717f4-639d-41a4-84e1-d49ef873a3f8";
	}
	public static class JobTypeChoiceGuids
	{
		public const string Manual = "0c53e213-318d-491e-998e-1098582b1c48";
		public const string Auto = "91114471-69ae-4c69-99e4-2695e0fd4048";
	}

	public static class TabGuids
	{
		public const string ContractAnalysisJobs = "4976a640-bf1c-46e1-b358-0102088e19aa";
		public const string Configuration = "f10f8d02-2355-486e-928a-a3b49b439fb5";
	}


}
