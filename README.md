# MechToolbox
Generate CAD models of mechanical parts using Inventor SDK

## How to use:
- clone the repo
- the latest app release is in Source/Builds
- open the model that you want to parameterize from PartsLibrary
- open the app, customize your parameters, select unit, and click ok
- go back to your Inventor window and your model should be updated

## Releases:
v0.8
- supports 3 units max (cm, mm, in)
- supports 4 user parameters max
- only has test model in PartsLibrary folder
- fully generalized for any model with matched user parameters formatting

## Contribute:
Contribute a model to PartsLibrary
- the app searches for and changes UserParameter of the model, so make sure your dimensions are linked to them
- there must be 1 UserParameter called "type" that has text as its input field, that is the name of the model
- right now the app supports a maximum of 4 UserParameter (not incl. name) 

## App TODO:
- throw exception at weird parameters
- unit test
- comment the code

## CAD TODO:
- make spur gear
- make other models