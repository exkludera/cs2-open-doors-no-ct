using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Core.Attributes.Registration;
using CounterStrikeSharp.API.Modules.Utils;

public class Plugin : BasePlugin
{
    public override string ModuleName => "OpenDoorsNoCT";
    public override string ModuleVersion => "";
    public override string ModuleAuthor => "exkludera";

    static void ForceEntInput(String name, String input)
    {
        var target = Utilities.FindAllEntitiesByDesignerName<CBaseEntity>(name);

        foreach (var ent in target)
        {
            if (!ent.IsValid)
                continue;

            ent.AcceptInput(input);
        }
    }

    private bool IsCTEmpty()
    {
        int CTCount = Utilities.GetPlayers().Where(p => p.Team.Equals(CsTeam.CounterTerrorist)).Count();
        if (CTCount >= 1)
            return false;
        return true;
    }

    [GameEventHandler]
    public HookResult RoundStart(EventRoundStart @event, GameEventInfo info)
    {
        if (IsCTEmpty() == true) {
            ForceEntInput("func_door", "Open");
            ForceEntInput("func_movelinear", "Open");
            ForceEntInput("func_door_rotating", "Open");
            ForceEntInput("prop_door_rotating", "Open");
        }
        return HookResult.Continue;
    }

}