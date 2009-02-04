// Copyright 2007-2008 The Apache Software Foundation.
//  
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use 
// this file except in compliance with the License. You may obtain a copy of the 
// License at 
// 
//     http://www.apache.org/licenses/LICENSE-2.0 
// 
// Unless required by applicable law or agreed to in writing, software distributed 
// under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR 
// CONDITIONS OF ANY KIND, either express or implied. See the License for the 
// specific language governing permissions and limitations under the License.
namespace Magnum
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Text;
    using Reflection;

    public class FunctionTimer : IDisposable
    {
        private readonly Action<string> _action;
        private readonly string _description;
        private readonly List<Stopwatch> _marks = new List<Stopwatch>(10);
        private readonly Stopwatch _stopwatch;
        private DateTime _started;

        public FunctionTimer(string description, Action<string> action)
        {
            _description = description;
            _action = action;
            _started = DateTime.UtcNow;

            _stopwatch = Stopwatch.StartNew();
        }

        public virtual string Header
        {
            get
            {
                StringBuilder sb = new StringBuilder(256);

                sb.Append("Date Time TimeTaken");

                for (int i = 0; i < _marks.Count; i++)
                {
                    sb.Append(" Mark").Append(i + 1);
                }

                OutputAdditionalHeaderValues(sb);

                if (!string.IsNullOrEmpty(_description))
                    sb.Append(" Description");

                return sb.ToString();
            }
        }

        public void Dispose()
        {
            _stopwatch.Stop();

            _action(ToString());
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder(256);

            sb.Append(_started.ToString("yyyy-MM-dd HH:mm:ss ")).Append(_stopwatch.ElapsedMilliseconds);

            foreach (Stopwatch mark in _marks)
            {
                sb.Append(' ').Append(mark.ElapsedMilliseconds);
            }

            OutputAdditionalValues(sb);

            if (!string.IsNullOrEmpty(_description))
                sb.Append(" ").Append(_description);

            return sb.ToString();
        }

        protected virtual void OutputAdditionalValues(StringBuilder sb)
        {
        }

        protected virtual void OutputAdditionalHeaderValues(StringBuilder sb)
        {
        }

        public CheckPoint Mark()
        {
            Stopwatch watch = Stopwatch.StartNew();
            _marks.Add(watch);

            CheckPoint point = new CheckPoint(watch);

            return point;
        }


        public class CheckPoint : IDisposable
        {
            private readonly Stopwatch _stopwatch;

            public CheckPoint(Stopwatch stopwatch)
            {
                _stopwatch = stopwatch;
            }

            public void Dispose()
            {
                _stopwatch.Stop();
            }
        }
    }

    public class FunctionTimer<T> :
        FunctionTimer
        where T : class, new()
    {
        private readonly T _values;

        public FunctionTimer(string description, Action<string> action)
            : base(description, action)
        {
            _values = new T();
        }

        public T Values
        {
            get { return _values; }
        }

        protected override void OutputAdditionalValues(StringBuilder sb)
        {
            foreach (var item in ReflectionCache<T>.GetEnumerator(_values))
            {
                sb.Append(' ').Append(item.Value ?? "null");
            }
        }
    }
}