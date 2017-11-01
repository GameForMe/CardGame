using System;


	public class CommonFun
	{
		public CommonFun ()
		{
		
		}

	public static bool IsPhoneNum(string str_handset)
	{
		return System.Text.RegularExpressions.Regex.IsMatch(str_handset,@"^[1]+[3,4,5,7,8]+\d{9}");
	}
	}


