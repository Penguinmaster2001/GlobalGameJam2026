
class_name CharacterSprite extends TextureRect



@export var default_texture: Texture2D = load("res://Resources/Characters/player.png")

	

func change_character(character_name: String):
	texture = Character.CHARACTER_DETAILS.get(character_name.to_lower(), default_texture)
