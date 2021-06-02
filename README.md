Blue Print Test Notes from Colin Hatfield
--------------------------------------------------------------------------------------------------------------------------

I'm going to blog this like the initial programming scene in The Social Network so that the thought process gets captured as well

Initial Scan 1.0
--------------------------------------------------------------------------------------------------------------------------
0. Read the test document. Looked at the Xml file, researched for 5 minutes on reading Xml in .Net

Implementing 1.0
--------------------------------------------------------------------------------------------------------------------------
1. Internet says the performant and best practice is to use Xml Serializer
    - Read up on Xml Serializer
    - Try to generate class files using the Visual Studio xsd tool
       - "Error: There was an error processing 'organization.xml' - The table (Units) cannot be the child table to itself in nested relations.
       - Looking closer at the Xml file, it appears there is some weird nesting in terms of flipping between units, unit, etc.
       - Give up on Xml Serializer
2. Task - Read up some more on the requirements...  just parsing with XmlReader through seems to knock off the Console printing piece. --> completed.
3. Tast - Switch the employees between the two departments.
    - This means having to alter the Xml. Since I'm already reading it, append it to a string builder
    - This is ugly, lets check XmlDocument
    - annnnd XmlDocument leads to XDocument (winner)
4. XDocument seems to be the silver bullet for all of this:
    - Able to iterate through xml easily (completed) --> (deleted the XmlReader implementation)
    - Able to change the values in memory (completed)
    - And Newtonsoft has an XDocument to Json method. (completed).

Completed the rough functionality....  Now to clean it up / make it sustainable.
1. Things to clean up / improve
    - Lets make the file path and json path commandline arguments
    - Should I spit the print & the unit switch to make them more supportable
        - Going w/Yes because:
        - File is small and loaded in memory. Reading it twice takes no time
            - Allows us to clearly separate two pieces of logic, despite similar processing occurring.

--------------------------------------------------------------------------------------------------------------------------
*** After speaking with a friend who is also a fellow software developer, describing the ask and how I've written it up, it
sounds like I should be making this thing all enterprisey to showcase how I am able to "model the problem" and observe best practices.

1.0 Repective
--------------------------------------------------------------------------------------------------------------------------
so re-visit my solution, lets enterprisey this thing up.

initial attempt respective: 
- all performed in single program.cs file in static methods.
   - I get this is hackey, but the code is very legible and easily to maintain
- kind of rigid in that the department names being switched were done so in a very hackey manner which if this were scaled out would run into issues
    - if there are multiple departments w/the same name being asked to be switched? --> breaks since can't specify which department
    - if it's just adding people to a department that already exists --> breaks since this would alter the xml structure (resulting in two departments w/same name within same node)
    - if the ask is employee oriented and the expectation was an API that could move a single employee between departments
        - *This what drives new design*

Re-Design!!!
--------------------------------------------------------------------------------------------------------------------------
blueprint xml parser 2.0 - Build a model in memory, and an API such that it's intuitive to navigate, alter the organization structure
1. Pull it out into a library so separated from the execution, can be leveraged by other implementations
2. Interfaces to reflect that the loading xml / writing to json isn't core to the model, and this can be extended to source and output in multiple ways
3. Unit tests / Mocking
4. Intuitive API for:
    - Loading the xml
    - Building the org structure in memory
    - Alerting the org structure
        - Change department name
        - Move employee between departments
    - Saving changes to Xml
    - Outputting Json
*** This is going to take me a lot longer than the 3 hours I had initially scoped
5. Required additions
    - To be able to uniquely identify departments and employees, they'll need IDs of some some
    - Going with a top down uni-directional linking structure (kind of follows the xml read approach)
        - Department --> child departments
        - Department --> employees

Implementing 2.0
--------------------------------------------------------------------------------------------------------------------------
Create separate library BluePrintXmlLibrary
    - Added the IOrganizationLoad Interface
    - Added the XmlFileLoader implementation
    - Within XmlFileLoader, having to walk the Xml structure is taking a loong time
        - Split out GetEmployee function (bottom node, so easy)
        - Split out GetDepartment(s)
            - This is a headache with the whole (unit --> units --> unit --> units)
    - Annnnnd this is taking much longer than anticipated
    - Sleep

The morning after
--------------------------------------------------------------------------------------------------------------------------
- There is no way I'm going to complete the second implementation.
- Clean up the intial ask, write up my thought process, include everything in the submission
- Check how far I got with 2.0
    - Fixes quick bug (I'm an idiot) --> XmlLoader completed.
    - Why did I use Guids instead of just an incremental integer? I should split this out into it's own thing. oh well

- End result: Two implementation attempts
    - First is the quick and kind of cleaned up but not delivering software development standards --> (completed)
    - Second I tried to apply a lot of what I perceive to be best standards, and showcase how the requested functionality could be done in a manner
            that could be built upon going forward. DID NOT FINISH

Conclusion
--------------------------------------------------------------------------------------------------------------------------
Request: If I could get some feedback in terms of what you were looking for, it would go a long way towards putting me at ease. Was the initial attempt in
line with what you were asking for (since it was largely motivated by the understanding this should take a couple hours and I'm not very familiar with xml libraries)
or were you looking for more of a 2.0 implementation with the split out library, the test suite, the data model, etc...

Annd now to get the day started! Coffee....

- Colin.Hatfield
    
