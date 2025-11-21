# ⚡ Polygon Execution – Top-Down Shooter

Bienvenido a **Polygon Execution**, un shooter arcade *top-down* de estética **neón**, ritmo **techno** y combate frenético.  
Derrota oleadas de enemigos, dasha entre proyectiles y sobrevive lo máximo posible.

---

## 🎮 Características principales

### ✅ Sistema de Oleadas (WaveManager)
- Enemigos generados por rondas progresivas
- Detección automática de muerte de enemigos
- Inicio automático de la siguiente oleada cuando todos son eliminados
- Los enemigos que caen fuera del mapa mueren automáticamente y cuentan como eliminados
- Escalado de dificultad por tipo de enemigo

---

## 🚀 Jugador

### 🎯 Dash
- Movimiento instantáneo en la dirección del input (WASD)
- Deja un *trail* neón visual
- **Invulnerabilidad total durante el dash** - atraviesa enemigos sin recibir daño
- Cooldown visible en la UI (5 segundos)
- Reproduce un sonido especial al activarse
- Solo se puede usar si hay input de movimiento

### 🔫 Disparo
- Disparo continuo manteniendo **Click Izquierdo**
- Balas con física realista (`Rigidbody`)
- Colisionan con enemigos mediante tag `"Enemy"`
- Velocidad y daño configurables
- Auto-destrucción tras un tiempo para optimizar rendimiento

### ❤️ Sistema de Salud
- HP visible en la UI (100 puntos base)
- **Invulnerabilidad de 1 segundo tras recibir daño**
- Efecto visual de parpadeo durante invulnerabilidad
- Detección de caída fuera del mapa (muerte instantánea)
- Al llegar a 0 HP:
  - Reproduce sonido de muerte
  - Reinicia la escena tras el audio
  - Animación de muerte

### 🕹️ Controles
```
WASD          - Movimiento del personaje
Click Izq.    - Disparar (mantener presionado)
Espacio       - Dash
ESC           - Pausar/Reanudar
```

---

## 👾 Enemigos

### Tipos de enemigos (4 clases)
| Tipo | HP | Velocidad | Daño |
|------|-----|-----------|------|
| **Soldado** 🟢 | 50 | 10 | 5 |
| **Sargento** 🔵 | 100 | 12.5 | 10 |
| **Teniente** 🟡 | 150 | 15 | 20 |
| **Coronel** 🔴 | 250 | 17.5 | 50 |

### Mecánicas generales
- IA básica de persecución hacia el jugador
- Notifican su muerte al WaveManager automáticamente
- Muerte automática si caen fuera del escenario (Y < -10)
- Cooldown de 0.5s entre ataques al jugador
- **Respetan la invulnerabilidad del jugador** (tras daño y durante dash)
- Sonidos independientes al morir
- Estadísticas escaladas según el tipo

---

## 🔊 Sistema de Audio (SoundManager)

### 🎵 Música adaptativa
- **Música de gameplay** en bucle rotatorio (múltiples tracks)
- **Música de menú principal** (ambiente chill)
- **Música de pausa** (versión suave)
- Transiciones suaves entre estados
- Sistema de pausa/resume que mantiene el progreso de la canción

### 🔉 Efectos de sonido (SFX)
- 🔫 Disparo del jugador
- ⚡ Dash con efecto whoosh
- 💀 Muerte del jugador
- 👾 Muerte de enemigos
- 🖱️ Clicks de UI (menú/botones)
- 🎵 Feedback de pausa/resume

### 🎚️ Sistema de Singleton
- Instancia única que persiste entre escenas
- Acceso global mediante `SoundManager.Instance`
- Destruye duplicados automáticamente

---

## 🖥️ Interfaz de Usuario (UI)

### 🏠 Menú Principal (MainMenu)
- **Botón Jugar**: 
  - Oculta el menú
  - Activa gameplay UI y jugador
  - Inicia música de combate
  - Reanuda el tiempo (`Time.timeScale = 1`)
  - Inicia el WaveManager
- **Botón Ajustes**: abre panel de controles
- **Botón Salir**: cierra la aplicación (`Application.Quit()`)
- Pausa automática al inicio (`Time.timeScale = 0`)

### ⏸️ Menú de Pausa (PauseMenu)
- Se abre/cierra con **ESC**
- Pausa real del juego (`Time.timeScale = 0`)
- Cambia automáticamente la música a modo pausa
- **Resume** restaura música de gameplay
- Opciones disponibles:
  - Reanudar partida
  - Abrir ajustes
  - Volver al menú principal

### ⚙️ Panel de Ajustes (SettingsManager)
- Accesible desde menú principal y pausa
- **Sistema inteligente de navegación**:
  - Recuerda desde qué panel vienes
  - Botón "Volver" te devuelve al panel correcto
  - Sin uso de múltiples botones duplicados
- Muestra los **controles del juego**:
```
  MOVIMIENTO
    WASD - Mover personaje
  
  COMBATE
    Click Izquierdo - Disparar (mantener)
  
  HABILIDADES
    Space - Dash
  
  MENÚ
    ESC - Pausar/Reanudar
```

### 📊 HUD en partida
- Barra de vida del jugador
- Indicador de cooldown del dash
- Contador de oleada actual
- Enemigos restantes

