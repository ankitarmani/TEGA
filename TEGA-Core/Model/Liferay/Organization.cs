using System;

namespace TEGACore
{
	public class Organization
	{
		public long organizationId { get; set; }

		public long parentOrganizationId { get; set; }

		public string name { get; set; }

		public string type { get; set; }

		public long logoId { get; set; }
	}
}

