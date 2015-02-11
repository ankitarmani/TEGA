using System;

namespace TEGACore
{
	public class Phone
	{
		public long phoneId { get; set; }
		
		public string className { get; set; }
		
		public long classPk { get; set; }

		public long typeId { get; set; }

		public string prefix { get; set; }
		
		public string number { get; set; }
		
		public string extension { get; set; }
	}
}

