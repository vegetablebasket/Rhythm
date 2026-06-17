# RhythmYouth

一款基于 Unity 开发的 **3D 节奏障碍躲避游戏**。玩家在音乐节奏中移动与跳跃，躲避从远处飞来的墙体与跳跃障碍，尽可能获得高分与连击。

## 技术栈

| 项目 | 版本 / 说明 |
|------|-------------|
| 引擎 | Unity **2022.3.62f3c1** (LTS) |
| 语言 | C# |
| 渲染 | Built-in Render Pipeline |

## 游戏玩法

- 障碍根据 **谱面（Beat Map）** 与背景音乐时间轴同步生成
- 障碍类型：**墙体（Wall）** 与 **跳跃台（Jump）**
- 墙体有多种缺口模式：左、中、右、双缺口、单缺口、全封闭等
- 成功通过障碍获得分数与连击；失误会扣血并打断连击
- 生命值耗尽则游戏结束；坚持到曲目结束则通关

## 操作说明

| 按键 | 功能 |
|------|------|
| `W` `A` `S` `D` | 移动 |
| `空格` | 跳跃（支持 Coyote Time 与 Jump Buffer） |
| `鼠标` | 视角转动（游戏中启用） |
| `Esc` | 暂停 / 继续 |

## 项目结构

```
RhythmYouth/
├── README.md
├── Docs/                    # 项目文档（预留）
└── RhythmYouthGame/         # Unity 工程根目录
    ├── Assets/
    │   └── _Project/
    │       ├── Audio/       # 背景音乐
    │       ├── Materials/   # 材质
    │       ├── Prefabs/     # 预制体（障碍、玩家、UI）
    │       ├── Scenes/      # 场景（GameScene）
    │       └── Scripts/
    │           ├── Managers/    # 游戏、音频、分数、生命、UI 管理
    │           ├── Obstacles/   # 障碍生成与逻辑
    │           ├── Player/      # 玩家控制与碰撞检测
    │           └── Rhythm/      # 谱面与节拍事件
    └── ProjectSettings/
```

## 核心模块

| 模块 | 说明 |
|------|------|
| `GameManager` | 游戏状态机（主菜单 / 游戏中 / 暂停 / 结算 / 游戏结束） |
| `BeatMapManager` | 谱面数据管理，按音乐时间配置障碍生成 |
| `ObstacleSpawner` | 根据谱面与音乐进度生成障碍 |
| `PlayerController` | 角色移动、跳跃与活动区域限制 |
| `ScoreManager` | 分数、连击、命中率统计 |
| `HealthManager` | 生命值管理 |
| `AudioManager` | 背景音乐播放与时间同步 |

## 环境要求

- [Unity Hub](https://unity.com/download) + Unity Editor **2022.3.62f3c1**（或同系列 2022.3 LTS）
- Windows / macOS / Linux（以 Unity 官方支持为准）

## 快速开始

1. 克隆本仓库到本地
2. 打开 Unity Hub，点击 **Add**，选择 `RhythmYouthGame` 文件夹
3. 确认编辑器版本为 **2022.3.62f3c1**，打开项目
4. 在 Project 窗口打开 `Assets/_Project/Scenes/GameScene.unity`
5. 点击 **Play** 开始游戏

> 首次打开项目时 Unity 会重新导入资源，可能需要几分钟。

## 构建发布

1. 菜单栏选择 **File → Build Settings**
2. 将 `GameScene` 加入 **Scenes In Build**
3. 选择目标平台（如 Windows），点击 **Build** 或 **Build And Run**

## 开发说明

- 自定义谱面：编辑 `BeatMapManager` 中的 `beatEvents`，或通过 Inspector 右键 **Build Demo Beat Map** 生成示例谱面
- 障碍提前量由 `obstacleLeadTime` 控制，用于对齐音乐节拍与障碍到达玩家的时间
- 测试用障碍生成器见 `TestObstacleSpawner.cs`（开发调试用）

## 许可证

暂未指定许可证。如需开源或分发，请补充 LICENSE 文件。
