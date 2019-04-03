using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenGL_Game.Components;
using OpenGL_Game.Objects;
using OpenGL_Game.Scenes;
using OpenTK.Input;

namespace OpenGL_Game.Systems
{
    class SystemCollision : ISystem
    {
        const ComponentTypes MASK = (ComponentTypes.COMPONENT_POSITION | ComponentTypes.COMPONENT_COLLISION | ComponentTypes.COMPONENT_VELOCITY);
        Vector3 up;
        Vector3 still;
        Vector3 down;
        Vector3 right;
        Vector3 left;
        Vector3 upfix;
        Vector3 downfix;
        Vector3 rightfix;
        Vector3 leftfix;
        Vector3 offscreen;
        int ghostCount = 0;
        public SystemCollision()
        {
            up = new Vector3(0.0f, 1.0f, 0.0f);
            still = new Vector3(0.0f, 0.0f, 0.0f);
            down = new Vector3(0.0f, -1.0f, 0.0f);
            right = new Vector3(1.0f, 0.0f, 0.0f);
            left = new Vector3(-1.0f, 0.0f, 0.0f);
            upfix = new Vector3(0.0f, 0.15f, 0.0f);
            downfix = new Vector3(0.0f, -0.15f, 0.0f);
            rightfix = new Vector3(0.15f, 0.0f, 0.0f);
            leftfix = new Vector3(-0.15f, 0.0f, 0.0f);
            offscreen = new Vector3(-100f, -100f, -100f);
        }

        public string Name
        {
            get { return "SystemPlayerControler"; }
        }

        public void OnAction(Entity entity, List<Entity> entityList)
        {
            if ((entity.Mask & MASK) == MASK)
            {
                foreach (Entity f in entityList)
                {
                    if (entity.Name != f.Name)
                    {
                        List<IComponent> components = entity.Components;

                        IComponent positionComponent = components.Find(delegate (IComponent component)
                        {
                            return component.ComponentType == ComponentTypes.COMPONENT_POSITION;
                        });

                        IComponent collisionComponent = components.Find(delegate (IComponent component)
                        {
                            return component.ComponentType == ComponentTypes.COMPONENT_COLLISION;
                        });

                        IComponent veloctyComponent = components.Find(delegate (IComponent component)
                        {
                            return component.ComponentType == ComponentTypes.COMPONENT_VELOCITY;
                        });

                        List<IComponent> fcomponents = f.Components;

                        IComponent fpositionComponent = fcomponents.Find(delegate (IComponent component)
                        {
                            return component.ComponentType == ComponentTypes.COMPONENT_POSITION;
                        });

                        IComponent fcollisionComponent = fcomponents.Find(delegate (IComponent component)
                        {
                            return component.ComponentType == ComponentTypes.COMPONENT_COLLISION;
                        });

                        IComponent fveloctyComponent = fcomponents.Find(delegate (IComponent component)
                        {
                            return component.ComponentType == ComponentTypes.COMPONENT_VELOCITY;
                        });
                        if (f.Name == "Ghost" || entity.Name == "Ghost")
                        {
                            if (entity.Name == "Player")
                            {
                                if (entity.Name == "Player")
                                {
                                    IComponent faudioComponent = fcomponents.Find(delegate (IComponent component)
                                    {
                                        return component.ComponentType == ComponentTypes.COMPONENT_AUDIO;
                                    });
                                    IComponent healthComponent = components.Find(delegate (IComponent component)
                                    {
                                        return component.ComponentType == ComponentTypes.COMPONENT_LIFE;
                                    });
                                    GhostPlayerCollision((ComponentPosition)positionComponent, (ComponentCollision)collisionComponent, (ComponentVelocity)veloctyComponent, (ComponentPosition)fpositionComponent, (ComponentCollision)fcollisionComponent, (ComponentVelocity)fveloctyComponent, (ComponentLife)healthComponent, entity.Name, entityList, (ComponentAudio)faudioComponent);
                                }
                            }
                            else if (entity.Name == "Wall" || f.Name == "Wall")
                            {
                                GhostWallCollision((ComponentPosition)positionComponent, (ComponentCollision)collisionComponent, (ComponentVelocity)veloctyComponent, (ComponentPosition)fpositionComponent, (ComponentCollision)fcollisionComponent, (ComponentVelocity)fveloctyComponent, entity.Name);
                            }
                        }
                        else if (f.Name == "Item"|| f.Name == "PowerItem")
                        {
                            IComponent faudioComponent = fcomponents.Find(delegate (IComponent component)
                            {
                                return component.ComponentType == ComponentTypes.COMPONENT_AUDIO;
                            });
                            IComponent ftextureComponent = fcomponents.Find(delegate (IComponent component)
                            {
                                return component.ComponentType == ComponentTypes.COMPONENT_TEXTURE;
                            });
                            ItemCollision((ComponentPosition)positionComponent, (ComponentCollision)collisionComponent, (ComponentVelocity)veloctyComponent, (ComponentPosition)fpositionComponent, (ComponentCollision)fcollisionComponent, (ComponentVelocity)fveloctyComponent, f, (ComponentAudio)faudioComponent, (ComponentTexture)ftextureComponent);
                        }
                        else if (f.Name == "Skybox")
                        {

                        }
                        else
                            Collision((ComponentPosition)positionComponent, (ComponentCollision)collisionComponent, (ComponentVelocity)veloctyComponent, (ComponentPosition)fpositionComponent, (ComponentCollision)fcollisionComponent, (ComponentVelocity)fveloctyComponent);
                    }
                }

            }
        }