---

## 📦 Scripts principales incluidos

### 🎮 Jugador
- `Jugador.cs` - Movimiento, dash y física
- `PlayerAim.cs` - Sistema de apuntado con mouse
- `PlayerHealth.cs` - Sistema de vida, invulnerabilidad y muerte
- `Shoot.cs` - Sistema de disparo continuo

### 🔫 Armas
- `BulletBehaviour.cs` - Física, colisiones y auto-destrucción de proyectiles

### 👾 Enemigos y Oleadas
- `EnemyAI.cs` - IA, persecución, ataque y gestión de tipos
- `SpawnPoint.cs` - Puntos de generación de enemigos
- `WaveManager.cs` - Sistema de oleadas y escalado de dificultad

### 🔊 Audio
- `SoundManager.cs` - Música adaptativa, efectos de sonido y singleton global

### 🖥️ UI y Menús
- `MainMenu.cs` - Menú principal y transiciones
- `PauseMenu.cs` - Sistema de pausa con música especial
- `SettingsManager.cs` - Navegación inteligente entre paneles
- `SettingsMenu.cs` - Panel de configuración
- `UIManager.cs` - Gestor general de interfaz
- `HealthBar.cs` - Visualización de vida del jugador
- `DashBar.cs` - Indicador de cooldown del dash

### 🎥 General
- `CameraFollow.cs` - Cámara que sigue al jugador
- `PlataformaMovil.cs` - Plataformas con movimiento

---

## 🏗️ Estructura del Proyecto
```
PolygonExecution/
├── Assets/
│   ├── Scenes/
│   │   └── MainScene.unity
│   ├── Scripts/
│   │   ├── Enemy/
│   │   │   ├── EnemyAI.cs
│   │   │   ├── SpawnPoint.cs
│   │   │   └── WaveManager.cs
│   │   ├── general/
│   │   │   ├── CameraFollow.cs
│   │   │   └── PlataformaMovil.cs
│   │   ├── Player/
│   │   │   ├── Jugador.cs
│   │   │   ├── PlayerAim.cs
│   │   │   ├── PlayerHealth.cs
│   │   │   └── Shoot.cs
│   │   ├── sound/
│   │   │   └── SoundManager.cs
│   │   ├── UI/
│   │   │   ├── DashBar.cs
│   │   │   ├── HealthBar.cs
│   │   │   ├── MainMenu.cs
│   │   │   ├── PauseMenu.cs
│   │   │   ├── SettingsManager.cs
│   │   │   ├── SettingsMenu.cs
│   │   │   └── UIManager.cs
│   │   └── Weapons/
│   │       └── BulletBehaviour.cs
│   ├── Prefabs/
│   │   ├── Player.prefab
│   │   ├── Enemies/
│   │   │   ├── Soldier.prefab
│   │   │   ├── Sergeant.prefab
│   │   │   ├── Lieutenant.prefab
│   │   │   └── Colonel.prefab
│   │   └── Bullet.prefab
│   ├── Audio/
│   │   ├── Music/
│   │   └── SFX/
│   ├── Materials/
│   └── UI/
└── README.md
```

---

## 🛠️ Requisitos técnicos

- **Unity Version**: 2022.3 LTS o superior
- **Render Pipeline**: URP (Universal Render Pipeline)
- **Input System**: Legacy Input Manager
- **Physics**: 3D Physics
- **Target Platform**: PC (Windows/Mac/Linux)

---

## 🎨 Características visuales

- Estética **neón retro-futurista**
- Efectos de **trail** en el dash
- Partículas de impacto
- Iluminación dinámica
- Post-processing (bloom, glow)
- Suelo reflectante estilo cyberpunk

---

## 🚧 Características futuras (Roadmap)

- [ ] Sistema de puntuación y high scores
- [ ] Power-ups y mejoras temporales
- [ ] Más tipos de armas
- [ ] Enemigos con comportamientos especiales
- [ ] Boss fights cada 5 oleadas
- [ ] Sistema de progresión permanente
- [ ] Modos de juego alternativos
- [ ] Leaderboards online
- [ ] Efectos visuales mejorados
- [ ] Más tracks de música

---

## 🐛 Bugs conocidos

- Ninguno reportado actualmente

---

## 👨‍💻 Desarrollo

Proyecto desarrollado en **Unity** como parte de un portfolio de game development.

### 🔧 Instalación para desarrollo

1. Clona el repositorio:
```bash
git clone https://github.com/AngelRagel05/PolygonExecution
```

2. Abre el proyecto en Unity Hub (versión 2022.3 LTS recomendada)

3. Abre la escena principal: `Assets/Scenes/MainScene.unity`

4. Dale al **Play** y disfruta

---

## 📧 Contacto

- **GitHub**: https://github.com/AngelRagel05
- **Email**: jimenezragelangel@gmail.com
- **Teléfono**: 603758003

---

## 🎮 ¡Juega ahora!

https://drive.google.com/drive/folders/1TTRt5qeZdJ_0_8Kt5elMyoJ2hSJdJQYq?usp=drive_link

---

**⚡ Made with Unity | 🎵 Powered by Techno | 💀 Designed for Chaos**