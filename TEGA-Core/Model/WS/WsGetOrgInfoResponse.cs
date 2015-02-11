using System;

namespace TEGACore
{
	public class WsGetOrgInfoResponse
	{
		public long orgId { get; set; }

		public string name { get; set; }

		public long logoId { get; set; }

		public long addressId { get; set; }

		public long addressTypeId { get; set; }

		public string street1 { get; set; }

		public string street2 { get; set; }

		public string street3 { get; set; }

		public string zip { get; set; }

		public string city { get; set; }

		public long phoneId { get; set; }

		public long phoneTypeId { get; set; }

		public string phonePrefix { get; set; }

		public string phoneNumber { get; set; }

		public string phoneExtension { get; set; }

		public long faxId { get; set; }

		public long faxTypeId { get; set; }

		public string faxPrefix { get; set; }

		public string faxNumber { get; set; }

		public string faxExtension { get; set; }

		public long emailId { get; set; }

		public long emailTypeId { get; set; }

		public string emailAddress { get; set; }

		public long websiteId { get; set; }

		public long websiteTypeId { get; set; }

		public string websiteUrl { get; set; }
	}
}

