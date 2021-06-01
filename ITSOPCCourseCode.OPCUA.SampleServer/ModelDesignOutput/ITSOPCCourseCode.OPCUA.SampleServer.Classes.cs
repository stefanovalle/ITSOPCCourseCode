/* ========================================================================
 * Copyright (c) 2005-2021 The OPC Foundation, Inc. All rights reserved.
 *
 * OPC Foundation MIT License 1.00
 *
 * Permission is hereby granted, free of charge, to any person
 * obtaining a copy of this software and associated documentation
 * files (the "Software"), to deal in the Software without
 * restriction, including without limitation the rights to use,
 * copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the
 * Software is furnished to do so, subject to the following
 * conditions:
 *
 * The above copyright notice and this permission notice shall be
 * included in all copies or substantial portions of the Software.
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
 * EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
 * OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
 * NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
 * HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
 * WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
 * FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
 * OTHER DEALINGS IN THE SOFTWARE.
 *
 * The complete license agreement can be found here:
 * http://opcfoundation.org/License/MIT/1.00/
 * ======================================================================*/

using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Runtime.Serialization;
using Opc.Ua;

namespace ITSOPCCourseCode.OPCUA.SampleServer
{
    #region Object Identifiers
    /// <summary>
    /// A class that declares constants for all Objects in the Model Design.
    /// </summary>
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public static partial class Objects
    {
        /// <summary>
        /// The identifier for the Bike Object.
        /// </summary>
        public const uint Bike = 2;
    }
    #endregion

    #region ObjectType Identifiers
    /// <summary>
    /// A class that declares constants for all ObjectTypes in the Model Design.
    /// </summary>
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public static partial class ObjectTypes
    {
        /// <summary>
        /// The identifier for the BikeType ObjectType.
        /// </summary>
        public const uint BikeType = 1;
    }
    #endregion

    #region Variable Identifiers
    /// <summary>
    /// A class that declares constants for all Variables in the Model Design.
    /// </summary>
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public static partial class Variables
    {
        /// <summary>
        /// The identifier for the BikeType_CurrentSpeed Variable.
        /// </summary>
        public const uint BikeType_CurrentSpeed = 119;

        /// <summary>
        /// The identifier for the BikeType_CurrentSpeed_EURange Variable.
        /// </summary>
        public const uint BikeType_CurrentSpeed_EURange = 123;

        /// <summary>
        /// The identifier for the Bike_CurrentSpeed Variable.
        /// </summary>
        public const uint Bike_CurrentSpeed = 3;

        /// <summary>
        /// The identifier for the Bike_CurrentSpeed_EURange Variable.
        /// </summary>
        public const uint Bike_CurrentSpeed_EURange = 111;
    }
    #endregion

    #region Object Node Identifiers
    /// <summary>
    /// A class that declares constants for all Objects in the Model Design.
    /// </summary>
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public static partial class ObjectIds
    {
        /// <summary>
        /// The identifier for the Bike Object.
        /// </summary>
        public static readonly ExpandedNodeId Bike = new ExpandedNodeId(ITSOPCCourseCode.OPCUA.SampleServer.Objects.Bike, ITSOPCCourseCode.OPCUA.SampleServer.Namespaces.SamplePlant);
    }
    #endregion

    #region ObjectType Node Identifiers
    /// <summary>
    /// A class that declares constants for all ObjectTypes in the Model Design.
    /// </summary>
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public static partial class ObjectTypeIds
    {
        /// <summary>
        /// The identifier for the BikeType ObjectType.
        /// </summary>
        public static readonly ExpandedNodeId BikeType = new ExpandedNodeId(ITSOPCCourseCode.OPCUA.SampleServer.ObjectTypes.BikeType, ITSOPCCourseCode.OPCUA.SampleServer.Namespaces.SamplePlant);
    }
    #endregion

    #region Variable Node Identifiers
    /// <summary>
    /// A class that declares constants for all Variables in the Model Design.
    /// </summary>
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public static partial class VariableIds
    {
        /// <summary>
        /// The identifier for the BikeType_CurrentSpeed Variable.
        /// </summary>
        public static readonly ExpandedNodeId BikeType_CurrentSpeed = new ExpandedNodeId(ITSOPCCourseCode.OPCUA.SampleServer.Variables.BikeType_CurrentSpeed, ITSOPCCourseCode.OPCUA.SampleServer.Namespaces.SamplePlant);

        /// <summary>
        /// The identifier for the BikeType_CurrentSpeed_EURange Variable.
        /// </summary>
        public static readonly ExpandedNodeId BikeType_CurrentSpeed_EURange = new ExpandedNodeId(ITSOPCCourseCode.OPCUA.SampleServer.Variables.BikeType_CurrentSpeed_EURange, ITSOPCCourseCode.OPCUA.SampleServer.Namespaces.SamplePlant);

        /// <summary>
        /// The identifier for the Bike_CurrentSpeed Variable.
        /// </summary>
        public static readonly ExpandedNodeId Bike_CurrentSpeed = new ExpandedNodeId(ITSOPCCourseCode.OPCUA.SampleServer.Variables.Bike_CurrentSpeed, ITSOPCCourseCode.OPCUA.SampleServer.Namespaces.SamplePlant);

        /// <summary>
        /// The identifier for the Bike_CurrentSpeed_EURange Variable.
        /// </summary>
        public static readonly ExpandedNodeId Bike_CurrentSpeed_EURange = new ExpandedNodeId(ITSOPCCourseCode.OPCUA.SampleServer.Variables.Bike_CurrentSpeed_EURange, ITSOPCCourseCode.OPCUA.SampleServer.Namespaces.SamplePlant);
    }
    #endregion

