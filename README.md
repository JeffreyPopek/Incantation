# Speech Recognition in Unity

This Unity project demonstrates two methods of integrating speech recognition as an input mechanism for casting spells within a game environment

## Overview

The project explores:

1. **Online AI-Based Speech Recognition**: Utilizing HuggingFace's API to convert voice recordings into text, which are then matched to predefined spell incantations using Levenshtein distance.

2. **Microsoft Phrase Recognizer**: Employing Unity's integration with Microsoft's speech recognition to match spoken phrases directly to predefined commands.

## Features

- **Voice Command Recognition**: Cast spells using voice commands interpreted through speech recognition.
- **Flexible Matching with AI Integration**: Implements Levenshtein distance to allow for minor discrepancies in spoken incantations.
- **Offline Recognition with Microsoft Tools**: Utilizes Microsoft's Phrase Recognizer for offline speech command recognition.

## Implementation Details

### 1. Online AI-Based Speech Recognition

- **Voice Recording**: Captures user input and converts it to a `.wav` file.
- **HuggingFace API**: Sends the `.wav` file to HuggingFace's API to transcribe speech to text.
- **Levenshtein Distance Matching**: Compares the transcribed text to a dictionary of spell incantations to determine the closest match.

### 2. Microsoft Phrase Recognizer

- **Grammar Definition**: Defines recognized phrases in an `.xml` file.
- **Grammar Recognizer Setup**: Initializes and binds the grammar recognizer to handle recognized phrases.
- **Command Execution**: Executes corresponding spell commands when phrases are recognized.

## Pros and Cons

### Online AI-Based Method

**Pros**:
- Flexible matching allows for minor user errors in incantations.

**Cons**:
- Requires an active internet connection for API calls.

### Microsoft Phrase Recognizer Method

**Pros**:
- Functions offline once set up.
- High accuracy with predefined phrases.

**Cons**:
- Limited to exact phrase matches; less tolerant to variations in user input.

## Learnings

This project provided insights into integrating alternative input methods in Unity, highlighting the trade-offs between flexibility and reliability in speech recognition implementations.

## References

- [Jeffrey Popek's Blog Post on Speech Recognition in Unity](https://jeffreypopek.dev/blog-speech-recog.html)
- [HuggingFace Speech-to-Text API](https://huggingface.co)
- [Unity Documentation on Grammar Recognizer](https://docs.unity3d.com/ScriptReference/Windows.Speech.GrammarRecognizer.html)
