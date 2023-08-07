using SharedDataSpace;
using System;
using SharedDataSpace;


public class Copy {

	List<string> allFiles;
	SharedData sharedData;

	public Copy(SharedData sharedDataInstance) {
		sharedData = sharedDataInstance;
	}

	public void path(List<string> inputs) {
		//copies all files in current directory
		allFiles = Directory.GetFiles(sharedData.WorkingDir, "*").ToList();
		foreach (string file in allFiles) {
			File.Copy($@"{sharedData.WorkingDir}\{Path.GetFileName(file)}", $@"{inputs[0]}\(Copy){Path.GetFileName(file)}");
		}

	}

	public void file(List<string> inputs) {
		//copies a file in to target directory
		string file = $@"{inputs[0]}";
		string targetDir = $"{inputs[1]}";

		File.Copy(file, targetDir);
	}





}
