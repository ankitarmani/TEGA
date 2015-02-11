using System;

namespace TEGACore
{
	public class Address
	{
		public long addressId { get; set; }

		public string className { get; set; }

		public long classPk { get; set; }

		public long typeId { get; set; }

		public string street1 { get; set; }

		public string street2 { get; set; }

		public string street3 { get; set; }

		public string city { get; set; }

		public string zip { get; set; }

		public string region { get; set; }

		public string country { get; set; }
	}
}