        public void Collision(ComponentPosition positionComponent, ComponentCollision collisionComponent, ComponentVelocity velocityComponent, ComponentPosition fpositionComponent, ComponentCollision fcollisionComponent, ComponentVelocity fvelocityComponent)
        {

            var still = Vector3.Zero;

            
            if (positionComponent.Position.X > fpositionComponent.Position.X - 1 && positionComponent.Position.X < fpositionComponent.Position.X + 1 && positionComponent.Position.Y > fpositionComponent.Position.Y - 1 && positionComponent.Position.Y < fpositionComponent.Position.Y + 1)
            {
                if (velocityComponent.Velocity == up)
                {
                    positionComponent.Position += downfix;
                    velocityComponent.Velocity = still;
                }
                else if (velocityComponent.Velocity == down)
                {
                    positionComponent.Position += upfix;
                    velocityComponent.Velocity = still;
                }
                else if (velocityComponent.Velocity == right)
                {
                    positionComponent.Position += leftfix;
                    velocityComponent.Velocity = still;
                }
                else if (velocityComponent.Velocity == left)
                {
                    positionComponent.Position += rightfix;
                    velocityComponent.Velocity = still;
                }
            }
        }

        public void ItemCollision(ComponentPosition positionComponent, ComponentCollision collisionComponent, ComponentVelocity velocityComponent, ComponentPosition fpositionComponent, ComponentCollision fcollisionComponent, ComponentVelocity fvelocityComponent, Entity item, ComponentAudio faudioComponent, ComponentTexture ftextureComponent)
        {
            if (positionComponent.Position.X > fpositionComponent.Position.X - 1 && positionComponent.Position.X < fpositionComponent.Position.X + 1 && positionComponent.Position.Y > fpositionComponent.Position.Y - 1 && positionComponent.Position.Y < fpositionComponent.Position.Y + 1 && ftextureComponent.Texture != 0)
            {
                faudioComponent.Start();
                ftextureComponent.remove();
                fpositionComponent.Position = offscreen;
            }
        }

