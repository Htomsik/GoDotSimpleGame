using System;
using Godot;


public class Player : KinematicBody2D
{
    #region Const

    private const int MoveSpeed = 10;
    
    private const int MaxMoveSpeed = 250;
    
    private const int GravitySpeed = 20;

    private const int JumpSpeed = 400;
    
    private const int LevelLimit = 1500;


    
    private const string JumpStartSprite = "JumpStart";
    
    private const string JumpEndSprite = "JumpEnd";
    
    private const string IdleSprite = "Idle";
    
    private const string WalkSprite = "Walk";
    
    private const string SitSprite = "Sit";
    
    private const string SitUpSprite = "SitUp";
    
  
    
    
    
    private const string MoveLeft = "PlayerMoveLeft";
    
    private const string MoveRight = "PlayerMoveRight";

    private const string MoveJump = "PlayerJump";
    
    private const string MoveSit = "PlayerSit";
    
    #endregion
    
    #region fields

    private Vector2 _linearVelocity = new Vector2(0, 0);

    private readonly Vector2 _floor = new Vector2(0, -1);

    private AnimatedSprite _sprite;
    
    private bool _canDoubleJump = true;
    
    #endregion

    #region Methods

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _sprite = (AnimatedSprite)GetNode("Sprite");
    }

    public override void _PhysicsProcess(float delta)
    {
        Gravity();
        
        Move();

        Jump();
        
        MoveAndSlide(_linearVelocity, _floor);
        
        Animate();
        
        base._PhysicsProcess(delta);
    }

    private void Animate()
    {
        if (_linearVelocity.x != 0)
        {
            _sprite.FlipH = _linearVelocity.x < 0;
        }
        
        if (_linearVelocity.x == 0 && _linearVelocity.y == 0 && Input.IsActionPressed(MoveSit))
        {
            _sprite.Play(SitSprite);
            return;
        }

        if (_sprite.Animation == SitSprite || (_sprite.Animation == SitUpSprite && _sprite.Frame < _sprite.Frames.GetFrameCount(SitUpSprite)-1))
        {
            _sprite.Play(SitUpSprite);
            return;
        }
        
        if (_linearVelocity.x == 0 && _linearVelocity.y == 0)
        {
            _sprite.Play(IdleSprite);
            return;
        }
        
        if (IsOnFloor() && _linearVelocity.x != 0)
        {
            _sprite.Play(WalkSprite);
            return;
        }
         
        if (_linearVelocity.y != 0)
        {
            _sprite.Play(_linearVelocity.y < 0 ? JumpStartSprite : JumpEndSprite);
        }
    }

    private void Jump()
    {
        if (Input.IsActionJustPressed(MoveJump) && _canDoubleJump)
        {
            _canDoubleJump = false;
            _linearVelocity.y = -JumpSpeed;
        }

        if (IsOnCeiling())
        {
            _linearVelocity.y = 1;
        }

        if (IsOnFloor())
        {
            _canDoubleJump = true;
        }
    }

    private void Move()
    {
        var highThenMax = Math.Abs(_linearVelocity.x) > MaxMoveSpeed;
        
        if (Input.IsActionPressed(MoveLeft) && !highThenMax)
        {
            _linearVelocity.x -=  MoveSpeed;
            return;
        }
        
        if (Input.IsActionPressed(MoveRight) && !highThenMax)
        {
            _linearVelocity.x  +=  MoveSpeed;
            return;
        }
        
        if (_linearVelocity.x != 0)
        {
            _linearVelocity.x += _linearVelocity.x > 0 ? -MoveSpeed : MoveSpeed;
        }
    }

    private void Gravity()
    {
        if (Position.y > LevelLimit)
        {
            GameOver();
            return;
        }
        
        if (!IsOnFloor())
            _linearVelocity.y += GravitySpeed;
        else
            _linearVelocity.y = 0;
    }

    private void GameOver()
    {
        GetTree().ChangeScene("res://Scenes/Level.tscn");
    }

  

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
        //  public override void _Process(float delta)
        //  {
        //      
        //  }
    #endregion
}

    
    


