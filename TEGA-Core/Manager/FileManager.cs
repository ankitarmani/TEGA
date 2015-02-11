using System;
using System.IO;
using System.Net;
using Android.Util;
using System.Text;

namespace TEGACore
{
	public class FileManager
	{
		private const string TAG = "FileManager";
		private static FileManager instance = null;

		private FileManager ()
		{
		}

		public static FileManager Instance {
			get {
				if (instance == null) {
					instance = new FileManager ();
				}
				return instance;
			}
		}

		public bool isExternalStorageMounted ()
		{
			if (Android.OS.Environment.ExternalStorageState == Android.OS.Environment.MediaMounted) {
				return true;
			} else {
				return false;
			}
		}

		public bool directoryExists (string dirPath)
		{
			if (Directory.Exists (dirPath)) {
				return true;
			} else {
				return false;
			}
		}

		public void createDirectory (string dirPath)
		{
			Directory.CreateDirectory (dirPath);
		}

		public bool fileExists (string filePath)
		{
			if (File.Exists (filePath)) {
				return true;
			} else {
				return false;
			}
		}

		public void createFile (string filePath)
		{
			File.Create (filePath);
		}

		public void deleteFile (string filePath)
		{
			File.Delete (filePath);
		}

		public string readFile (string filePath)
		{
			StreamReader reader = new StreamReader (filePath);
			string fileContent = reader.ReadToEnd ();
			reader.Close ();
			return fileContent;
		}

		public byte[] readFileAsByteArray (string filePath)
		{
			return File.ReadAllBytes (filePath);
		}

		public void writeFile (string filePath, string fileContent)
		{
			StreamWriter writer = new StreamWriter (filePath);
			writer.Write (fileContent);
			writer.Close ();
		}

		public string getHumanReadableSize (long size)
		{
			string[] suffixes = { "B", "KB", "MB", "GB", "TB", "PB" };
			int place = Convert.ToInt32 (Math.Floor (Math.Log (size, 1024)));
			double num = Math.Round (size / Math.Pow (1024, place), 1);
			return num.ToString () + suffixes [place];
		}
	}
}