        public void GhostPlayerCollision(ComponentPosition positionComponent, ComponentCollision collisionComponent, ComponentVelocity velocityComponent, ComponentPosition fpositionComponent, ComponentCollision fcollisionComponent, ComponentVelocity fvelocityComponent, ComponentLife healthComponent, string entname, List<Entity> EntList, ComponentAudio faudioComponent)
        {
            int test = healthComponent.health();
            if(test == 1)
            {
                if (entname == "Ghost")
                {
                    if (positionComponent.Position.X > fpositionComponent.Position.X - 1 && positionComponent.Position.X < fpositionComponent.Position.X + 1 && positionComponent.Position.Y > fpositionComponent.Position.Y - 1 && positionComponent.Position.Y < fpositionComponent.Position.Y + 1)
                    {
                        GameScene.gameInstance.test();
                        faudioComponent.Start();
                    }
                }
                else
                {
                    if (fpositionComponent.Position.X > positionComponent.Position.X - 1 && fpositionComponent.Position.X < positionComponent.Position.X + 1 && fpositionComponent.Position.Y > positionComponent.Position.Y - 1 && fpositionComponent.Position.Y < positionComponent.Position.Y + 1)
                    {
                        GameScene.gameInstance.test();
                        faudioComponent.Start();
                    }
                }
            }
            else
            {
                if (entname == "Ghost")
                {
                    if (positionComponent.Position.X > fpositionComponent.Position.X - 1 && positionComponent.Position.X < fpositionComponent.Position.X + 1 && positionComponent.Position.Y > fpositionComponent.Position.Y - 1 && positionComponent.Position.Y < fpositionComponent.Position.Y + 1)
                    {
                        healthComponent.down();
                        fpositionComponent.Position = new Vector3(0.0f, 0.25f, -29.5f);
                        foreach(Entity x in EntList)
                        {
                            if(x.Name == "Ghost")
                            {
                                List<IComponent> components = x.Components;
                                IComponent GhostpositionComponent = components.Find(delegate (IComponent component)
                                {
                                    return component.ComponentType == ComponentTypes.COMPONENT_POSITION;
                                });
                                GhostResest((ComponentPosition)GhostpositionComponent);
                            }
                        }
                        ghostCount = 0;
                        faudioComponent.Start();
                    }
                }
                else
                {
                    if (fpositionComponent.Position.X > positionComponent.Position.X - 1 && fpositionComponent.Position.X < positionComponent.Position.X + 1 && fpositionComponent.Position.Y > positionComponent.Position.Y - 1 && fpositionComponent.Position.Y < positionComponent.Position.Y + 1)
                    {
                        healthComponent.down();
                        positionComponent.Position = new Vector3(0.0f, 0.25f, -29.5f);
                        foreach (Entity x in EntList)
                        {
                            if (x.Name == "Ghost")
                            {
                                List<IComponent> components = x.Components;
                                IComponent GhostpositionComponent = components.Find(delegate (IComponent component)
                                {
                                    return component.ComponentType == ComponentTypes.COMPONENT_POSITION;
                                });
                                GhostResest((ComponentPosition)GhostpositionComponent);
                            }
                        }
                        ghostCount = 0;
                        faudioComponent.Start();
                    }
                }
            }


        }

