using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Reflection;
using Microsoft.CSharp;
using Microsoft.VisualBasic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Text;
using System.CodeDom;

namespace AP.CHRP.XRDB.UL.GenDTCls
{
    public class GenerateTableCS
    {

        public void AddProperties(ref CodeTypeDeclaration targetClass)
        {
            // Declare the read-only Width property.
            CodeMemberProperty widthProperty = new CodeMemberProperty();
            widthProperty.Attributes =
                MemberAttributes.Public | MemberAttributes.Final;
            widthProperty.Name = "Width";
            widthProperty.HasGet = true;
            widthProperty.Type = new CodeTypeReference(typeof(System.Double));
            widthProperty.Comments.Add(new CodeCommentStatement(
                "The Width property for the object."));
            widthProperty.GetStatements.Add(new CodeMethodReturnStatement(
                new CodeFieldReferenceExpression(
                new CodeThisReferenceExpression(), "widthValue")));
            targetClass.Members.Add(widthProperty);

            // Declare the read-only Height property.
            CodeMemberProperty heightProperty = new CodeMemberProperty();
            heightProperty.Attributes =
                MemberAttributes.Public | MemberAttributes.Final;
            heightProperty.Name = "Height";
            heightProperty.HasGet = true;
            heightProperty.Type = new CodeTypeReference(typeof(System.Double));
            heightProperty.Comments.Add(new CodeCommentStatement(
                "The Height property for the object."));
            heightProperty.GetStatements.Add(new CodeMethodReturnStatement(
                new CodeFieldReferenceExpression(
                new CodeThisReferenceExpression(), "heightValue")));
            targetClass.Members.Add(heightProperty);

            // Declare the read only Area property.
            CodeMemberProperty areaProperty = new CodeMemberProperty();
            areaProperty.Attributes =
                MemberAttributes.Public | MemberAttributes.Final;
            areaProperty.Name = "Area";
            areaProperty.HasGet = true;
            areaProperty.Type = new CodeTypeReference(typeof(System.Double));
            areaProperty.Comments.Add(new CodeCommentStatement(
                "The Area property for the object."));

            // Create an expression to calculate the area for the get accessor  
            // of the Area property.
            CodeBinaryOperatorExpression areaExpression =
                new CodeBinaryOperatorExpression(
                new CodeFieldReferenceExpression(
                new CodeThisReferenceExpression(), "widthValue"),
                CodeBinaryOperatorType.Multiply,
                new CodeFieldReferenceExpression(
                new CodeThisReferenceExpression(), "heightValue"));
            areaProperty.GetStatements.Add(
                new CodeMethodReturnStatement(areaExpression));
            targetClass.Members.Add(areaProperty);
        }


        public CodeCompileUnit table_CodeCompileUnit()
        {
            CodeCompileUnit compileUnit = new CodeCompileUnit();


            CodeNamespace globalNamespace = new CodeNamespace();
            globalNamespace.Imports.Add(new CodeNamespaceImport("System"));
            globalNamespace.Imports.Add(new CodeNamespaceImport("System.Data"));
            globalNamespace.Imports.Add(new CodeNamespaceImport("System.Data.Common"));


            // globalNamespace.Comments = string.Empty;    you cannot do this
            compileUnit.Namespaces.Add(globalNamespace);



            CodeNamespace codedomsamplenamespace = new CodeNamespace("PIHMS.DBAccess");


            CodeTypeDeclaration newType = new CodeTypeDeclaration("tbl_mas_staff");
            newType.Attributes = MemberAttributes.Public;
            newType.IsPartial = true;


            CodeConstructor constructor = new CodeConstructor();
            constructor.Attributes = MemberAttributes.Public;
            CodeMethodInvokeExpression constructorexp = new CodeMethodInvokeExpression(new CodeTypeReferenceExpression("System.Console"), "WriteLine", new CodePrimitiveExpression("Inside CodeDomSample Constructor ..."));
            constructor.Statements.Add(constructorexp);
            newType.Members.Add(constructor);


            CodeTypeReference ctr;
            int i = 1;
            if (i == 0)
            {
                ctr = new CodeTypeReference(typeof(Nullable<>));
                ctr.TypeArguments.Add(new CodeTypeReference("System.Int32"));
            }
            else
            {
                ctr = new CodeTypeReference("long?");
            }



            CodeMemberField property0 = new CodeMemberField();
            property0.Name = "StringProperty_1";
            property0.Type = ctr;
            property0.Attributes = MemberAttributes.Private;

            newType.Members.Add(property0);

            // Declares a property of type String named StringProperty.
            CodeMemberProperty property1 = new CodeMemberProperty();
            property1.Name = "StringProperty";
            property1.Type = ctr;
            property1.Attributes = MemberAttributes.Public;
            property1.GetStatements.Add(new CodeMethodReturnStatement(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "testStringField")));
            property1.SetStatements.Add(new CodeAssignStatement(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "testStringField"), new CodePropertySetValueReferenceExpression()));

            newType.Members.Add(property1);

            //            AddProperties(ref newType);


