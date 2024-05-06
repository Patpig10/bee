# Garden

Name: David Abaya

Student Number: C21326401

# Description

In this project you'll be in a garden looking at various animals for example bees, birds and snake. Seeing how they work and react to the weather.

## Video:
https://youtu.be/T49Gc0grQfU
## Screenshots
![image](https://github.com/Patpig10/bee/assets/99894564/3ec79569-c816-49cc-9362-4b5ae2e7fb87)
![image](https://github.com/Patpig10/bee/assets/99894564/e1f592c3-8138-49de-9b65-6857f34bdbff)


# Instructions
You use WASD to move in the scene, mouse to look around
U key to make it rain
I key to make it sunny
# How it works
In this project, each animal is represented by a "boid" (a simulated creature) that is governed by a set of behaviors attached to it. These behaviors dictate what the animal will do based on specific conditions being met. For example, a bird might exhibit flocking behavior or seek shelter when it rains, while a bee might search for flowers to collect nectar.

To interact with the simulation, you can use keyboard inputs:

Pressing U triggers rain, allowing you to observe how the animals react. Birds might flock together or seek shelter, while bees could alter their behavior to stay airborne or seek cover.
Pressing I switches the weather to sunny, causing the animals to adjust their actions accordingly.
# List of classes/assets

| Class/asset | Source |
|-----------|-----------|
| Boid.cs | Self written |
| BirdWingAnimator.cs | Self written |
| FlightBehavior.cs | Self written |
| GroundedBehavior.cs | Self written |
| BirdBoid.cs | Self written |
| link.cs | Self written |
| GoToTreeBehavior.cs | Self written |
| RainCollisionBehavior.cs | Self written |
| RaindropSpawner.cs | Self written |
| FreeRoamCameraController.cs | Self written |
| SkyboxManager.cs | Self written |
| Weather.cs | Self written |
| WeatherController.cs | Self written |
| BeeStateMachine.cs | Self written |
| CreateBeeCreature.cs | Self written |
| WingFlapAnimator.cs | Self written |
| DisperseBehavior.cs | Self written |
| FlowerSeekBehavior.cs | Self written |
| WanderBehavior.cs | Self written |
| Flower.cs | Self written |
| FlowerSpawner.cs | Self written |
| FormationController.cs | Self written |
| Harmonic.cs | From skooter500 |
| NoiseWander.cs | From skooter500 |
| SpineAnimator.cs | From skooter500 |

## What I did

In this project, I took on the task of developing the entire bird simulation and its associated behaviours and interactions independently. This included creating scripts like Boid.cs for bird movement, FlightBehavior.cs for flying actions, and RaindropSpawner.cs for rain effects. I also designed behaviours for bees (BeeStateMachine.cs) and flower spawning, as well as implementing features for flocking (FormationController.cs).

To enhance the project, I integrated some existing code into the work. For instance, I utilized scripts like Harmonic.cs and NoiseWander.cs, developed by my professor skooter500, to add specific functionalities for the snake. Throughout the project, I worked independently to create a cohesive simulation environment, ensuring that all elements interacted realistically. The only asset that isn't mine is the skybox.
## What I'm most proud of

What I'm most proud of in this project is the level of depth and complexity I achieved with the bee simulation and its behaviors. Building the entire system from scratch and seeing it come to life with realistic  flight dynamics, and interactions was incredibly rewarding.
## What I've learned

Through this project, I've significantly enhanced my programming skills in C# within Unity and how to optimize my game. I've improved in object-oriented programming (OOP) by creating scripts for bird flight, bee behaviours, and environmental interactions like rain and flower seeking.
# References

This playlist contain what I used for bird flight and bees in the rain
https://www.youtube.com/playlist?list=PLBSn1y6NuXhyEO6b4RfqWj7jboqpPJiMF

