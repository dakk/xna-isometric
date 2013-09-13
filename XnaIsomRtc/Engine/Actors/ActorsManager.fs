namespace XnaIsomRtc.Actors

open System

type ActorsManager() = 
    member val Actors : Actor list = [] with get, set
    
    