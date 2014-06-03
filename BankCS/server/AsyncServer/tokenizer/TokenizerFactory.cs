using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncServer.tokenizer
{
    public interface TokenizerFactory<T>
    {
        MessageTokenizer<T> create();
    }
}
