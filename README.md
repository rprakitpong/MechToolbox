# MechToolbox
Generate CAD models of mechanical parts using Inventor SDK

![](Media/sample.gif)
Sample from v0.9

## How to use:
- clone the repo
- the latest app release is in Source/Builds
- run the app, click "Get File" to select a part from PartsLibrary
- after Inventor initializes, form will populate with parameters
- customize your parameters, and parameters are automatically saved on each change

## Contribute:
Contribute a model to PartsLibrary
- the app searches for and changes UserParameter of the model, so make sure your dimensions are linked to them
- there must be 1 UserParameter called "type" that has text as its input field, that is the name of the model
- right now the app supports a maximum of 12 UserParameter (not incl. name) 

## App TODO:
- not update if blank, instead of throwing textbox
- refactoring polling into async in InventorModelWrapper.initPart
- SolidWorksModelWrapper class, and the guards to separate between sldr and ipt
- read/write FileStream of ipt instead of opening Inventor instance
- complete Inventor units

## CAD TODO:
- make bevel gear
- make other models

## Releases:
v0.9.1
- instead of manually opening part instance and modifying current part, app takes part path, copies part, open part, save on parameter modification
- Inventor window can be ignored completely while using app (still need Inventor instance opened per Inventor SDK's implementation)

v0.9
- supports all options of length, angle, and unitless found in inventor
- can't change unit in app
- supports 12 user parameters max

v0.8
- supports 3 units max (cm, mm, in), supports 4 user parameters max
- only has test model in PartsLibrary folder
- fully generalized for any model with matched user parameters formatting