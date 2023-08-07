using SharedDataSpace;
using System;
using SharedDataSpace;

public class ViewDirectory {

	List<string> surfFiles = new List<string>();
	List<string> surfDirs = new List<string>();
	List<string> allFiles = new List<string>();
	List<string> allDirs = new List<string>();
	SharedData sharedData;
	public ViewDirectory(SharedData sharedDataInstance) {
		sharedData = sharedDataInstance;
		surfFiles = Directory.GetFiles(sharedData.WorkingDir, "*").ToList();
		surfDirs = Directory.GetDirectories(sharedData.WorkingDir, "*").ToList();
        allFiles = Directory.GetFiles(sharedData.WorkingDir, "*", SearchOption.AllDirectories).ToList();
        allDirs = Directory.GetDirectories(sharedData.WorkingDir, "*", SearchOption.AllDirectories).ToList();
    }

	public void surf() {
        //Shows surface of directory
        Console.WriteLine();
        foreach ( string surfDir in surfDirs ) {
            Console.Write("    ");//4 spaces for readability
            Console.WriteLine(surfDir.Replace(Path.GetDirectoryName(surfDir), ""));
        }
        foreach (string surfFile in surfFiles) {
            Console.Write("    ");//4 spaces for reeadability
			Console.WriteLine(Path.GetFileName(surfFile));
		}
        Console.WriteLine();
	}

	public void all() {
        //Shows all files and paths in directory
        foreach ( string deepDir in allDirs ) {
            Console.Write("    ");//4 spaces for readability
            Console.WriteLine(deepDir.Replace(Path.GetDirectoryName(deepDir), ""));
        }
        foreach ( string deepFile in allFiles ) {
            Console.Write("    ");//4 spaces for readability
            Console.WriteLine(Path.GetFileName(deepFile));
        }
    }
}
