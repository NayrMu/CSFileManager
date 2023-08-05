using System;
using System.Reflection;
using SharedDataSpace;

/*
 * TODO
 * 1. Determine if user gave a mod or not
 * 2. Allow for multiple mods
 * 3. Flesh out commands and mods
 */

public class MainPrompt {
    
    SharedData sharedData;
    List<string> commandArr;
    Type? workingCommand;
    Type[] allTypes;
    object? workingCommandInstance;
    MethodInfo? noMod;

    Dictionary<string, Type> allCommands = new Dictionary<string, Type> {
        {"cd", typeof(ChangeDirectory) }
    };

    public MainPrompt() {
        sharedData = new SharedData();
        allTypes = Assembly.GetExecutingAssembly().GetTypes();
    }

    static void Main() {
        MainPrompt prompt = new MainPrompt();
        prompt.Run();
        
    }
    public void Run() {
        while ( true ) {
            Console.Write($"{sharedData.WorkingDir}:: ");
            try {
                sharedData.CommandInput = Console.ReadLine();
                commandArr = CommandSplitter(sharedData.CommandInput);
                workingCommand = allCommands[commandArr[0]];
                noMod = workingCommand.GetMethod("noMod");
                workingCommandInstance = Activator.CreateInstance(workingCommand, sharedData);
                if ( false ) {
                    MethodInfo? modifier = workingCommand.GetMethod(commandArr[1]);
                    if ( modifier != null ) {
                        modifier.Invoke(workingCommandInstance, null);
                    }
                    else {
                        Console.WriteLine("Modifier not recognized.");
                    }
                }
                else {
                    noMod.Invoke(workingCommandInstance, new object[] { commandArr[1] });
                }
            }
            catch (ArgumentException ex) {
                Console.WriteLine(ex.Message);
                continue;
            }
            
        }
    }

    List<string> CommandSplitter(string commandStr) {

        List<string> returnArr = new List<string>();
        returnArr = commandStr.Split(" ").ToList();

        return returnArr;
    }
}
