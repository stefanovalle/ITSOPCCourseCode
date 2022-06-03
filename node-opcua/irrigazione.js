const os = require("os");
const { OPCUAServer, Variant, DataType, StatusCodes } = require("node-opcua");


(async () => {

    // Let's create an instance of OPCUAServer
    const server = new OPCUAServer({
        port: 4334, // the port of the listening socket of the server
        resourcePath: "/UA/Irrigazione", // this path will be added to the endpoint resource name
        buildInfo: {
            productName: "Irrigazione",
            buildNumber: "1",
            buildDate: new Date(2022, 5, 31)
        }
    });
    await server.initialize();
    console.log("initialized");

    const addressSpace = server.engine.addressSpace;
    const namespace = addressSpace.getOwnNamespace();

    // declare a new object
    const impianto = namespace.addObject({
        organizedBy: addressSpace.rootFolder.objects,
        browseName: "Impianto"
    });

    /**
     * RUBINETTO TYPE
     */

    // create object type
    const rubinettoType = namespace.addObjectType({
        browseName: "RubinettoType"
    });

    namespace.addVariable({
        componentOf:     rubinettoType,
        browseName:     "Stato",
        dataType:       DataType.Boolean,
        modellingRule:  "Mandatory",
        value: { dataType: DataType.Boolean, value: false}
    });


    /**
     * CENTRALINA TYPE
     */

    // create object type
    const centralinaType = namespace.addObjectType({
        browseName: "CentralinaType",
    });
    
    const modalitaEnum = namespace.addEnumerationType({
        browseName: "Modalità Enum",
        enumeration: ["auto", "manuale"]
    });

    namespace.addVariable({
        componentOf:     centralinaType,
        browseName:     "Modalita",
        dataType:       modalitaEnum,
        modellingRule:  "Mandatory",
        value: { dataType: DataType.Int32, value: 0}
    });

    rubinettoType.instantiate({
        componentOf: centralinaType,
        modellingRule:  "Mandatory",
        browseName: "Rubinetto 1"
    });
    
    const method = namespace.addMethod(centralinaType, {
        browseName: "AvviaIrrigazioneManuale",
        inputArguments: [{
            name: "Rubinetto",
            description: { text: "Numero rubinetto" },
            dataType: DataType.UInt16
        },
        {
            name: "durata",
            description: { text: "Durata dell'irrigazione (in minuti)" },
            dataType: DataType.UInt16
        }],
        outputArguments: [],
        modellingRule:  "Mandatory",
    });

    method.bindMethod((inputArguments,context,callback) => {

        console.log(context.object);

        context.object.getChildByName("Rubinetto 1").stato.setValueFromSource({ dataType: DataType.Boolean, value: true });
        
        // prossime 2 righe sono alternative
        context.object.getChildByName("Modalita").setValueFromSource({ dataType: DataType.Int32, value: 1 });
        //context.object.modalita.writeEnumValue(1);
    
        const callMethodResult = {
            statusCode: StatusCodes.Good,
            outputArguments: []
        };
        callback(null,callMethodResult);
    });
    
   

    /**
     * ISTANZE
     */

    const centralina1 = centralinaType.instantiate({
        componentOf: impianto,
        browseName: "Centralina 1"
    });
    


    await server.start();
    console.log("Server is now listening ... ( press CTRL+C to stop)");
    console.log("port ", server.endpoints[0].port);
    console.log(" the primary server endpoint url is ", server.getEndpointUrl());

    process.once("SIGINT", async () => {
        console.log("shuting down");
        await server.shutdown();
    });

})();