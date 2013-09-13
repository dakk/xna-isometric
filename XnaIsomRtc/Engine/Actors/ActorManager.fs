namespace XnaIsomRtc.Actors

open System

type ActorManager() = 
    member val Actors : Actor list = [] with get, set
    
    