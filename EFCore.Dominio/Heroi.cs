using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCore.Dominio
{
    public class Heroi
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public IdentidadeSecreta Identidade { get; set; }   // nem sempre um Heroi pode ter um identidade secreta, não é necessario colocar um id not null
        public List<HeroiBatalha> HeroiBatalhas { get; set; }   // Um heroi pode ter várias batalhas (Relacionamneto muitos para muitos)
        public List<Arma> Armas { get; set; }    // Um heroi pode ter vários armas (Relacionamneto Um para muitos)

    }
}
