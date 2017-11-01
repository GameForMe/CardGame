using System;


public class StringFunSet
{
	public StringFunSet ()
	{
	}

	public static int CaculateStrLen (string str)
	{ 
		int lenTotal = 0;
		int len = str.Length; 
		int asc;
		for (int i = 0; i < len; i++) { 
			asc = Convert.ToChar (str [i]); 
			if (asc < 0 || asc > 127)
				lenTotal = lenTotal + 2;
			else
				lenTotal = lenTotal + 1; 
		}
		return lenTotal; 
	}

	public static string CutStrByLen(string str,int needLen = 8) { 
		string backStr = "";
		int lenTotal = 0;
		int len = str.Length; 
		int asc;
		for (int i = 0; i < len; i++) 
		{ 
			backStr+=str[i];
			asc = Convert.ToChar(str[i]); 
			if (asc < 0 || asc > 127)
				lenTotal = lenTotal + 2; 
			else
				lenTotal = lenTotal + 1; 

			if(lenTotal >= needLen)
			{
				backStr+="..";
				break;
			}
		}
		return backStr; 
	}
}