        public void GhostWallCollision(ComponentPosition positionComponent, ComponentCollision collisionComponent, ComponentVelocity velocityComponent, ComponentPosition fpositionComponent, ComponentCollision fcollisionComponent, ComponentVelocity fvelocityComponent, string entname)
        {
            if (entname == "Ghost")
            {
                if (positionComponent.Position.X > fpositionComponent.Position.X - 1 && positionComponent.Position.X < fpositionComponent.Position.X + 1 && positionComponent.Position.Y > fpositionComponent.Position.Y - 1 && positionComponent.Position.Y < fpositionComponent.Position.Y + 1)
                {
                    if (velocityComponent.Velocity == up)
                    {
                        positionComponent.Position += downfix;
                        int test;

                        Random rnd = new Random();
                        test = rnd.Next(4);
                        switch (test)
                        {
                            case 0:
                                velocityComponent.Velocity = right;
                                break;
                            case 1:
                                velocityComponent.Velocity = left;
                                break;
                            case 2:
                                velocityComponent.Velocity = up;
                                break;
                            case 3:
                                velocityComponent.Velocity = down;
                                break;
                        }
                    }
                    else if (velocityComponent.Velocity == down)
                    {
                        positionComponent.Position += upfix;
                        int test;

                        Random rnd = new Random();
                        test = rnd.Next(4);
                        switch (test)
                        {
                            case 0:
                                velocityComponent.Velocity = right;
                                break;
                            case 1:
                                velocityComponent.Velocity = left;
                                break;
                            case 2:
                                velocityComponent.Velocity = up;
                                break;
                            case 3:
                                velocityComponent.Velocity = down;
                                break;
                        }
                    }
                    else if (velocityComponent.Velocity == right)
                    {
                        positionComponent.Position += leftfix;
                        int test;

                        Random rnd = new Random();
                        test = rnd.Next(4);
                        switch (test)
                        {
                            case 0:
                                velocityComponent.Velocity = right;
                                break;
                            case 1:
                                velocityComponent.Velocity = left;
                                break;
                            case 2:
                                velocityComponent.Velocity = up;
                                break;
                            case 3:
                                velocityComponent.Velocity = down;
                                break;
                        }
                    }
                    else if (velocityComponent.Velocity == left)
                    {
                        positionComponent.Position += rightfix;
                        int test;

                        Random rnd = new Random();
                        test = rnd.Next(4);
                        switch (test)
                        {
                            case 0:
                                velocityComponent.Velocity = right;
                                break;
                            case 1:
                                velocityComponent.Velocity = left;
                                break;
                            case 2:
                                velocityComponent.Velocity = up;
                                break;
                            case 3:
                                velocityComponent.Velocity = down;
                                break;
                        }
                    }
                }
            }
            else
            {
                if (fpositionComponent.Position.X > positionComponent.Position.X - 1 && fpositionComponent.Position.X < positionComponent.Position.X + 1 && fpositionComponent.Position.Y > positionComponent.Position.Y - 1 && fpositionComponent.Position.Y < positionComponent.Position.Y + 1)
                {
                    if (fvelocityComponent.Velocity == up)
                    {
                        fpositionComponent.Position += downfix;
                        int test;

                        Random rnd = new Random();
                        test = rnd.Next(4);
                        switch (test)
                        {
                            case 0:
                                velocityComponent.Velocity = right;
                                break;
                            case 1:
                                velocityComponent.Velocity = left;
                                break;
                            case 2:
                                velocityComponent.Velocity = up;
                                break;
                            case 3:
                                velocityComponent.Velocity = down;
                                break;
                        }
                    }
                    else if (fvelocityComponent.Velocity == down)
                    {
                        fpositionComponent.Position += upfix;
                        int test;

                        Random rnd = new Random();
                        test = rnd.Next(4);
                        switch (test)
                        {
                            case 0:
                                velocityComponent.Velocity = right;
                                break;
                            case 1:
                                velocityComponent.Velocity = left;
                                break;
                            case 2:
                                velocityComponent.Velocity = up;
                                break;
                            case 3:
                                velocityComponent.Velocity = down;
                                break;
                        }
                    }
                    else if (fvelocityComponent.Velocity == right)
                    {
                        fpositionComponent.Position += leftfix;
                        int test;

                        Random rnd = new Random();
                        test = rnd.Next(4);
                        switch (test)
                        {
                            case 0:
                                velocityComponent.Velocity = right;
                                break;
                            case 1:
                                velocityComponent.Velocity = left;
                                break;
                            case 2:
                                velocityComponent.Velocity = up;
                                break;
                            case 3:
                                velocityComponent.Velocity = down;
                                break;
                        }
                    }
                    else if (fvelocityComponent.Velocity == left)
                    {
                        fpositionComponent.Position += rightfix;
                        int test;

                        Random rnd = new Random();
                        test = rnd.Next(4);
                        switch (test)
                        {
                            case 0:
                                velocityComponent.Velocity = right;
                                break;
                            case 1:
                                velocityComponent.Velocity = left;
                                break;
                            case 2:
                                velocityComponent.Velocity = up;
                                break;
                            case 3:
                                velocityComponent.Velocity = down;
                                break;
                        }
                    }
                }
            }
        }


        public void GhostResest(ComponentPosition Pos)
        {
            switch (ghostCount)
            {
                case 0:
                    Pos.Position = new Vector3(20f, -10.5f, -29f);
                    break;
                case 1:
                    Pos.Position = new Vector3(20f, -15f, -29.5f);
                    break;
                case 2:
                    Pos.Position = new Vector3(16f, -15f, -29.5f);
                    break;
                case 3:
                    Pos.Position = new Vector3(18f, -17f, -29.5f);
                    break;
            }
            ghostCount++;
        }
    }
}
