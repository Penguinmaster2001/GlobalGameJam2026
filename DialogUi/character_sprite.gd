class_name CharacterSprite extends Node2D

@export var sprite: Sprite2D
@export var default_texture = load("res://Resources/Characters/player.png")

	
func change_character(character_name: String):
	sprite.texture = Character.CHARACTER_DETAILS.get(character_name, default_texture)