    #region BrowseName Declarations
    /// <summary>
    /// Declares all of the BrowseNames used in the Model Design.
    /// </summary>
    public static partial class BrowseNames
    {
        /// <summary>
        /// The BrowseName for the Bike component.
        /// </summary>
        public const string Bike = "My Bike";

        /// <summary>
        /// The BrowseName for the BikeType component.
        /// </summary>
        public const string BikeType = "A bike";

        /// <summary>
        /// The BrowseName for the CurrentSpeed component.
        /// </summary>
        public const string CurrentSpeed = "CurrentSpeed";
    }
    #endregion

    #region Namespace Declarations
    /// <summary>
    /// Defines constants for all namespaces referenced by the model design.
    /// </summary>
    public static partial class Namespaces
    {
        /// <summary>
        /// The URI for the OpcUa namespace (.NET code namespace is 'Opc.Ua').
        /// </summary>
        public const string OpcUa = "http://opcfoundation.org/UA/";

        /// <summary>
        /// The URI for the OpcUaXsd namespace (.NET code namespace is 'Opc.Ua').
        /// </summary>
        public const string OpcUaXsd = "http://opcfoundation.org/UA/2008/02/Types.xsd";

        /// <summary>
        /// The URI for the SamplePlant namespace (.NET code namespace is 'ITSOPCCourseCode.OPCUA.SampleServer').
        /// </summary>
        public const string SamplePlant = "http://iiot.its/SamplePlant";
    }
    #endregion

    #region BikeState Class
    #if (!OPCUA_EXCLUDE_BikeState)
    /// <summary>
    /// Stores an instance of the BikeType ObjectType.
    /// </summary>
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public partial class BikeState : BaseObjectState
    {
        #region Constructors
        /// <summary>
        /// Initializes the type with its default attribute values.
        /// </summary>
        public BikeState(NodeState parent) : base(parent)
        {
        }

        /// <summary>
        /// Returns the id of the default type definition node for the instance.
        /// </summary>
        protected override NodeId GetDefaultTypeDefinitionId(NamespaceTable namespaceUris)
        {
            return Opc.Ua.NodeId.Create(ITSOPCCourseCode.OPCUA.SampleServer.ObjectTypes.BikeType, ITSOPCCourseCode.OPCUA.SampleServer.Namespaces.SamplePlant, namespaceUris);
        }

        #if (!OPCUA_EXCLUDE_InitializationStrings)
        /// <summary>
        /// Initializes the instance.
        /// </summary>
        protected override void Initialize(ISystemContext context)
        {
            base.Initialize(context);
            Initialize(context, InitializationString);
            InitializeOptionalChildren(context);
        }

        /// <summary>
        /// Initializes the instance with a node.
        /// </summary>
        protected override void Initialize(ISystemContext context, NodeState source)
        {
            InitializeOptionalChildren(context);
            base.Initialize(context, source);
        }

        /// <summary>
        /// Initializes the any option children defined for the instance.
        /// </summary>
        protected override void InitializeOptionalChildren(ISystemContext context)
        {
            base.InitializeOptionalChildren(context);
        }

        #region Initialization String
        private const string InitializationString =
           "AQAAABsAAABodHRwOi8vaWlvdC5pdHMvU2FtcGxlUGxhbnT/////BGCAAgEAAAABABAAAABCaWtlVHlw" +
           "ZUluc3RhbmNlAQEBAAEBAQABAAAA/////wEAAAAVYIkKAgAAAAEADAAAAEN1cnJlbnRTcGVlZAEBdwAA" +
           "LwEAQAl3AAAAAAv/////AwP/////AQAAABVgiQoCAAAAAAAHAAAARVVSYW5nZQEBewAALgBEewAAAAEA" +
           "dAP/////AQH/////AAAAAA==";
        #endregion
        #endif
        #endregion

        #region Public Properties
        /// <remarks />
        public AnalogItemState<double> CurrentSpeed
        {
            get
            {
                return m_currentSpeed;
            }

            set
            {
                if (!Object.ReferenceEquals(m_currentSpeed, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_currentSpeed = value;
            }
        }
        #endregion

        #region Overridden Methods
        /// <summary>
        /// Populates a list with the children that belong to the node.
        /// </summary>
        /// <param name="context">The context for the system being accessed.</param>
        /// <param name="children">The list of children to populate.</param>
        public override void GetChildren(
            ISystemContext context,
            IList<BaseInstanceState> children)
        {
            if (m_currentSpeed != null)
            {
                children.Add(m_currentSpeed);
            }

            base.GetChildren(context, children);
        }

        /// <summary>
        /// Finds the child with the specified browse name.
        /// </summary>
        protected override BaseInstanceState FindChild(
            ISystemContext context,
            QualifiedName browseName,
            bool createOrReplace,
            BaseInstanceState replacement)
        {
            if (QualifiedName.IsNull(browseName))
            {
                return null;
            }

            BaseInstanceState instance = null;

            switch (browseName.Name)
            {
                case ITSOPCCourseCode.OPCUA.SampleServer.BrowseNames.CurrentSpeed:
                {
                    if (createOrReplace)
                    {
                        if (CurrentSpeed == null)
                        {
                            if (replacement == null)
                            {
                                CurrentSpeed = new AnalogItemState<double>(this);
                            }
                            else
                            {
                                CurrentSpeed = (AnalogItemState<double>)replacement;
                            }
                        }
                    }

                    instance = CurrentSpeed;
                    break;
                }
            }

            if (instance != null)
            {
                return instance;
            }

            return base.FindChild(context, browseName, createOrReplace, replacement);
        }
        #endregion

        #region Private Fields
        private AnalogItemState<double> m_currentSpeed;
        #endregion
    }
    #endif
    #endregion
}