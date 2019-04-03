	

#version 330

layout(location = 0) in vec3 aPosition;

out vec3 TexCoords;

uniform mat4 WorldViewProj;

void main()
{
    gl_Position = WorldViewProj * vec4(aPosition, 1.0);
    TexCoords = aPosition;
}