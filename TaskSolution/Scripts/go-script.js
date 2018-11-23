function init() {
    jQuery.ajax({
        url: "/Diagrama/ItensDiagramaGoJS",
        type: 'POST',
        dataType: 'json',
        success: function (response) {

            if (window.goSamples) goSamples();  // init for these samples -- you don't need to call this
            var $ = go.GraphObject.make;  // for conciseness in defining templates
            myDiagram =
                $(go.Diagram, "diagramaGoJS",
                    {
                        allowCopy: false,
                        initialContentAlignment: go.Spot.Center,
                        "draggingTool.dragsTree": true,
                        "commandHandler.deletesTree": true,
                        layout:
                            $(go.TreeLayout,
                                { angle: 90 }),
                        "undoManager.isEnabled": true
                    });

            // when the document is modified, add a "*" to the title and enable the "Save" button
            myDiagram.addDiagramListener("Modified", function (e) {
                var button = document.getElementById("SaveButton");
                if (button) button.disabled = !myDiagram.isModified;
                var idx = document.title.indexOf("*");
                if (myDiagram.isModified) {
                    if (idx < 0) document.title += "*";
                } else {
                    if (idx >= 0) document.title = document.title.substr(0, idx);
                }
            });

            var bluegrad = $(go.Brush, "Linear", { 0: "#C4ECFF", 1: "#70D4FF" });
            var greengrad = $(go.Brush, "Linear", { 0: "#B1E2A5", 1: "#7AE060" });

            // each action is represented by a shape and some text
            var actionTemplate =
                $(go.Panel, "Horizontal",
                    $(go.Shape,
                        { width: 12, height: 12 },
                        new go.Binding("figure"),
                        new go.Binding("fill")
                    ),
                    $(go.TextBlock,
                        { font: "10pt Verdana, sans-serif" },
                        new go.Binding("text")
                    )
                );

            // each regular Node has body consisting of a title followed by a collapsible list of actions,
            // controlled by a PanelExpanderButton, with a TreeExpanderButton underneath the body
            myDiagram.nodeTemplate =  // the default node template
                $(go.Node, "Vertical",
                    { selectionObjectName: "BODY", deletable: false },
                    // the main "BODY" consists of a RoundedRectangle surrounding nested Panels
                    $(go.Panel, "Auto",
                        { name: "BODY" },
                        $(go.Shape, "Rectangle",
                            { fill: bluegrad, stroke: null }
                        ),
                        $(go.Panel, "Vertical",
                            { margin: 3 },
                            // the title
                            $(go.TextBlock,
                                {
                                    stretch: go.GraphObject.Horizontal,
                                    font: "bold 12pt Verdana, sans-serif"
                                },
                                new go.Binding("text", "question")
                            ),
                            // the optional list of actions
                            $(go.Panel, "Vertical",
                                { stretch: go.GraphObject.Horizontal, visible: false },  // not visible unless there is more than one action
                                new go.Binding("visible", "actions", function (acts) {
                                    return (Array.isArray(acts) && acts.length > 0);
                                }),
                                // headered by a label and a PanelExpanderButton inside a Table
                                $(go.Panel, "Table",
                                    { stretch: go.GraphObject.Horizontal },
                                    $(go.TextBlock, "Choices",
                                        {
                                            alignment: go.Spot.Left,
                                            font: "10pt Verdana, sans-serif"
                                        }
                                    ),
                                    $("PanelExpanderButton", "COLLAPSIBLE",  // name of the object to make visible or invisible
                                        { column: 1, alignment: go.Spot.Right }
                                    )
                                ), // end Table panel
                                // with the list data bound in the Vertical Panel
                                $(go.Panel, "Vertical",
                                    {
                                        name: "COLLAPSIBLE",  // identify to the PanelExpanderButton
                                        padding: 2,
                                        stretch: go.GraphObject.Horizontal,  // take up whole available width
                                        background: "white",  // to distinguish from the node's body
                                        defaultAlignment: go.Spot.Left,  // thus no need to specify alignment on each element
                                        itemTemplate: actionTemplate  // the Panel created for each item in Panel.itemArray
                                    },
                                    new go.Binding("itemArray", "actions")  // bind Panel.itemArray to nodedata.actions
                                )  // end action list Vertical Panel
                            )  // end optional Vertical Panel
                        )  // end outer Vertical Panel
                    ),  // end "BODY"  Auto Panel
                    $(go.Panel,  // this is underneath the "BODY"
                        { height: 15 },  // always this height, even if the TreeExpanderButton is not visible
                        $("TreeExpanderButton")
                    )
                );

            // define a second kind of Node:
            myDiagram.nodeTemplateMap.add("Terminal",
                $(go.Node, "Spot",
                    { deletable: false },
                    $(go.Shape, "Circle",
                        { width: 55, height: 55, fill: greengrad, stroke: null }
                    ),
                    $(go.TextBlock,
                        { font: "10pt Verdana, sans-serif" },
                        new go.Binding("text")
                    )
                )
            );

            myDiagram.linkTemplate =
                $(go.Link, go.Link.Orthogonal,
                    { deletable: false, corner: 10 },
                    $(go.Shape,
                        { strokeWidth: 2 }
                    ),
                    $(go.TextBlock, go.Link.OrientUpright,
                        {
                            background: "white",
                            visible: false,  // unless the binding sets it to true for a non-empty string
                            segmentIndex: -2,
                            segmentOrientation: go.Link.None
                        },
                        new go.Binding("text", "answer"),
                        // hide empty string;
                        // if the "answer" property is undefined, visible is false due to above default setting
                        new go.Binding("visible", "answer", function (a) { return (a ? true : false); })
                    )
                );
            
            var nodeDataArray = [];
            var linkDataArray = [];

            nodeDataArray.push({
                key: 0,
                question: "TaskSolution"
            });

            jQuery.each(response, function (i, item) {
                nodeDataArray.push({
                    key: item.key,
                    question: item.question
                });
                
                linkDataArray.push({
                    from: item.idPai == null ? 0 : item.idPai,
                    to: item.key
                });
            });

            // create the Model with the above data, and assign to the Diagram
            myDiagram.model =
                $(go.GraphLinksModel,
                    {
                        nodeDataArray: nodeDataArray,
                        linkDataArray: linkDataArray
                    });
        }
    });
}