            codedomsamplenamespace.Types.Add(newType);



            CodeMemberField field2 = new CodeMemberField("System.String", "_name");
            field2.Attributes = MemberAttributes.Private;
            newType.Members.Add(field2);

            CodeMemberProperty propertyField2 = new CodeMemberProperty();
            propertyField2.Attributes = MemberAttributes.Public;
            propertyField2.Name = "Name";
            propertyField2.HasGet = true;
            propertyField2.Type = new CodeTypeReference("System.String");
            propertyField2.GetStatements.Add(new CodeMethodReturnStatement(
                new CodeMethodReferenceExpression(new CodeThisReferenceExpression(), "_name")));
            newType.Members.Add(propertyField2);
            propertyField2.SetStatements.Add(new CodeAssignStatement(
            new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "_name"), new CodePropertySetValueReferenceExpression()));



            // Add the NameSpace to the CodeCompileUnit
            //
            compileUnit.Namespaces.Add(codedomsamplenamespace);



            // Return the CompileUnit
            //
            return compileUnit;
        }
        public void GenerateCodeAndWriteToFile(CodeCompileUnit ccu, String sourceFile)
        {

            //Delete Existing File...
            //TODO

            CSharpCodeProvider csharpcodeprovider = new CSharpCodeProvider();


            IndentedTextWriter tw = new IndentedTextWriter(new StreamWriter(sourceFile, false), "    ");

            CodeGeneratorOptions codeGeneratorOptions = new CodeGeneratorOptions();
            codeGeneratorOptions.BracingStyle = "C";


            csharpcodeprovider.GenerateCodeFromCompileUnit(ccu, tw, codeGeneratorOptions);
            tw.Close();
        }



        private void ccutbl_AddUsingStatements(ref CodeCompileUnit ccu)
        {
            CodeNamespace globalNamespace = new CodeNamespace();
            globalNamespace.Imports.Add(new CodeNamespaceImport("System"));
            globalNamespace.Imports.Add(new CodeNamespaceImport("System.Data"));
            globalNamespace.Imports.Add(new CodeNamespaceImport("System.Data.Common"));

            ccu.Namespaces.Add(globalNamespace);
        }

        private void GenTemp(ref CodeTypeDeclaration ctd)
        {
            string anyString = "property of chris";

            //// Make a nice property name by making it title case, and no spaces
            string propertyName = "PropertyOfChris";
            //propertyName = Regex.Replace(propertyName, @"\W|\s", "");

            // Make the private file starting with 2 underscores and an all lower name
            string privateFieldName = "__" + "propertyofchris";

            // Generate the private field
            CodeMemberField field = new CodeMemberField()
            {
                Name = privateFieldName,
                Type = new CodeTypeReference(typeof(string)),
                Attributes = MemberAttributes.Private,
            };

            // Generate the property
            CodeMemberProperty property = new CodeMemberProperty()
            {
                Name = propertyName,
                Type = new CodeTypeReference(typeof(string)),
                Attributes = MemberAttributes.Public | MemberAttributes.Final,
                HasGet = true,
                HasSet = true
            };

            // Add the return field statement to the property
            property.GetStatements.Add(new CodeMethodReturnStatement(
                new CodeFieldReferenceExpression(
                    new CodeThisReferenceExpression(), privateFieldName)));

            // Add the set field to value to the property
            property.SetStatements.Add(
                new CodeAssignStatement(
                    new CodeFieldReferenceExpression(
                        new CodeThisReferenceExpression(), privateFieldName),
                    new CodePropertySetValueReferenceExpression()));

            // Add a comment
            string comment = "Remember children: ALWAYS COMMENT YOUR CODE.";
            property.Comments.Add(new CodeCommentStatement(comment, true));

            // Add the XmlElement Attribute
            CodeAttributeDeclaration attribute = new CodeAttributeDeclaration(
                new CodeTypeReference(typeof(XmlElementAttribute)),
                    new CodeAttributeArgument("Type", new CodeTypeOfExpression(typeof(string))),
                    new CodeAttributeArgument("ElementName", new CodePrimitiveExpression(propertyName)),
                    new CodeAttributeArgument("Namespace", new CodePrimitiveExpression("http://xml.sirchristian.net")));


            property.CustomAttributes.Add(attribute);

            ctd.Members.Add(field);

            CodeSnippetTypeMember snippet = new CodeSnippetTypeMember();
            snippet.Comments.Add(new CodeCommentStatement("this is integer property", true));
            snippet.Text = "        private int IntergerProperty;";
            ctd.Members.Add(snippet);

            ctd.Members.Add(property);
        }


        private void ccutbl_AddNameScpaceAndClass(ref CodeCompileUnit ccu, ref DBTableDefination tableTemp, String strProjectName)
        {
            CodeNamespace codedomsamplenamespace = new CodeNamespace(strProjectName);


            CodeTypeDeclaration newType = new CodeTypeDeclaration(tableTemp.Name);
            newType.Attributes = MemberAttributes.Public;
            newType.IsPartial = true;
            newType.IsClass = true;

            CodeConstructor constructor = new CodeConstructor();
            constructor.Attributes = MemberAttributes.Public;
            newType.Members.Add(constructor);

            for (int i = 0; i < tableTemp.Columns.Count; i++)
            {

                CodeTypeReference ctr;
                switch (tableTemp.Columns[i].DataType)
                {
                    case "bigint":
                        if (tableTemp.Columns[i].IsNullable == "YES")
                        {
                            ctr = new CodeTypeReference("long?");
                        }
                        else
                        {
                            ctr = new CodeTypeReference("System.Int64");
                        }
                        break;
                    case "int":
                        if (tableTemp.Columns[i].IsNullable == "YES")
                        {
                            ctr = new CodeTypeReference("int?");
                        }
                        else
                        {
                            ctr = new CodeTypeReference("System.Int32");
                        }
                        break;
                    case "varchar":
                        if (tableTemp.Columns[i].IsNullable == "YES")
                        {
                            ctr = new CodeTypeReference("System.String?");
                        }
                        else
                        {
                            ctr = new CodeTypeReference("System.String");
                        }
                        break;
                    case "datetime":
                        if (tableTemp.Columns[i].IsNullable == "YES")
                            ctr = new CodeTypeReference("DateTime?");
                        else
                            ctr = new CodeTypeReference("DateTime");
                        break;
                    case "decimal":
                        if (tableTemp.Columns[i].IsNullable == "YES")
                            ctr = new CodeTypeReference("decimal?");
                        else
                            ctr = new CodeTypeReference("decimal");
                        break;
                    case "bit":
                        
                        if (tableTemp.Columns[i].IsNullable == "YES")
                            ctr = new CodeTypeReference("System.Boolean?");
                        else
                            ctr = new CodeTypeReference("System.Boolean");
                        break;
                    case "blob":
                        ctr = new CodeTypeReference("System.Byte[]");
                        break;
                    case "mediumblob":
                        ctr = new CodeTypeReference("System.Byte[]");
                        break;
                    case "longblob":
                        ctr = new CodeTypeReference("System.Byte[]");
                        break;
                    default:
                        ctr = new CodeTypeReference("System.String");
                        break;
                }

                CodeSnippetTypeMember snippet = new CodeSnippetTypeMember();
                snippet.Text = "";
                newType.Members.Add(snippet);

                CodeMemberField MemberField = new CodeMemberField();
                MemberField.Name = "_" + tableTemp.Columns[i].Name;
                MemberField.Type = ctr;
                MemberField.Comments.Add(new CodeCommentStatement("Column Comment...", true));
                MemberField.Attributes = MemberAttributes.Private;
                newType.Members.Add(MemberField);

                // Declares a property of type String named StringProperty.
                CodeMemberProperty MemberProperty = new CodeMemberProperty();
                MemberProperty.Name = tableTemp.Columns[i].Name;
                MemberProperty.Type = ctr;
                MemberProperty.Attributes = MemberAttributes.Public;
                MemberProperty.GetStatements.Add(new CodeMethodReturnStatement(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), MemberField.Name)));
                MemberProperty.SetStatements.Add(new CodeAssignStatement(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), MemberField.Name), new CodePropertySetValueReferenceExpression()));
                newType.Members.Add(MemberProperty);
            }

            //GenTemp(ref newType);
            //GenTemp(ref newType);

            codedomsamplenamespace.Types.Add(newType);

            ccu.Namespaces.Add(codedomsamplenamespace);
        }

        public void GenerateCodeAndWriteToFile(ref DBTableDefination tblTemp, String path, String strProjectName)
        {
            CodeCompileUnit ccu = new CodeCompileUnit();

            ccutbl_AddUsingStatements(ref ccu);

            ccutbl_AddNameScpaceAndClass(ref ccu, ref tblTemp, strProjectName);


            //Delete Existing File...
            //TODO

            

            IndentedTextWriter tw = new IndentedTextWriter(new StreamWriter(path + tblTemp.Name + ".generated.cs", false), "    ");
            CodeGeneratorOptions codeGeneratorOptions = new CodeGeneratorOptions();
            codeGeneratorOptions.BracingStyle = "C";
            codeGeneratorOptions.VerbatimOrder = true;
            codeGeneratorOptions.BlankLinesBetweenMembers = false;

            CSharpCodeProvider csharpcodeprovider = new CSharpCodeProvider();
            csharpcodeprovider.GenerateCodeFromCompileUnit(ccu, tw, codeGeneratorOptions);


            //Code to remove the Header Commetns NOt Working YEt...
            //using (var stringWriter = new StringWriter())
            //using (var streamWriter = new StreamWriter(tblTemp.Name + ".generated1.cs", false))
            //{
            //    csharpcodeprovider.GenerateCodeFromCompileUnit(ccu, streamWriter, codeGeneratorOptions);

            //    StringBuilder sb = stringWriter.GetStringBuilder();
            //    /* Remove the header comment (444 is for C#, use 435 for VB) */
            //    sb.Remove(0, 444);
            //    streamWriter.Write(sb);
            //}

            tw.Close();
        }

    }
}
