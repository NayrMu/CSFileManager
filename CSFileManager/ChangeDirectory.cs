using System;
using SharedDataSpace;
public class ChangeDirectory {
	
	SharedData sharedData;
	public ChangeDirectory(SharedData sharedDataInstance) {
		sharedData = sharedDataInstance;
	}

	public void dDir( List<string> destinationDir) {
		sharedData.WorkingDir = string.Join("", destinationDir);
	}

	public void upDir() {
		string parentDir = sharedData.WorkingDirInfo.Parent.FullName;
		sharedData.WorkingDir = parentDir;
	}

	public void cu(List<string> destinationDir) {
		sharedData.WorkingDir = @$"{sharedData.WorkingDir}\{string.Join("", destinationDir)}";
    }

	public void root() {
		sharedData.WorkingDir = sharedData.homeDir;

    }
}
