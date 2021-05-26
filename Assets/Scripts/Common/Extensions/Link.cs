using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Common.Extensions
{
    public class Link
    {
        public string AnswerText { get; set; }
        public string NextNodeId { get; set; }
        public string Condition { get; set; }

        public static IEnumerable<Link> ParseLine(string line)
        {
            var split = line.Split('\n');
            foreach (var word in split.Where(w => w.Length > 1))
            {
                if (word[0] == '[' && word[1] == '[')
                {
                    var link = word.Substring(2, word.Length - 4).Split('|');
                    yield return new Link() { AnswerText = link[0], NextNodeId = link[1] };
                }
            }
        }
    }
}
