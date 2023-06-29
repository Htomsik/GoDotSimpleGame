using System.Collections.Generic;
using Godot;

namespace SimpleGame.Scripts.Models.HotBar;

public class HotBarUi : Container
{
    #region Properties

    protected int OldSelected { get; set; }

    #endregion

    #region Физические свойства

    protected List<HotBarUiPanel> Panels { get; } = new ();

    protected IHotBar Bar
    {
        get => _bar;
        set
        {
            _bar = value;
            
            CreatePanels();
            SetHotBarSubscriptions();
        }
    }

    private IHotBar _bar;

    #endregion

    #region Constructors

    public HotBarUi(IHotBar bar)
    {
        Bar = bar;
    }

    #endregion
    
    #region Methods

    protected virtual void SetHotBarSubscriptions()
    {
        Bar.SelectionChanged += id =>
        {
            Panels[OldSelected].Selected = false;
            Panels[id].Selected = true;
            OldSelected = id;
        };

        Bar.Box.BoxChanged += InitializePanels;
    }

    /// <summary>
    ///     Пересборка панелей пр ининцалзаци нового хотбара
    /// </summary>
    public virtual void CreatePanels()
    {
        var count = 0;
        
        for (var id = 0; id < Bar.Box.MaxItemsCount; id++)
        {
            var uiPanel = new HotBarUiPanel();
            
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

    public virtual void InitializePanels()
    {
        for (var id = 0; id < Bar.Box.Count; id++)
        {
            Panels[id].Item = Bar.Box[id];
        }
    }

    public void SetPositionByCameraSize(Vector2 position)
    {
        position.x /= - 3f;

        position.y /=  3f;
            
        RectPosition = position;
    }

    #endregion
}