# cpp

[![Join the chat at https://gitter.im/nateforsyth/cpp](https://badges.gitter.im/Join%20Chat.svg)](https://gitter.im/nateforsyth/cpp?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=badge)
A public repo for CPP students to collaborate

This project is written in Visual Studio 2015 RC using C#

It's a basic Console based Event Handler example using a delegate. Reflection is also used to make the output a little more spiffy.

1) A simple object is created (a person) and values are assigned to the publically visible properties.

2) The program subscribes to an event handler.

3) Upon changing values for the object's properties, the event handler is triggered, which outputs information pertaining to what changed.

There is a basic and reflection based output.

BASIC informs the value has changed; from -> to.

REFLECTION informs the property that has been changed, and the relevant values; from -> to.

* un/comment Line 1 of program.cs to switch between basic and reflection output.
