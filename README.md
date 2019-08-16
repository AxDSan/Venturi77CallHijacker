# Venturi77CallHijacker
 KoiVM,EazVM,AgileVM Patcher - Will Keep On Updating (This is a rushed release, i will add all the features tomorrow. Expect lots of updates!) Detailed Guide TomorrowTM

Team Venturi77 Members (Current):

TobitoFatito - https://github.com/TobitoFatitoNulled/

XSilent007 - https://github.com/XSilent007/

# How To Use
Run the venturi call hijacker exe and create a debug config.

A Config should be saved on Config.Json of the directory executed.

Run call hijacker again and inject the dll, automatic detection works perfectly.

Now after you have injected the dll you can run the .exe that you injected it into and

it should leave a debug.txt that contains the information of the calls that were handled

Ex: https://i.imgur.com/Ekomjuc.png

Now you can just create a new config and according to the debug txt you can

make it patch the calls. 

Example of method that needs Patching:

https://i.imgur.com/goFjTTm.png

Example of config made for the method:

https://i.imgur.com/1Qd1nHH.png

Care: You need to have the config.json on the same directory that the .exe is.
# Bugs:

Agile,Eaz injection is buggy some files wont inject, will fix really soon.

If you have a program to inject that is eaz/agile and wont work you can inject

it yourself with dnlib. How? 

On eaz you can follow the calls with the parameters like this

https://i.imgur.com/YLeVeTm.png

till you find this 

https://i.imgur.com/TWq3R3V.png

Then just Control+f and search for .Invoke till you find this method:

https://i.imgur.com/2kzcHMj.png

Now just edit il instructions, make sure the venturi dll is on the same directory

and that you have added it on dnlib and just edit the call like this https://i.imgur.com/0Ur15Bz.png

Click OK https://i.imgur.com/qDfliTe.png and after that make sure to click on the .HandleInvoke so you can see the dll.

Now you just save the assembly with keeping MDToken settings.

Injection done you can just make configs now to debug/patch

