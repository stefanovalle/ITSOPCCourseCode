const { OPCUAServer, DataType, Variant, VariantArrayType, StatusCodes, makeAccessLevelFlag } = require("node-opcua");

(async () => {
    try {
        const server = new OPCUAServer({
            port: 4334 // the port of the listening socket of the server
        });

        await server.initialize();

        const addressSpace = server.engine.addressSpace;
        const namespace = addressSpace.getOwnNamespace();

        const myDevice = namespace.addObject({
            organizedBy: addressSpace.rootFolder.objects,
            browseName: "MyDevice"
        });

        const counter = namespace.addVariable({
            componentOf: myDevice,
            browseName: "Counter",
            dataType: DataType.UInt32,
            minimumSamplingInterval: 100,
        });

        const method = namespace.addMethod(myDevice, {
            browseName: "Increment",

            inputArguments: [
                {
                    name: "start",
                    description: { text: "specifies the sequence start number" },
                    dataType: DataType.UInt32
                },
                {
                    name: "end",
                    description: { text: "specifies the sequence end number" },
                    dataType: DataType.UInt32
                }
            ],

            outputArguments: [
                {
                    name: "end",
                    description: { text: "the sequence end number" },
                    dataType: DataType.UInt32
                }
            ]
        });

        method.outputArguments.userAccessLevel = makeAccessLevelFlag("CurrentRead");
        method.inputArguments.userAccessLevel = makeAccessLevelFlag("CurrentRead");

        method.bindMethod((inputArguments,context,callback) => {

            const start = inputArguments[0].value;
            const end =  inputArguments[1].value;
        
            console.log("I'll count from ", start ," to ", end);

            (function myLoop(i) {
                setTimeout(function() {
                    counter.setValueFromSource({ dataType: DataType.UInt32, value: end - i + 1 })
                    if (--i) myLoop(i);
                }, 1000)
            })(end - start + 1);
        
            const callMethodResult = {
                statusCode: StatusCodes.Good,
                outputArguments: [{
                        dataType: DataType.UInt32,
                        value: end
                }]
            };
            callback(null,callMethodResult);
        });

        await server.start();
        console.log("Server is now listening ... ( press CTRL+C to stop)");
        const endpointUrl = server.endpoints[0].endpointDescriptions()[0].endpointUrl;
        console.log(" the primary server endpoint url is ", endpointUrl);
    } catch (err) {
        console.log(err);
    }
})();