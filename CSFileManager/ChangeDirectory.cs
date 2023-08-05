using System;
using SharedDataSpace;
public class ChangeDirectory {
	
	SharedData sharedData;
	public ChangeDirectory(SharedData sharedDataInstance) {
		sharedData = sharedDataInstance;
	}

	public void noMod(string destinationDir) {
		sharedData.WorkingDir = destinationDir;
	}

	public void upDir() {
		string parentDir = sharedData.WorkingDirInfo.Parent.FullName;
		sharedData.WorkingDir = parentDir;
	}
}
