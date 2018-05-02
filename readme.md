# PlantUml.Net
[![Build status](https://ci.appveyor.com/api/projects/status/228llgqqagi9kutg?svg=true)](https://ci.appveyor.com/project/KevReed/plantuml-net)

PlantUml.Net is a .Net wrapper for [PlantUml](http://plantuml.com/)

## Rendering Modes

PlantUml.Net can render in 2 modes, [Local](#Local) and [Remote](#Remote).

### Local Rendering

Local rendering mode uses a local copy of PlantUml to render diagrams.

### Remote Rendering

Remote rendering mode uses the PlantUml hosted service to render diagrams


## Requirements

### Java

Install [Java](https://java.com/en/download/)
Ensure that the JAVA_HOME environment variable is set

### GraphViz Dot (optional)

GraphViz Dot is required for [Local](#Local) rendering mode of any diagram other than sequence.

Install [GraphViz Dot](https://graphviz.gitlab.io/download/)
You may need to set the GRAPHVIZ_DOT environment variable

see the [PlantUml documentation](http://plantuml.com/graphviz-dot) for more detailed instructions

## Installation

Add to your project via NuGet

## Usage

```csharp

var factory = new RendererFactory();

var renderer = factory.CreateRenderer(new PlantUmlSettings());

var bytes = renderer.Render("Bob -> Alice : Hello", OutputFormat.Png);
File.WriteAllBytes("out.png", bytes);

```