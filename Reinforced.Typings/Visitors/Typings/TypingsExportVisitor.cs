﻿using System;
using System.IO;
using System.Linq;
using Reinforced.Typings.Ast;
using Reinforced.Typings.Visitors.TypeScript;
#pragma warning disable 1591
namespace Reinforced.Typings.Visitors.Typings
{
    public partial class TypingsExportVisitor : TypeScriptExportVisitor
    {
        public override void Visit(RtDecorator node)
        {
            //no decorators allowed in the .d.ts
        }


        public TypingsExportVisitor(TextWriter writer, string tabulation) : base(writer, tabulation)
        {
        }
    }
}
