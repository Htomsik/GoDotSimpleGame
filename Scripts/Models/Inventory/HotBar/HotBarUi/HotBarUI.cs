using System.Collections.Generic;
using Godot;


namespace SimpleGame.Scripts.Models.Inventory.HotBar;

public class HotBarUi : Container
{
    #region Properties

    protected int OldSelected { get; set; }

    #endregion

    #region Физические свойства

    protected List<HotBarUiPanel> Panels { get; } = new ();

    protected IHotBar HotBar
    {
        get => _hotBar;
        set
        {
            _hotBar = value;
            
            ReCreatePanels();
            SetHotBarSubscriptions();
        }
    }

    private IHotBar _hotBar;

    #endregion

    #region Constructors

    public HotBarUi(IHotBar hotBar)
    {
        HotBar = hotBar;
    }

    #endregion
    
    #region Methods

    protected virtual void SetHotBarSubscriptions()
    {
        HotBar.SelectionChanged += id =>
        {
            Panels[OldSelected].Selected = false;
            Panels[id].Selected = true;
            OldSelected = id;
        };
    }

    /// <summary>
    ///     Пересборка панелей пр ининцалзаци нового хотбара
    /// </summary>
    public virtual void ReCreatePanels()
    {
        var count = 0;

        foreach (var elem in HotBar.Box)
        {
            var uiPanel = new HotBarUiPanel(elem);
            
            uiPanel.SetPosition(new Vector2(count, 0));

            count += 20;
            
            AddChild(uiPanel);
            
            Panels.Add(uiPanel);
        }

        if (Panels.Count == 0)
        {
            return; 
        }


        MarginLeft = - count / 2;
        MarginRight = - MarginLeft;
        MarginTop = -Panels[0].RectSize.x / 2;
        MarginBottom = -MarginTop;
    }

    public void SetPositionByCameraSize(Vector2 position)
    {
        position.x /= - 3f;

        position.y /=  3f;
            
        RectPosition = position;
    }

    #endregion
}