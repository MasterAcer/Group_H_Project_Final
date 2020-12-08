public class Upgrade
{
    public string description;
    public string affectedStat;
    public float multiplier;

    public Upgrade(string text, string stat, float mult)
    {
        description = text;
        affectedStat = stat;
        multiplier = mult;
    }

    public void applyUpgrade(ref CharacterController player)
    {
        switch (affectedStat)
        {
            case "maxHealth":
                player.maxHealth = player.maxHealth + (player.maxHealth * multiplier);
                break;
            case "Speed":
                player.speed = player.speed + (player.speed * multiplier);
                break;
            case "DMG":
                player.damageDealt = player.damageDealt + (player.damageDealt * multiplier);
                break;
            case "Fire Rate":
                player.fireRate = player.fireRate + (player.fireRate * multiplier);
                break;
            case "Jump":
                player.setJumpForce = player.setJumpForce + (player.setJumpForce * multiplier);
                break;
            default:
                break;
        }
        player.health = player.health + (player.health * multiplier);
    }
}

