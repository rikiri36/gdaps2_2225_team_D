﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamD_bullet_hell
{
    enum GameState
    {
        Menu,
        Levels,
        Gameplay,
        Infinity,
        Pause,
        LeaderBoard

    }

    enum FontType
    {
        Title,
        Button,
        Normal
    }

    enum Entity
    {
        Player,
        Enemy,
        Bullet
    }

    enum ButtonAssets
    {
        Outline,
        BackButton
    }

    internal class EnumCollection
    {
    }
}
