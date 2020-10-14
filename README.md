# SimplifiedDriver
This is a simple C # console application that accepts "packages" that are
sequence of characters typed in the console, their processing and display of the answer.
Packets are processed immediately after the last character is typed, there must not be a need to press ENTER.
This description somehow simulates a typical (simplified) description of the communication between
the driver plugin and device.

You can enter an arbitrary number of symbols into the console, but as soon as the sequence of characters ":E" is entered, 
the application immediately begins to process the entire text for searching for a package, in cases of a positive answer, 
determines the command and the validation of arguments
The start of packet stream sequence is a "P" character.


Payload structure is defined as follows:
[COMMAND_CHARACTER][:][PARAMETERS][:]
where
	[:] is character colon : (58)
	COMMAND_CHARACTER is a command definition command
	PARAMETERS is a set of parameters, concrete definition is a command specific

At the moment, the ability to receive and process only 2 packets has been implemented. These are "Text" and "Sound"
The application is case sensitive.
Package "Text"- PT: your text: E
Package "Sound"- PS:(int)frequency,(int)duration: E
