(Microsoft Visual Studio 2013, version 12.0.21005.1  |  Microsoft .NET Framework, version 4.7.03062)

# Features
## HomeScreen

![image](https://user-images.githubusercontent.com/53065114/90325915-ddb13000-dfab-11ea-8f86-a4d16c19bf12.png)

### Detail:
Mode Setting area: switch mode (real-time and simulation), display the COM port connection status, current time. 
Communication Status area: display COM port connection status, sending/receiving status (only works in real-time mode).

![image](https://user-images.githubusercontent.com/53065114/90325968-8069ae80-dfac-11ea-89f5-f06d7918d525.png)
![image](https://user-images.githubusercontent.com/53065114/90327640-df84ee80-dfbf-11ea-967c-8aa3c8185aa6.png)

AGVs Monitoring area: monitor basic parameters of AGV:
+ Status
+ Position (ExitNode, Orient, DistanceToExitNode)
+ Velocity
+ AGV is running, the information row will be green

Tasks Monitoring area: display AGV's task information:
+ Status
+ Type
+ AGV doing the task
+ Pick node
+ Drop node
+ Pallet code

![image](https://user-images.githubusercontent.com/53065114/90327524-82d50400-dfbe-11ea-93b1-fca3f0220224.png)
![image](https://user-images.githubusercontent.com/53065114/90327614-87e68300-dfbf-11ea-91db-ef1cd58625dc.png)

Map display area:
+ Monitoring the position (the AGV is green, the pallet is loaded)
+ Show AGV path on map (right click to display AGV current path)
+ Display pallet density in stock
+ Add a new pallet by pressing the 'Add' button in the Input area

![image](https://user-images.githubusercontent.com/53065114/90327743-05f75980-dfc1-11ea-8fc1-124dc1273349.png)

## Communication tab
Config port COM in real time mode.

![image](https://user-images.githubusercontent.com/53065114/90327774-5cfd2e80-dfc1-11ea-85f6-1768de6f92a9.png)

## AGVs tab
+ Add/Remove AGV: add new AGV to the system with initialization position parameters or remove AGV from the system.
+ Monitoring: monitoring the AGV parameters such as status, position, current path, battery capacity, velocity graph, line detection error graph.

![image](https://user-images.githubusercontent.com/53065114/90327830-b7968a80-dfc1-11ea-89da-8415c9a0a643.png)
![image](https://user-images.githubusercontent.com/53065114/90327833-ba917b00-dfc1-11ea-97fa-08e701b84e67.png)

## Tasks tab
+ Add/remove tasks manually
+ Monitoring all current tasks

![image](https://user-images.githubusercontent.com/53065114/90327967-c5004480-dfc2-11ea-949d-a723e3eec936.png)

## Orders tab
Choose pallets for automatic delivery.

![image](https://user-images.githubusercontent.com/53065114/90327994-098be000-dfc3-11ea-8391-74473ce980f8.png)

## Warehouse Data tab
Display node map data table and import/export information of pallets retrieved from the database.

![image](https://user-images.githubusercontent.com/53065114/90328031-58397a00-dfc3-11ea-948e-c336db859d97.png)
![image](https://user-images.githubusercontent.com/53065114/90328033-5b346a80-dfc3-11ea-85d2-c6a3f04a54e0.png)

## Report tab
Export report as word file with information on existing pallet in stock, with pre-formatted report template (template).
When you need to export the report file, you need to fill out the reporter's information, then upload the available template, click the Preview button to preview the information that will be exported.
Click the Save as button to export the newly created report file (as a word file).

![image](https://user-images.githubusercontent.com/53065114/90328082-de55c080-dfc3-11ea-8ab1-e41fc143b06f.png)

Report file example:

![image](https://user-images.githubusercontent.com/53065114/90328088-ef9ecd00-dfc3-11ea-9e86-7a2bc0cb9956.png)
