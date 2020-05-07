using _0x46696E616C.CommandPattern.Commands;
using _0x46696E616C.MobHandler.Units;
using _0x46696E616C.TechManager.Stats;
using NationBuilder.TileHandlerLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0x46696E616C.Units.Attacks
{
    class Ranged : AttackType
    {
        ProjectileManager shotManager;
        TextureValue projectile;
        public Ranged(ProjectileManager shotManager, TextureValue projectile) : base()
        {
            this.shotManager = shotManager;
            this.projectile = projectile;
            
        }
        public override void Attack(IEntity Target, IUnit Attacker, float Damage)
        {
            shotManager.AddProjectile(new Projectile(Target.Position, 2.5f, Attacker.Position, projectile, ((BasicUnit)Attacker).stats[typeof(MeleeDamage)].Value + ((BasicUnit)Attacker).teamStats[typeof(MeleeDamage)].Value, (BasicUnit)Attacker));
        }
    }
}
