#region License

/*
 
Copyright (c) 2010-2014 Danko Kozar

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
 
*/

#endregion License

using System.Collections.Generic;
using System.Reflection;
using eDriven.Core.Data.Collections;
using eDriven.Gui.Data;
using eDriven.Gui.Designer.Adapters;
using eDriven.Gui.Reflection;
using UnityEngine;
using Component = eDriven.Gui.Components.Component;

namespace eDriven.Gui.Designer.Data
{
    [AddComponentMenu("eDriven/Gui/Data/ListItemDataProvider")]
    [Obfuscation(Exclude = true)]
    public class ListItemDataProvider : ComponentAdapterBase
    {
        [Saveable]
        [SerializeField]
        public string[] Keys = { };

        [Saveable]
        [SerializeField]
        public string[] Values = { };

        // ReSharper disable UnusedMember.Local
        [Obfuscation(Exclude = true)]
        void InitializeComponent(Component component)
        // ReSharper restore UnusedMember.Local
        {
            if (!enabled)
            {
                Debug.Log("ListItemDataProvider not enabled");
                return;
            }

            Apply(component);
        }

        // ReSharper disable UnusedMember.Local
        void Update()
        // ReSharper restore UnusedMember.Local
        {
        
        }

        public override void Apply(Component component)
        {
            DataGroup dataProviderClient = component as DataGroup;
            if (null == dataProviderClient)
            {
                Debug.LogWarning("GUI component is not a IDataProviderClient");
                return;
            }

            List<object> list = new List<object>();
            int count = Keys.Length;
            for (int i = 0; i < count; i++)
            {
                list.Add(new ListItem(Keys[i], Values[i]));
            }

            dataProviderClient.DataProvider = new ArrayList(list);
        }
    }
}