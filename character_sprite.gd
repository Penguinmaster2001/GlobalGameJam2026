class_name CharacterSprite extends Node2D

@export var sprite: Sprite2D


# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	pass # Replace with function body.
	
func change_character(character_name: Character.Name): 
	sprite.texture = Character.CHARACTER_DETAILS[character_name]["sprite"]
