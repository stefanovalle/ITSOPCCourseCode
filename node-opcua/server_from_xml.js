/*global require,setInterval,console */
const opcua = require("node-opcua");

// Let's create an instance of OPCUAServer
const server = new opcua.OPCUAServer({
    port: 4334, // the port of the listening socket of the server
    resourcePath: "/UA/SampleBike", // this path will be added to the endpoint resource name
     buildInfo : {
        productName: "SampleBike",
        buildNumber: "1",
        buildDate: new Date(2022,5,24)
    },
    nodeset_filename: [
        opcua.nodesets.standard,
        "NodeSet/samplebike.xml"
      ]
});

function post_initialize() {
    console.log("initialized");
    
    server.start(function() {
        console.log("Server is now listening ... ( press CTRL+C to stop)");
        console.log("port ", server.endpoints[0].port);
        const endpointUrl = server.endpoints[0].endpointDescriptions()[0].endpointUrl;
        console.log(" the primary server endpoint url is ", endpointUrl );
    });
}

server.initialize(post_initialize);