﻿#version 330
 
out vec4 FragColor;

in vec3 TexCoords;

uniform samplerCube skyboxSampler;

void main()
{
	FragColor = texture(skyboxSampler, TexCoords);
}